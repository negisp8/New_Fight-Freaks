Shader "Unlit/ImageEffect0"
{
	//白黒にするShaderです
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Param ("mix-Ratio", Range(0,1)) = 0
		_Param2 ("param", Float) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Param;
			float _Param2;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				//モザイクにする
				/*i.uv = floor(i.uv*_Param2)/_Param2;*/

				//白黒にする
				fixed4 col = tex2D(_MainTex, i.uv);
				float c = (col.r+col.g+col.b)/3;
				fixed4 col2 = col;
				col2.rgb = c;
				if(col.r<1){
					col = col*(1-_Param)+col2*_Param;
				}
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}

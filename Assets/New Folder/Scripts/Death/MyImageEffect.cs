using UnityEngine;
using System.Collections;

public class MyImageEffect : MonoBehaviour {
    //死亡の時カメラにshaderを読む
	[SerializeField]Shader shader;
	[SerializeField]
	[Range(0,1)]
	float param = 0f;
	[SerializeField]
	[Range(500,5000)]
	float param2 = 0f;

    public float m_life;

    Material material;

	void Awake(){
		material = new Material(shader);
	}
	public void OnRenderImage(RenderTexture source, RenderTexture destination){
		UpdateMaterial();
		Graphics.Blit(source,destination,material);
	}

	void UpdateMaterial(){
        m_life = this.GetComponentInParent<Combat>().health;
        
        if(m_life <= 0)
            material.SetFloat("_Param", 1);
        if (m_life > 0)
            material.SetFloat("_Param", 0);
        material.SetFloat("_Param2",param2);
	}
}

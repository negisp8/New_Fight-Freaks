using UnityEngine;
using System.Collections;

public class pointerCtrl : MonoBehaviour
{
	public float scrollSpeed = 0.5f;
	public float pulseSpeed = 1.5f;
	public float noiseSize = 1.0f;
	public float maxWidth = 0.5f;
	public float minWidth = 0.5f;
	
	private float aniDir = 1.0f;
	private LineRenderer lRenderer;
	public GameObject pointer = null;//赤点
    private PreFrameRaycast raycast;//飛ばしたレイ

    void Start ()
	{
		lRenderer = gameObject.GetComponent (typeof(LineRenderer)) as LineRenderer;
		raycast = gameObject.GetComponent (typeof(PreFrameRaycast)) as PreFrameRaycast;
	}
	
	// Update is called once per frame
	void Update ()
	{
        //レイのエフェクト
        GetComponent<Renderer>().material.mainTextureOffset += new Vector2 (Time.deltaTime * aniDir * scrollSpeed, 0);
        //
        GetComponent<Renderer>().material.SetTextureOffset ("_NoiseTex", new Vector2 (-Time.time * aniDir * scrollSpeed, 0.0f));
		
		float aniFactor = Mathf.PingPong (Time.time * pulseSpeed, 1.0f);
		aniFactor = Mathf.Max (minWidth, aniFactor) * maxWidth;
        //レイの幅
        lRenderer.SetWidth (aniFactor, aniFactor);
        //レイの原点は銃口
        lRenderer.SetPosition(0,this.gameObject.transform.position);
		if (raycast == null) {
			Debug.Log ("raycast is null");
			return;
		}
        //衝突したオブジェクトの情報をもらう
        RaycastHit hitInfo = raycast.GetHitInfo ();
		if (hitInfo.transform) {
            //レイの終点は衝突した点
            lRenderer.SetPosition (1, hitInfo.point);
			GetComponent<Renderer>().material.mainTextureScale = new Vector2 (0.1f * (hitInfo.distance), GetComponent<Renderer>().material.mainTextureScale.y);
			GetComponent<Renderer>().material.SetTextureScale ("_NoiseTex", new Vector2 (0.1f * hitInfo.distance * noiseSize, noiseSize));
			
			if (pointer) {
				pointer.GetComponent<Renderer>().enabled = true;
				pointer.transform.position = hitInfo.point + (transform.position - hitInfo.point) * 0.01f;
				pointer.transform.rotation = Quaternion.LookRotation (hitInfo.normal, transform.up);
				pointer.transform.eulerAngles = new Vector3 (90, pointer.transform.eulerAngles.y, pointer.transform.eulerAngles.z);
			}		
		} else {//衝突しなかった
			if (pointer) {
				pointer.GetComponent<Renderer>().enabled = false;
			}
            //レイの最大長さ
            float maxDist = 200.0f;
            //衝突しなかった場合、最大長さのところが終点
            lRenderer.SetPosition (1, (this.transform.forward * maxDist));
			GetComponent<Renderer>().material.mainTextureScale = new Vector2 (0.1f * maxDist, GetComponent<Renderer>().material.mainTextureScale.y);
			GetComponent<Renderer>().material.SetTextureScale ("_NoiseTex", new Vector2 (0.1f * maxDist * noiseSize, noiseSize));
			
		}
	}
}

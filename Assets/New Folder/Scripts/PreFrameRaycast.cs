using UnityEngine;
using System.Collections;

public class PreFrameRaycast : MonoBehaviour {
    //レイを飛ばした際に、衝突したオブジェクトの情報を得るためのScript
    private RaycastHit hitInfo;
	private Transform  tr;
	// Use this for initialization
	void Start () {
	
	}
	
	void Awake(){
		tr = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
	    hitInfo = new RaycastHit();
		Physics.Raycast(tr.position,tr.forward,out hitInfo);
		Debug.DrawRay(tr.position,tr.forward,Color.red);
	}

    //衝突したオブジェクトの情報return
    public RaycastHit GetHitInfo(){
		if(hitInfo.Equals(null)){
			Debug.LogWarning("hitInfo is null");
		}
		return hitInfo;
	}
}




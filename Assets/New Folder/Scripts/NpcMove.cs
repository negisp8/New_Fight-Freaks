using UnityEngine;
using System.Collections;

public class NpcMove : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        Vector3 a = new Vector3 (Mathf.Sin(Time.time)*Time.deltaTime* speed,-1,0);
        transform.position = a;
        //transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }
}

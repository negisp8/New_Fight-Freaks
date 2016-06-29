using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {
    public Transform Hud;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = Hud.position;
	}
}

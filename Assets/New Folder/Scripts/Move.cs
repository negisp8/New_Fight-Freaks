using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
    public bool sw = false;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        DoorCtrl();
    }
    void OnTriggerEnter(Collider other)
    {
        var hit = other.gameObject;
        Debug.Log("HIT:" + hit.name);
        if (hit.tag.CompareTo("Player'sHand") == 0)
        {
                Debug.Log("OPEN-CLOSE");
                //  CmdSwitch();
                sw = !sw;
                Debug.Log(sw);
        }
    }
    void DoorCtrl()
    {
        float doorYpos = transform.localPosition.x;
        if (sw)
        {
            if (doorYpos < 3.0f)
            {
                doorYpos += Time.deltaTime * 1;
                if (doorYpos >= 3.0f)
                {
                    doorYpos = 3.0f;
                }
                transform.localPosition = new Vector3(doorYpos,0, 0);
            }
        }
        else
        {
            if (doorYpos > 0.0f)
            {
                doorYpos -= Time.deltaTime * 1;
                if (doorYpos <= 0.0f)
                {
                    doorYpos = 0.0f;
                }
                transform.localPosition = new Vector3(doorYpos,0,0);
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {
    public float speed=1.0f;
    bool sw=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        DoorCtrl();
    }
    public void switchON(bool a)
    {
        sw = a;
    }
    void DoorCtrl()
    {
        float doorYpos = transform.localPosition.y;
        if (sw)
        {
            if (doorYpos < 2.0f)
            {
                doorYpos += Time.deltaTime * 1;
                if (doorYpos >= 2.0f)
                {
                    doorYpos = 2.0f;
                }
                transform.localPosition = new Vector3(5, doorYpos, 1.5f);
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
                transform.localPosition = new Vector3(5, doorYpos, 1.5f);
            }
        }
    }
}

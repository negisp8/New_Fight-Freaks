using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {
    public bool sw = false;
    public GameObject Like_door;
    Animator anim;
    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
    }
    void OnTriggerEnter(Collider other)
    {
        var hit = other.gameObject;
        Debug.Log("HIT:" + hit.name);
        if (hit.tag.CompareTo("Player'sHand") == 0)
        {
            sw = !sw;
            Debug.Log(sw);
            anim.SetBool("Sw_Ani", sw);
            Like_door.GetComponent<door>().switchON(sw);
        }
    }

    
 
   /* IEnumerator m_LocalPositon(int u)
    {
        float a = transform.localPosition.y;
        bool chk = true;
        while (chk)
        {
            a = a + Time.deltaTime * u;
            if (a >= 3) { a = 3; chk = false; }
            if (a <= 0) { a = 0; chk = false; }
            transform.localPosition = new Vector3(5, a, 1.5f);
            yield return 0;
        }
        sw = false;
    }*/
}

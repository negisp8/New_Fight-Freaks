using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
    CharacterController m_ch;
    Rigidbody rig;
    
    public float jumpForce = 8.0F;
    // Use this for initialization
    void Start () {
        //获取角色控制器组件
        m_ch = this.GetComponent<CharacterController>();
        rig = this.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (m_ch.isGrounded)
        {
            Debug.Log("isGrounded");
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log(jumpForce);
                rig.AddForce(0, -jumpForce, 0);
            }
        }
    }
}

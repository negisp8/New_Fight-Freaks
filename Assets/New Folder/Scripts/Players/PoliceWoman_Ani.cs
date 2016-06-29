using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PoliceWoman_Ani : NetworkBehaviour
{
	Animator anim;
	CharacterController chara;

    bool dead_once = false;
    // Use this for initialization
    void Start  ()
    {
		anim = GetComponent<Animator>();
        chara = GetComponent<CharacterController>();
     
	}
    public override void OnStartClient()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) { return; }
        Cmd_animCtrl();
    }
    //[Command]
    void Cmd_animCtrl()
    {
        if (this.GetComponent<Combat>().health <= 0)
        {
            if (dead_once == false)
            {
                anim.PlayInFixedTime("DAMAGED01", -1, 0);
                dead_once = true;
            }
            Invoke("deadAnim", 2);
            return;
        }
        else
        {
            dead_once = false;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Wait", false);
            anim.SetFloat("Speed", 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("Wait", false);
            anim.SetFloat("Speed", -1);
        }
        else
        {
            anim.SetBool("Wait", true);
            anim.SetFloat("Speed", 0);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (!chara.isGrounded)
            {
                //anim.SetBool("Jump", true);
                anim.PlayInFixedTime("JUMP00");
            }
            
        }
        else
        {
            anim.SetBool("Jump", false);
        }
    }

    void deadAnim()
    {
        anim.PlayInFixedTime("DAMAGED01", -1, 2);
    }
}

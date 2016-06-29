using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PoliceRabirabi_Ani : NetworkBehaviour
{
    Animator anim;
    CharacterController chara;
    // Use this for initialization
    public override void OnStartLocalPlayer()
    {
        anim = GetComponent<Animator>();
        chara = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update(){
        if (!isLocalPlayer) { return; }

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

        if (Input.GetButtonDown("Jump") && !chara.isGrounded)
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }
    }
}

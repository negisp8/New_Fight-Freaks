using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Combat : NetworkBehaviour {
    
    public const int maxHealth = 100;
    [SyncVar]
    public int health = maxHealth;
    
    public void TakeDamage(int amount)
    {
        Debug.Log("take damage.");
        if (!isServer)
            return;
        Debug.Log("-" + amount);
        health -= amount;
        if (health <= 0)
        {
            Debug.Log("Dead!");
            Invoke("RpcRespawn", 5);
            
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Rigidbody body = hit.collider.attachedRigidbody;
        if (hit.gameObject.tag.CompareTo("Dead") == 0)
        {
            health = 0;
            Invoke("RpcRespawn", 5);
        }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer){
            transform.position = new Vector3(-17,1,-3);
        }
        health = maxHealth;
    }
}

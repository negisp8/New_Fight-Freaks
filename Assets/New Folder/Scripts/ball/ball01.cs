using UnityEngine;
using System.Collections;
//using UnityEngine.Networking;

public class ball01 : MonoBehaviour {
    public Transform f_Fx;
    public float Exptimer = 3;
    public int Power = 10;

    float t = 0;
    bool _Once = true;
	// Use this for initialization
	void Start () {
        _Once = true;
	}
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
        if (t >= Exptimer && _Once)
        {
            _Once = false;
            Expro();

            Collider[] colliders = Physics.OverlapSphere(this.transform.position, 3);//爆弾爆発範囲内のオブジェクト
            foreach (Collider hit in colliders)
            {
                if (hit == null)
                {
                    continue;
                }
                if (hit.GetComponent<Rigidbody>() == null)
                {
                    continue;
                }
                if (hit.gameObject.tag != "Player")
                {
                    continue;
                }
                var hitPlayer = hit.GetComponent<Combat>();
                if (hitPlayer!=null)
                {
                    hitPlayer.TakeDamage(Power);
                    //hit.GetComponent<Rigidbody>().AddExplosionForce(200f, this.transform.position, 5, 3.0f);
                }
            }
        }
        
	}
    void Expro()
    {
        var obj = Instantiate(f_Fx, transform.position, Quaternion.identity);
        var go = ((Transform)obj).gameObject;
        Destroy(go, 3);
        Destroy(this.gameObject, 0.1f);
    }
        
}

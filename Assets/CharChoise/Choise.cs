using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Choise : MonoBehaviour {
    public int a; 
	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        switch (Player_Choise.m_choise)
        {
            case 0:
                if (transform.position.x <= 0)
                {
                    //transform.position =new Vector3 (0,-0.94f,1.01f);
                    transform.Translate(Vector3.left * Time.deltaTime*4);
                }
                break;
            case 1:
                if (transform.position.x >= -4)
                {  
                    transform.Translate(Vector3.right * Time.deltaTime*4);
                }
                if (transform.position.x < -4)
                {
                    
                    transform.Translate(Vector3.left * Time.deltaTime * 4);
                }
                break;
            case 2:
                if (transform.position.x >= -8)
                {    
                    transform.Translate(Vector3.right * Time.deltaTime * 4);
                }
                if (transform.position.x < -8)
                {  
                    transform.Translate(Vector3.left * Time.deltaTime * 4);
                }
                break;
            case 3:
                if (transform.position.x >= -12)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * 4);
                }
                break;
        }
            
    }

    public void load_Lobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void mset(int i)
    {
        Player_Choise.m_choise = i;
        //Invoke("load_Lobby", 5f);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    GameObject[] dispObj;
    
    Image Hp,Run;

    Transform m_transform;
    Text _countText;
    bool _Stop=false;
	//キャラクターコンポーネント
	CharacterController m_ch;
    //キャラクターの移動とジャンプ
    private Vector3 moveDirection = Vector3.zero;
    float m_moveSpeed = 3.0f;
    public float m_moveSpeed_run = 6.0f;
    public float m_moveSpeed_wolk = 6.0f;
    public float m_moveSpeed_Back = 3.0f;
    public float jumpSpeed = 8.0F;
    public float jumpPre = 1;
    float jumptimer;
    float runtimer;
    //重力
    public float m_gravity = 20f;

	//生命値
	public float m_life = 5;

	//カメラTransform
	public Transform m_camTransform;

	//カメラ回転
	Vector3 m_camRot;

	//カメラ高さ（キャラの目線）
	float m_camHeight = 0.35f;

    //カメラ回転スピード
    float m_camSpeed = 1.8f;

    //攻撃
    public Transform f_muzzle;//射撃点
    public GameObject Mod_Gun;
    public Transform f_BallBring;//爆弾発射点
    public LayerMask f_Layer;
    public Transform f_Fx;
    public Transform f_Fx01;
    public AudioClip f_audio;
    public float f_shootSpeed = 0.1f;
    float f_shootTimer = 0;
    public float f_power;
    int teamNo=1;


    //LocalPlayerじゃない場合はカメラを止める、じゃないと他のプレーヤーに影響する
    public override void OnStartClient()
    {
        m_camTransform.gameObject.SetActive(false);
    }
    // Use this for initialization
    //Star関数，カメラ初期化，カーソルロック
    public override void OnStartLocalPlayer () {
        GetComponent<MeshRenderer>().material.color = Color.red;
        Debug.Log(dispObj.Length);
        for (int i=0; i<= dispObj.Length-1; i++)
        {
            dispObj[i].layer = 10;
        }

        Hp = GameObject.Find("blood").GetComponent<Image>();
       _countText = GameObject.Find("CD").GetComponent<Text>();
        Run = GameObject.Find("Run").GetComponent<Image>();
        m_transform = this.transform;
		//
		m_ch = GetComponent<CharacterController>();
        //
        //m_camTransform = Camera.main.transform;

        m_camTransform.gameObject.SetActive(true);//自分のカメラを起動
        //
        Vector3 pos = m_transform.position;
		pos.y += m_camHeight;
        
        m_camTransform.position = pos;
		//カメラとキャラ一致する
		m_camTransform.rotation = m_transform.rotation;
		m_camRot = m_camTransform.eulerAngles;
        //カーソルをロック
        //Screen.lockCursor = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (gameObject.name.CompareTo("Player_policewoman(Clone)") == 0)
        {
            Mod_Gun.SetActive(true);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer) {return;}
        if (Input.GetKeyDown(KeyCode.Escape)) { _Stop = !_Stop; }
        if (!_Stop) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (_Stop)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        m_life = this.GetComponent<Combat>().health;
      
        Hp_display(m_life);
        Run_display(runtimer);

        //HPが0だったら，止める
        if (m_life <= 0) { m_life = 0; return; }
        Control2();
        Cam_Contro();
        if (gameObject.name.CompareTo("Player_policewoman(Clone)") == 0) {

            m_Fire();
        }
        else m_Fire01();
        //if(gameObject.name.CompareTo("Player_HeiRen(Clone)") == 0) m_Fire01();
    }
    
    void Cam_Contro()
    {
        //カメラとキャラ一致する
        //マウスの移動
        float rh = Input.GetAxis("Mouse X");
        float rv = Input.GetAxis("Mouse Y");

        //
        m_camRot.x -= rv * m_camSpeed;
        m_camRot.y += rh * m_camSpeed;
        if(m_camRot.x > 75)m_camRot.x = 75;
        if(m_camRot.x <-75)m_camRot.x = -75;

        m_camTransform.eulerAngles = m_camRot;

        //
        Vector3 camrot = m_camTransform.eulerAngles;
        camrot.x = 0;
        camrot.z = 0;
        m_transform.eulerAngles = camrot;

        //
        Vector3 pos = m_transform.position;
        pos.y += m_camHeight;
        
        m_camTransform.position = pos;
    }
    void Control2()
    {
        _countText.text = "" + runtimer;
        if (Input.GetKey(KeyCode.S))
        {
            m_moveSpeed = m_moveSpeed_Back;
        }
        
        else
        {
            m_moveSpeed = m_moveSpeed_wolk;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(runtimer >= 0)
            {
                m_moveSpeed = m_moveSpeed_run;
                runtimer -= Time.deltaTime;
            }  
        }
        else 
        {
            runtimer += Time.deltaTime;
            if (runtimer >= 2f) runtimer = 2f;//走れる時間
        }

        if (m_ch.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= m_moveSpeed;
            jumptimer -= Time.deltaTime;
            if (Input.GetButton("Jump")&&jumptimer<=0)
            {
                moveDirection.y = jumpSpeed;
                jumptimer = jumpPre;
            }

        }
        moveDirection.y -= m_gravity * Time.deltaTime;
        //使用角色控制器提供的Move函数进行移动，它会自动检测碰撞
        m_ch.Move(moveDirection * Time.deltaTime);
    }

	/*void Control()
	{
		//定义3个值控制移动
		float xm = 0, ym = 0, zm = 0;
        if (m_ch.isGrounded)
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                        Debug.Log("jump");
                        ym = jumpSpeed * Time.deltaTime;
                    }
                }
        //自由落体
        ym -= m_gravity * Time.deltaTime;

        
        //前后左右运动
        if (Input.GetKey (KeyCode.W))
		{
			zm += m_moveSpeed*Time.deltaTime;
		}
		if(Input.GetKey (KeyCode.S))
		{
			zm -= m_moveSpeed*Time.deltaTime;
		}
		if(Input.GetKey (KeyCode.A))
		{
			xm -= m_moveSpeed*Time.deltaTime;
		}
		if(Input.GetKey (KeyCode.D))
		{
			xm += m_moveSpeed*Time.deltaTime;
		}

		//使用角色控制器提供的Move函数进行移动，它会自动检测碰撞
		m_ch.Move (m_transform.TransformDirection (new Vector3 (xm, ym, zm)));	
	}*/
 
        //射撃
    void m_Fire() 
    {
        f_shootTimer -= Time.deltaTime;
        if (f_shootTimer < 0) { f_shootTimer = 0; }
        
        //GetComponent<Animator>().SetBool("Fire01", false);
        if (Input.GetMouseButton(0) && f_shootTimer <= 0)
        {
            f_shootTimer = f_shootSpeed;
            GetComponent<Animator>().SetTrigger("Fire01");
            GetComponent<Animator>().PlayInFixedTime("Take 001");

            GetComponent<AudioSource>().PlayOneShot(f_audio);
            animator.PlayInFixedTime("MARMO3");
            //获取当前动画状态
            //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            //if (animator.IsInTransition(1)) {
            //   animator.StartPlayback();
            //}
            
            RaycastHit info;
            //向屏幕中点射
            //bool hit = Physics.Raycast(f_muzzle.position, m_camTransform.TransformDirection(Vector3.forward), out info, 100, f_Layer);
            //向枪口指向方向射
            bool hit = Physics.Raycast(f_muzzle.position, f_muzzle.forward, out info, 100, f_Layer);

            if (hit)
            {
                Debug.Log(info.transform.tag+"/"+info.transform.name);
                if (info.transform.tag.CompareTo("Player") == 0)
                {                   
                    GameObject go = info.transform.gameObject;
                    /*Player player = go.GetComponent<Player>();
                    if (player.teamNo != teamNo)
                    {

                    }
                    else
                    {

                    }*/
                    // Enemy enemy = info.transform .GetComponent<Enemy>();
                    //enemy.OnDamage(f_power);
                    if (go == this.gameObject) return;
                    CmdGiveDamage(go);
                }
                CmdExpro(info.point);
            }
        }
    }
   
    [Command]
    void CmdExpro(Vector3 pos)
    {
        //GameObject go = Instantiate(f_Fx, info.point, info.transform.rotation) as GameObject;
        var obj = Instantiate(f_Fx, pos, Quaternion.identity);
        var go = ((Transform)obj).gameObject;
        NetworkServer.Spawn(go);
        Destroy(go, 3);
    }

    //爆弾投げる
    void m_Fire01()
    {
        f_shootTimer -= Time.deltaTime;
        if (f_shootTimer < 0) { f_shootTimer = 0; }
        //_countText.text = "" + f_shootTimer;
        //GetComponent<Animator>().SetBool("Fire01", false );
        if (Input.GetMouseButton(0) && f_shootTimer <= 0)
        {
            f_shootTimer = f_shootSpeed;
            //GetComponent<Animator>().SetBool("Fire01", true);
            GetComponent<Animator>().PlayInFixedTime("Take 001");

            //GetComponent<AudioSource>().PlayOneShot(f_audio);
            //animator.PlayInFixedTime("MARMO3");
            //获取当前动画状态
            //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            //if (animator.IsInTransition(1)) {
            //   animator.StartPlayback();
            //}
            Cmd_Ball01(f_BallBring.position, f_BallBring.forward);
        }
    }
    [Command]
    void Cmd_Ball01(Vector3 pos01, Vector3 pos02)
    {
        var obj = Instantiate(f_Fx01,pos01, Quaternion.identity);
        var go = ((Transform)obj).gameObject;
        //        go.GetComponent<Rigidbody>().AddForce(pos02 * 5.0f, ForceMode.Impulse);
        go.GetComponent<Rigidbody>().velocity = pos02 * 9.0f;
        NetworkServer.Spawn(go);
        Destroy(go, 5);
    }

    [Command]
    void CmdGiveDamage(GameObject a)
    {
        a.GetComponent<Combat>().TakeDamage(10);
    }
    void teamNo_W(int a)
    {

    }

    void Hp_display(float  H)
    {
        Hp.GetComponent<Image>().fillAmount = H / 100.0f; 
    }

    void Run_display(float R)
    {
        Run.GetComponent<Image>().fillAmount = R / 2.0f;
    }
    //
    void OnDrawGizmo()
	{
		Gizmos.DrawIcon (this.transform.position,"Spawn.tif");

	}
}

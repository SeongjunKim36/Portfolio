using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;
using RootMotion.FinalIK;
using RootMotion.Dynamics;


public class Player : MonoBehaviour
{
    //플레이어가 UI 선택할때 발사 금지
    public static bool DontShoot = false;
    
    //플레이어가 레이저로프를 타고 있을 때 true
    public static bool isHanging = false;

    //플레이어 스탯
    public int hp = 100;

    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Input_Sources lefthand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources righthand = SteamVR_Input_Sources.RightHand;
    //public SteamVR_Action_Boolean grab = SteamVR_Actions.default_GrabGrip;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_Trigger;
    public SteamVR_Action_Pose pose = SteamVR_Actions.default_Pose;
    public SteamVR_Action_Boolean touchpadClick = SteamVR_Actions.default_TouchPadClick;
    public SteamVR_Action_Vector2 touchpadPos = SteamVR_Actions.default_TouchPadPos;
    public SteamVR_Action_Boolean menu = SteamVR_Actions.default_Menu;
    public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;

    //touchpadClick.GetStateDown(lefthand)

    private Rigidbody rb;
    private float MoveSpeed = 300f;
    private float dirX;
    private float dirY;

    private AudioSource sound;
    
    private bool onetime = true;
    
    public GameObject tunnel;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
    }
    void Update()
    {
        Move();

        if(hp <=0 && onetime)
        {
            onetime = false;
            sound.Play();
            StartCoroutine(dead());
        }

        if(menu.GetState(lefthand) && menu.GetState(righthand))
        {
            SceneManager.LoadScene(2);
        }
    }

    IEnumerator dead()
    {
        sound.Play();
        // tunnel.GetComponent<RectTransform>().position = tunnel.GetComponent<RectTransform>().position +  (Vector3.up * -2f) + (Vector3.forward * 2f);
        // tunnel.transform.position = new Vector3(tunnel.transform.position.x, tunnel.transform.position.y-2f, tunnel.transform.position.z+2f);
        tunnel.transform.localPosition = new Vector3(tunnel.transform.localPosition.x, tunnel.transform.localPosition.y-2f, tunnel.transform.localPosition.z+2f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(2);
    }
    void Move()
    {
        dirX = Input.GetAxis("Horizontal");
        dirY = Input.GetAxis("Vertical");
        //Vector3 dir = new Vector3(dirX, 0f, dirY);
        //rb.velocity = new Vector3(dirX, 0f, dirY) * Time.deltaTime * MoveSpeed;
        transform.Translate(new Vector3(dirX,0f,dirY) * Time.deltaTime * 5f);
        //rb.AddForce(dir * MoveSpeed);
    }

    
}
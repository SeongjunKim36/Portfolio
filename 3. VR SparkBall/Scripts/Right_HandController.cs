using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;


public class Right_HandController : Photon.PunBehaviour
{

    public Animator anim;
    public Transform tr;
    // private int default_hand;
    public SteamVR_Input_Sources handType;

    //public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    //public SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand;
    //public SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;


    //액션 - 트리거 버튼(InteractUI)
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;
    //액션 - 트랙패드 클릭(Teleport)
    public SteamVR_Action_Boolean trackPad = SteamVR_Actions.default_Teleport;
    //액션 - 트랙패드 터치 여부(TrackpadTouch)
    //public SteamVR_Action_Boolean trackPadTouch = SteamVR_Actions.default_TouchpadTouch;
    //액션 - 트랙패드 터치 좌표(TrackpadPosition)
    public SteamVR_Action_Vector2 trackPadPosition = SteamVR_Actions.default_TouchPosition;

    // 액션 - 그립 버튼의 잡기(GrabGrip)
    public SteamVR_Action_Boolean grip = SteamVR_Input.GetBooleanAction("GrabGrip");
    // 액션 - 햅틱(Haptic)
    public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;

    private SteamVR_Action_Pose pose = SteamVR_Actions.default_Pose; //컨트롤러 정보

    //public GameObject ObjCollider;



    private GameObject collidingObject; //현재 충돌중인 객체
    private GameObject objectInHand;    //플레이어가 잡은 객체


    public GameObject Shield;
    public GameObject GripBall;
    public Transform ballTr;

    public ActionTest SuperHot;
    private bool isMove = false;
    private Vector3 prevPosition;
    private Vector3 currPostion;
    private Vector3 deltaPosition;

    private TimeManager TM;

    private float targetScale;
    private float lerpSpeed = 2;
    private bool isSuper = false;

    public SteamVR_Behaviour_Pose myPose;
    public GameObject grabPosition;

    public GameObject Kuckle_R;

    //public Vector3 curpos;
    //public Vector3 oldpos;
    //public Vector3 vel;




    //public void GripballMove()
    //{
    //    curpos = transform.position;
    //    vel = curpos - oldpos;

    //    oldpos = curpos;

    //}


    private void Start()
    {
        if (photonView.isMine)
        {
            myPose.enabled = true;
            grabPosition.SetActive(true);
        }
        tr = GetComponent<Transform>();
        collidingObject = null;
        TM = TimeManager.GetInstance();
    }

    //IEnumerator OnOffColl()
    //{
    //    GripBall.SetActive(false);
    //    yield return new WaitForSeconds(1.0f);
    //    GripBall.SetActive(true);

    //}
    void Update()
    {
       
        //GripballMove();
        if (trigger.GetStateDown(handType))
        {

            GrabAnimOn();
            //Kuckle_R.GetComponent<BoxCollider>().enabled = false;

            ObjectColliderOn();
        }
        if (trigger.GetStateUp(handType))
        {

            GrabAnimOff();
            //Kuckle_R.GetComponent<BoxCollider>().enabled = true;

            //StartCoroutine(OnOffColl());
            ObjectColliderOff();
        }


        if(trackPad.GetStateDown(handType))
        {
            SuperHot.enabled = true;
            isSuper = true;
            targetScale = 0.03f;
            lerpSpeed = 10f;
            TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);
            Debug.Log("SuperHot on!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            //TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);
            
        }
        if(isSuper == true)
        {
            
            currPostion = tr.position;
            
            deltaPosition = currPostion - prevPosition;

            prevPosition = currPostion ;

            
                
            //Debug.Log(deltaPosition.x +"얼마나와");
            if(Mathf.Abs(deltaPosition.x)  >= 0.002f || Mathf.Abs(deltaPosition.y)   >= 0.002f || Mathf.Abs(deltaPosition.z)   >= 0.002f)
            {
                //SuperHot.enabled = false;
                targetScale = 1.0f;
                lerpSpeed = 0.8f;
                //Debug.Log("SuperHot off!!!!!!!!!!!!!!!!!!!");
                //Debug.Log(deltaPosition);
                
                //isMove = false;
                //Debug.Log("오나?");
                TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);
                

            }
            else
                {
                    //SuperHot.enabled = true;
                    targetScale = 0.03f;
                    lerpSpeed = 0.5f;
                    Debug.Log("SuperHot On@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                    TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);

                }
                
                if(trackPad.GetStateUp(handType))
                {
                    Debug.Log("here i am");
                        targetScale = 2f;
                        lerpSpeed = 30;
                        isSuper = false;
                        TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);
                        SuperHot.enabled = false;
                }
                
                //TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);
                
                //Debug.Log("오고있니");
            
            
        }
        
        
        if(grip.GetStateDown(handType))
        {
            SceneManager.LoadScene("SuperHot_Test3");
        }
    }
    private void GrabAnimOn()
    {
        anim.SetBool("grip", true);
        anim.SetBool("index", true);
        anim.SetBool("thumb", true);

    }

    private void GrabAnimOff()
    {
        anim.SetBool("grip", false);
        anim.SetBool("index", false);
        anim.SetBool("thumb", false);

    }

    // 오브젝트 홀더(콜라이더)
    private void ObjectColliderOn()
    {
        Debug.Log(MyGameManger.myIndexNumber);
        if (collidingObject && collidingObject.CompareTag("BALL"))
        {
            if(collidingObject.layer == 8 &&  MyGameManger.myIndexNumber == 0)
            {
                
                photonView.RPC("_1PGrabObject", PhotonTargets.All, gameObject.GetComponentInChildren<Transform>().Find("Ball_Position").gameObject.transform.position, gameObject.GetComponentInChildren<Transform>().Find("Ball_Position").gameObject.transform.rotation, MyGameManger.myIndexNumber);
                //GrabObject();
            }
            else if(collidingObject.layer == 9 &&  MyGameManger.myIndexNumber == 1)
            {
                photonView.RPC("_2PGrabObject", PhotonTargets.All,gameObject.GetComponentInChildren<Transform>().Find("Ball_Position").gameObject.transform.position, gameObject.GetComponentInChildren<Transform>().Find("Ball_Position").gameObject.transform.rotation, MyGameManger.myIndexNumber);
            }

            
        }
    }
    private void ObjectColliderOff()
    {
        Debug.Log(MyGameManger.myIndexNumber);

        if (collidingObject)
        {
            //ReleaseObject(objectInHand.transform.position, objectInHand.transform.rotation);
            if(collidingObject.layer == 8 &&  MyGameManger.myIndexNumber == 0)
            {
                Vector3 PoseGetVel = pose.GetVelocity(handType) * 15.0f;
                Vector3 PoseGetAngularVel = pose.GetAngularVelocity(handType) * 15.0f;

                photonView.RPC("_1PReleaseObject", PhotonTargets.All, 
                                gameObject.GetComponentInChildren<Transform>().Find("Ball_Position").gameObject.transform.position, 
                                gameObject.GetComponentInChildren<Transform>().Find("Ball_Position").gameObject.transform.rotation, 
                                MyGameManger.myIndexNumber,
                                PoseGetVel,
                                PoseGetAngularVel);
                //GrabObject();
            }
            else if(collidingObject.layer == 9 &&  MyGameManger.myIndexNumber == 1)
            {
                Vector3 PoseGetVel = pose.GetVelocity(handType) * 15.0f;
                Vector3 PoseGetAngularVel = pose.GetAngularVelocity(handType) * 15.0f;

                photonView.RPC("_2PReleaseObject", PhotonTargets.All,
                                                gameObject.GetComponentInChildren<Transform>().Find("Ball_Position").gameObject.transform.position,
                                                gameObject.GetComponentInChildren<Transform>().Find("Ball_Position").gameObject.transform.rotation,
                                                MyGameManger.myIndexNumber,
                                                PoseGetVel,
                                                PoseGetAngularVel);
            }

        }

    }

    void OnTriggerEnter(Collider other) //충돌감지 콜백함수 
    {

        SetCollidingObject(other);

    }
    //충돌 중
    //void OnTriggerStay(Collider other)
    //{
    //    SetCollidingObject(other);
    //}
    //충돌이 끝날 때
    void OnTriggerExit(Collider other) //충돌 상태에서 빠져 나왔을 때, 1회 자동 호출
    {

        collidingObject = null;
    }

    //충돌중인 객체로 설정
    void SetCollidingObject(Collider col)
    {
        if (collidingObject != null)  //collidingObject를 2개 잡지 않기 위해 || rigidbody가 없는 object는 충돌객체로 인식x
        {
            return;
        }

        collidingObject = col.gameObject;
    }

    //객체 잡기
    [PunRPC]
    void _1PGrabObject(Vector3 _grabPos, Quaternion _grabRot, int _id)
    {
        Debug.Log("1PGrab");

        GameObject ball1InHand = GameObject.Find("1PlayerBall");
        GameObject ball2InHand = GameObject.Find("2PlayerBall");
        //Debug.Log(ball1InHand.transform.GetChild(5).gameObject.name);     

        ball1InHand.transform.position = _grabPos;
        ball1InHand.transform.rotation = _grabRot;
        ball1InHand.transform.SetParent(this.gameObject.transform);
        ball1InHand.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.transform.GetChild(5).gameObject.SetActive(true); 
            //gameObject.GetComponentInChildren<Transform>(true).gameObject.SetActive(true);
        
            
        
        
        //objectInHand = collidingObject; //잡은 객체로 설정
                                        //collidingObject = null; //충돌 객체 해제
        
        
        


        //objectInHand.transform.position = ballTr.position;

        //var joint = AddFixedJoint();
        //joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    [PunRPC]
    void _2PGrabObject(Vector3 _grabPos, Quaternion _grabRot, int _id)
    {
        Debug.Log("2PGrab");
        GameObject ball1InHand = GameObject.Find("1PlayerBall");
        GameObject ball2InHand = GameObject.Find("2PlayerBall");

        ball2InHand.transform.position = _grabPos;
        ball2InHand.transform.rotation = _grabRot;
        ball2InHand.transform.SetParent(this.gameObject.transform);
        ball2InHand.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.transform.GetChild(5).gameObject.SetActive(true);
    }
    //객체를 놓음
    //pose.GetVelocity() = 컨트롤러의 속도
    //pose.GetAngularVelocity() = 컨트롤러의 각속도

    [PunRPC]
    private void _1PReleaseObject(Vector3 _grabPos, Quaternion _grabRot, int _id, Vector3 _GetVel, Vector3 _GetAngularVel)
    {
        GameObject ball1InHand = GameObject.Find("1PlayerBall");
        GameObject ball2InHand = GameObject.Find("2PlayerBall");
        
        //SteamVR_Action_Pose handPose = SteamVR_Actions.default_Pose;
        Debug.Log("떨어져라");

        //GetComponent<FixedJoint>().connectedBody = null;
        
        Debug.Log("@@@@@@@@@@@@!");
        gameObject.transform.GetChild(5).gameObject.SetActive(false);

        ball1InHand.transform.SetParent(null);
        ball1InHand.transform.position = _grabPos;
        ball1InHand.transform.rotation = _grabRot;


        ball1InHand.GetComponent<Rigidbody>().isKinematic = false;

        ball1InHand.GetComponent<Rigidbody>().velocity = _GetVel;
        ball1InHand.GetComponent<Rigidbody>().angularVelocity = _GetAngularVel;
            //objectInHand = null;
        
        
    }

    [PunRPC]
    private void _2PReleaseObject(Vector3 _grabPos, Quaternion _grabRot, int _id, Vector3 _GetVel, Vector3 _GetAngularVel)
    {
        GameObject ball1InHand = GameObject.Find("1PlayerBall");
        GameObject ball2InHand = GameObject.Find("2PlayerBall");
        //SteamVR_Action_Pose handPose = SteamVR_Actions.default_Pose;
        gameObject.transform.GetChild(5).gameObject.SetActive(false);

        ball2InHand.transform.SetParent(null);
        ball2InHand.transform.position = _grabPos;
        ball2InHand.transform.rotation = _grabRot;


        ball2InHand.GetComponent<Rigidbody>().isKinematic = false;
        _GetVel.z = _GetVel.z * -1;
        _GetAngularVel.z = _GetAngularVel.z * -1;
        ball2InHand.GetComponent<Rigidbody>().velocity = _GetVel;
        ball2InHand.GetComponent<Rigidbody>().angularVelocity = _GetAngularVel;
        //objectInHand = null;
    }





}

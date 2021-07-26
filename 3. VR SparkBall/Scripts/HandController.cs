using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Kandooz.Burger;

public class HandController : MonoBehaviour
{


    public Animator anim;
    public Transform tr;
    public SteamVR_Input_Sources handType;

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


    private GameObject collidingObject; //현재 충돌중인 객체
    private GameObject objectInHand;    //플레이어가 잡은 객체
    private FixedJoint fx;


    public GameObject Shield;
    public GameObject GripBall;
    public Transform ballTr;
    
    private void Start()
    {
        tr = GetComponent<Transform>();
        fx = gameObject.GetComponent<FixedJoint>();
        collidingObject = null;
    }


    void Update()
    {

        if (trigger.GetStateDown(handType))
        {

            GrabAnimOn();
            ObjectColliderOn();
        }
        if (trigger.GetStateUp(handType))
        {

            GrabAnimOff();
            ObjectColliderOff();
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
        
        if (collidingObject && collidingObject.CompareTag("BALL"))
        {
            if(collidingObject.gameObject.layer == LayerMask.NameToLayer("1ST"))
            {
                GrabObject();

            }
        }
    }
    private void ObjectColliderOff()
    {
        
        if (collidingObject)
        {
            ReleaseObject();

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
    void GrabObject()
    {
       
            objectInHand = collidingObject; //잡은 객체로 설정
                                            //collidingObject = null; //충돌 객체 해제

            objectInHand.transform.SetParent(tr);
            objectInHand.GetComponent<Rigidbody>().isKinematic = true;
            Shield.SetActive(true);
      
    }
    //FixedJoint = 객체들을 하나로 묶어 고정 시킴
    //breakForce = 조인트가 제거되기 위해 필요한 힘의 크기
    //breakTorque = 조인트가 제거되기 위해 필요한 토크

    private FixedJoint AddFixedJoint()
    {
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    //객체를 놓음
    //pose.GetVelocity() = 컨트롤러의 속도
    //pose.GetAngularVelocity() = 컨트롤러의 각속도
    private void ReleaseObject()
    {
        objectInHand.transform.SetParent(null);
        objectInHand.GetComponent<Rigidbody>().isKinematic = false;

        objectInHand.GetComponent<Rigidbody>().velocity = pose.GetVelocity(handType) * 15.0f;
        objectInHand.GetComponent<Rigidbody>().angularVelocity = pose.GetAngularVelocity(handType) * 15.0f;
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;


public class HandControllerSuperhotTest : MonoBehaviour
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

    

    public ActionTest SuperHot;
    private bool isMove = false;
    private Vector3 prevPosition;
    private Vector3 currPostion;
    private Vector3 deltaPosition;

    private TimeManager TM;

    private float targetScale;
    private float lerpSpeed = 2;
    private bool isSuper = false;
    

    private void Start()
    {
        tr = GetComponent<Transform>();
        
        TM = TimeManager.GetInstance();
        //anim = GetComponent<Animator>();
        //Animscript = GetComponent<AnimationController>();
        

    }

    void Update()
    {


        if (trigger.GetStateDown(handType))
        {

            anim.SetBool("grip",  true);
            anim.SetBool("index", true);
            anim.SetBool("thumb", true);

            //Animscript.Grip = true;
            //Animscript.Index = true;
            // Animscript.Thumb = true;

        }
        if (trigger.GetStateUp(handType))
        {
            anim.SetBool("grip", false);
            anim.SetBool("index", false);
            anim.SetBool("thumb", false);
            // Animscript.Grip = false;
            // Animscript.Index = false;
            // Animscript.Thumb = false;
        }

        

        

        //prevPosition = tr.localPosition;

        //Debug.Log(prevPosition);

        // if(trackPad.GetStateDown(handType))
        // {
        //     SuperHot.enabled = true;
        //     Debug.Log("SuperHot on");
        // }
        // else if( Mathf.Abs(deltaPosition.y)  > 1f || Mathf.Abs(deltaPosition.z)  > 1f)
        // {
        //     SuperHot.enabled = false;
        //     targetScale = 0.5f;
        //     lerpSpeed = 10;
        //     Debug.Log("SuperHot off!!!!!!!!!!!!!!!!!!!");

        // }
        // else 
        // {
        //     if(trackPad.GetStateUp(handType))
        //     {
        
        //         SuperHot.enabled = false;
        //         Debug.Log("SuperHot off@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        
            
        //         // targetScale = 1;
        //         // lerpSpeed = 10;
        //     }
        //     else
        //     {
        //         SuperHot.enabled = true;
        //         targetScale = 0;
        //         lerpSpeed = 4;
        //         Debug.Log("SuperHot On@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        //     }
            
        //     Debug.Log("오고있니");
        // }
        // TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);
        
        if(trackPad.GetStateDown(handType))
        {
            SuperHot.enabled = true;
            isSuper = true;
            targetScale = 0.05f;
            lerpSpeed = 0.8f;
            //Debug.Log("SuperHot on!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
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
                //TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);

            }
            
                else if(trackPad.GetStateUp(handType))
                {
                        targetScale = 1f;
                        lerpSpeed = 30;
                        isSuper = false;
                        //TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);
                        SuperHot.enabled = false;
                }
                else
                {
                    //SuperHot.enabled = true;
                    targetScale = 0.05f;
                    lerpSpeed = 0.5f;
                    //Debug.Log("SuperHot On@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                    //TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);

                }
                
                //Debug.Log("오고있니");
            
            
        }
        TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);
        
        

        

        

        if(grip.GetStateDown(handType))
        {
            SceneManager.LoadScene("SuperHot_Test");
        }

        

        //Debug.Log(Mathf.Abs(deltaPosition.y));

        // if(pe )
        // {
            
        //     targetScale = 0.5f;
        //     lerpSpeed = 10;
        // }
        // else 
        // {
        //     if(Input.anyKey)
        //     {
        //         targetScale = 1;
        //         lerpSpeed = 10;
        //     }
        //     else
        //     {
        //         targetScale = 0;
        //         lerpSpeed = 4;
        //     }

        // 
        // }




        
    }

    void GetCurrentPos()
    {
        currPostion = tr.position;
        Debug.Log(currPostion+"curr");
        
    }


}



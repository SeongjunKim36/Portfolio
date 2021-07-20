using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Anim_Right : MonoBehaviour
{
    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Input_Sources lefthand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources righthand = SteamVR_Input_Sources.RightHand;

    public SteamVR_Action_Boolean grab = SteamVR_Actions.default_GrabGrip;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_Trigger;
    public SteamVR_Action_Pose pose = SteamVR_Actions.default_Pose;
    public SteamVR_Action_Boolean touchpadClick = SteamVR_Actions.default_TouchPadClick;
    public SteamVR_Action_Vector2 touchpadPos = SteamVR_Actions.default_TouchPadPos;
    public SteamVR_Action_Boolean menu = SteamVR_Actions.default_Menu;
    public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;

        public Animator anim;
    public GameObject leftHand;
    public GameObject rightHand;
    




    void Start()
    {
        
        anim = GetComponent<Animator>();
        leftHand = GetComponent<GameObject>();
        rightHand = GetComponent<GameObject>();
    }

    private void Update()
    {

        //GetStateDown: Once when Pressed, GetState: While Presing, GetStateUp: Once when Released



        if (trigger.GetStateDown(righthand))
        {
            //Debug.Log("Trigger" + righthand);
            anim.SetTrigger("Shoot");
        }

    }

}

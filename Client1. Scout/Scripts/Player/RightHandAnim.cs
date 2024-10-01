using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RightHandAnim : MonoBehaviour
{
    public Animator anim;
    public Transform tr;
    private int default_hand;


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


    void Start()
    {
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        default_hand = Animator.StringToHash("PlayerLeftHand");
    }

    void Update()
    {
        if (GetTeleportDown())
        {
            Debug.Log("Teleport" + hand);
            anim.SetBool("IsHoldingGun", true);
        }
        
        if (GetGrab())
        {
            Debug.Log("Grab" + hand);
            anim.SetBool("IsGrabbing", true);
        }
     /*   Debug.Log(trigger.GetState(lefthand));
        Debug.Log(trigger.GetState(righthand));
        Debug.Log(grab.GetState(lefthand));
        Debug.Log(grab.GetState(righthand)); */

        if (trigger.GetStateDown(lefthand))
        {
            Debug.Log("Trigger" + hand);
            anim.SetTrigger("Shoot");
        }
    }

    public bool GetTeleportDown()
    {
        return touchpadClick.GetStateDown(lefthand);
    }
    public bool GetGrab()
    {
        return grab.GetState(lefthand);
    }
    public bool GetTrigger()
    {
        return trigger.GetState(lefthand);
    }
}
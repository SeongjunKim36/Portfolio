using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LaserRopeController : MonoBehaviour
{
    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Input_Sources lefthand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources righthand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Pose pose = SteamVR_Actions.default_Pose;
    public SteamVR_Action_Boolean touchpadClick = SteamVR_Actions.default_TouchPadClick;
    public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;
    public SteamVR_Action_Boolean grab = SteamVR_Actions.default_GrabGrip;
    public SteamVR_Action_Boolean Trigger = SteamVR_Actions.default_Trigger;

    //public GameObject HitEffect;
    public GameObject laser;
    public GameObject player;
    private GameObject fixture;
    

    private float maxLaserLength = 100.0f;
    private RaycastHit hit;
    private Rigidbody playerRigid;
    private float pullForce = 10.0f;


    void Start()
    {
        playerRigid = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(pose.GetLocalRotation(righthand) == null)
        {
            return;
        }
        transform.rotation = pose.GetLocalRotation(righthand);
        ShootLaserRope();
        PullRope();
        
    }
    
    void ShootLaserRope()
    {
        if(grab.GetStateDown(righthand))
        {
            laser.SetActive(true);
            if(Physics.Raycast(transform.position, transform.forward, out hit, maxLaserLength))
            {
                
                fixture = hit.collider.gameObject;
                Debug.Log(fixture.name);
                if (fixture.GetComponent<Rigidbody>() != null)
                {
                    fixture.AddComponent<CharacterJoint>();
                    fixture.GetComponent<CharacterJoint>().connectedBody = player.GetComponent<Rigidbody>();
                }
            }
     
            

        }
        else if(grab.GetStateUp(righthand))
        {
            laser.SetActive(false);
        }
    }

    void PullRope()
    {
        if(Trigger.GetState(righthand))
        {
            Debug.Log("PowerUp");
            playerRigid.velocity = player.transform.forward * pullForce;
            Debug.Log(playerRigid.velocity + "vel");
        }
    }
}

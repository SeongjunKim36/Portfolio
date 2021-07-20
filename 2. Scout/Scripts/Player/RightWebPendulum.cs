using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using RootMotion.FinalIK;
using RootMotion.Dynamics;

public class RightWebPendulum : MonoBehaviour
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

    public LineRenderer line;
    public VRIK vrik;
    private Vector3 target;
    private GameObject fixture;
    public GameObject playerRig;
    public GameObject swingingAnchor;
    public GameObject playerAnchor;
    public GameObject rotateStabilizer;
    public GameObject rightCrosshair;
    public Rigidbody rb;
	public Vector3 velocity = Vector3.up;

	public PuppetMaster puppetMaster; 

	private bool jumpFlag;
    private bool hanging;
    private Vector3 currAnchorVel;
    private Vector3 prevAnchorVel;
    private Vector3 deltaAnchorVel;
    private Vector3 currPlayerVel;
    private Vector3 prevPlayerVel;
    private Vector3 deltaPlayerVel;

    private int wallLayer;
    private int waterLayer;

    private Vector3 AnchorPreVel;
    private Vector3 PlayerPreVel;
    private bool isXPositive;
    private bool isYPositive;
    private bool isZPositive;
    private float xCutValue;
    private float yCutValue;
    private float zCutValue;
    private bool collectValue = false;
    public AudioSource player_audio;
    public PlayerSound playerSound;
    public Pendulum pendulum;


    
    
    
    void Start()
    {
        wallLayer = 1<<LayerMask.NameToLayer("WALL");
        waterLayer = 1<<LayerMask.NameToLayer("Water");
        
    }

    void FixedUpdate()
    {        
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 1000, wallLayer | waterLayer))
        {
            rightCrosshair.transform.position = hit.point;
            rightCrosshair.transform.rotation = Quaternion.FromToRotation(hit.point, hit.normal);
            
            PlayerPreVel = playerRig.GetComponent<Rigidbody>().velocity;
            

            if(grab.GetStateDown(righthand))
            {
                Player.isHanging = true;
                transform.parent.Find("RightHandGun").GetComponent<Gun_Right>().DontShoot = true;
                transform.parent.Find("RightPortalGun").GetComponent<PortalGun_Right>().DontShoot = true;
                //player_audio.clip =  playerSound.player_rope;
                //player_audio.Play();

                if (hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 4)
                {
                    line.enabled = true;
                    line.SetPosition(0, transform.position);
                    target = hit.point;
                    
                    line.SetPosition(1, target);
                    line.material.mainTextureOffset = Vector2.zero;
                    fixture = hit.collider.gameObject;
                    swingingAnchor.transform.position = hit.point;
                    
                    
                        
                    pendulum.Pivot = swingingAnchor;
                    pendulum.Bob = playerRig;
                    pendulum.enabled = true;
                        
                    

                    
                }
            }

            
        }

        if(grab.GetState(righthand) && line.enabled == true)
        {
            line.SetPosition(0, transform.position);
            line.material.mainTextureOffset = new Vector2(line.material.mainTextureOffset.x + Random.Range(0.01f,-0.5f), 0.0f);

            
            
            if(trigger.GetState(righthand))
            {
               
                line.SetPosition(0, transform.position);
                line.material.mainTextureOffset = new Vector2(line.material.mainTextureOffset.x + Random.Range(0.01f,-0.5f), 0.0f);

                
                pendulum.ropeLength += -0.1f;
                if( pendulum.ropeLength <= 0)
                {
                    pendulum.ropeLength = 0;
                }
                

            }
            else if(trigger.GetStateUp(righthand))
            {
                pendulum.enabled = false;
                

            }
        }
        else if(grab.GetStateUp(righthand))
        {
            pendulum.enabled = false;
            line.enabled = false;
            Player.isHanging = false;
            transform.parent.Find("RightHandGun").GetComponent<Gun_Right>().DontShoot = false;
            transform.parent.Find("RightPortalGun").GetComponent<PortalGun_Right>().DontShoot = false;

            
            
        }
    }
    // void FixedUpdate() 
    // {
    //     //AddForce to PlayerRig and muscles
	// 	if (jumpFlag) 
    //     {
			
    //         playerRig.GetComponent<Rigidbody>().AddForce((target - transform.position).normalized * 15f, ForceMode.Force );
    //         playerRig.GetComponent<Rigidbody>().AddForce(transform.up * 2);

	// 		//set velocities for all the muscles
	// 		foreach (Muscle m in puppetMaster.muscles) 
    //         {
	// 			m.rigidbody.AddForce((target - transform.position).normalized * 15f, ForceMode.Force);
    //             m.rigidbody.AddForce(transform.up * 2);

			    
	// 	    }
    //         jumpFlag = false;
	//     }

    // }

}
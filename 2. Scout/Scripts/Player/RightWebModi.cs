using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using RootMotion.FinalIK;
using RootMotion.Dynamics;

public class RightWebModi : MonoBehaviour
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


    
    
    
    void Start()
    {
        wallLayer = 1<<LayerMask.NameToLayer("WALL");
        waterLayer = 1<<LayerMask.NameToLayer("Water");
        
    }

    void FixedUpdate()
    {
        

        currPlayerVel = playerRig.GetComponent<Rigidbody>().velocity;
        deltaPlayerVel = currPlayerVel - prevPlayerVel;
        prevPlayerVel = currPlayerVel;

        

        //Debug.Log(deltaPlayerVel +"player");
        //Debug.Log(deltaAnchorVel + "Anchor");
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 1000, wallLayer | waterLayer))
        {
            rightCrosshair.transform.position = hit.point;
            rightCrosshair.transform.rotation = Quaternion.FromToRotation(hit.point, hit.normal);
            //Debug.Log(hit.collider.gameObject.name);
            
            PlayerPreVel = playerRig.GetComponent<Rigidbody>().velocity;
            

            if(grab.GetStateDown(righthand))
            {
                Player.isHanging = true;
                transform.parent.Find("RightHandGun").GetComponent<Gun_Right>().DontShoot = true;
                transform.parent.Find("RightPortalGun").GetComponent<PortalGun_Right>().DontShoot = true;
                player_audio.clip =  playerSound.player_rope;
                player_audio.Play();

                if (hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 4)
                {
                    // playerRig.GetComponent<Rigidbody>().velocity = PlayerPreVel;//.normalized * 5.0f;
                    // foreach (Muscle m in puppetMaster.muscles) 
                    // {
                    //     m.rigidbody.velocity = PlayerPreVel;//.normalized * 5.0f;
                    // }
                    
                    
                    line.enabled = true;
                    line.SetPosition(0, transform.position);
                    target = hit.point;
                    
                    line.SetPosition(1, target);
                    line.material.mainTextureOffset = Vector2.zero;
                    fixture = hit.collider.gameObject;
                    swingingAnchor.transform.position = hit.point;
                    playerAnchor.GetComponent<Rigidbody>().isKinematic = false;
                    
                    if (swingingAnchor.GetComponent<Rigidbody>() != null)
                    {
                        swingingAnchor.AddComponent<SpringJoint>();
                        swingingAnchor.GetComponent<SpringJoint>().connectedBody = playerAnchor.GetComponent<Rigidbody>();
                        rotateStabilizer.GetComponent<FixedJoint>().connectedBody = playerRig.GetComponent<Rigidbody>();
                        swingingAnchor.GetComponent<SpringJoint>().damper = 300.0f;
                        swingingAnchor.GetComponent<SpringJoint>().spring = 500.0f;
                        swingingAnchor.GetComponent<SpringJoint>().autoConfigureConnectedAnchor = true;
                        //vrik.solver.locomotion.weight = 0;
                        rotateStabilizer.GetComponent<Rigidbody>().isKinematic = true;
                        
                        Debug.Log(PlayerPreVel);
                        playerAnchor.GetComponent<Rigidbody>().velocity = PlayerPreVel;//.normalized * 5.0f;
                        // foreach (Muscle m in puppetMaster.muscles) 
                        // {
                        //     m.rigidbody.velocity = PlayerPreVel;//.normalized * 5.0f;
                        // }

                        currAnchorVel = playerAnchor.GetComponent<Rigidbody>().velocity;
                        deltaAnchorVel = currAnchorVel - prevAnchorVel;
                        prevAnchorVel = currAnchorVel;
                        //playerAnchor.GetComponent<Rigidbody>().MovePosition(playerRig.transform.position);
                    }

                    
                }
            }

            
        }

        if(grab.GetState(righthand) && line.enabled == true)
        {
            line.SetPosition(0, transform.position);
            line.material.mainTextureOffset = new Vector2(line.material.mainTextureOffset.x + Random.Range(0.01f,-0.5f), 0.0f);
            //vrik.solver.locomotion.weight = 0;

            AnchorPreVel = playerAnchor.GetComponent<Rigidbody>().velocity;

            
            
            if(trigger.GetStateDown(righthand))
            {
                collectValue = true;
            }
            
            else if(trigger.GetState(righthand))
            {
               
                line.SetPosition(0, transform.position);
                line.material.mainTextureOffset = new Vector2(line.material.mainTextureOffset.x + Random.Range(0.01f,-0.5f), 0.0f);

                //collectValue = true;
                //Debug.Log(collectValue);
                if(collectValue)
                {
                    float xTemp = swingingAnchor.GetComponent<SpringJoint>().connectedAnchor.x;
                    float yTemp = swingingAnchor.GetComponent<SpringJoint>().connectedAnchor.y;
                    float zTemp = swingingAnchor.GetComponent<SpringJoint>().connectedAnchor.z;
                    float minValue = Mathf.Abs(Mathf.Min(xTemp, yTemp, zTemp));
                    
                    xCutValue = xTemp / minValue;
                    yCutValue = yTemp / minValue;
                    zCutValue = zTemp / minValue;

                    if(xTemp >= 0)
                    {
                        isXPositive = true;
                    }
                    else if(xTemp < 0)
                    {
                        isXPositive = false;
                    }

                    if(yTemp >= 0)
                    {
                        isYPositive = true;
                    }               
                    else if(yTemp < 0)
                    {
                        isYPositive = false;
                    } 

                    if(zTemp >= 0)
                    {
                        isZPositive = true;
                    }               
                    else if(zTemp < 0)
                    {
                        isZPositive = false;
                    } 

                    collectValue = false;
                }

                
                
                swingingAnchor.GetComponent<SpringJoint>().autoConfigureConnectedAnchor = false;
                float x = swingingAnchor.GetComponent<SpringJoint>().connectedAnchor.x;
                float y = swingingAnchor.GetComponent<SpringJoint>().connectedAnchor.y;
                float z = swingingAnchor.GetComponent<SpringJoint>().connectedAnchor.z;


                if(isXPositive)
                {
                    if(x <= 0)
                    {
                        x = 0;
                    }
                }
                else if(isXPositive == false)
                {
                    if(x >= 0)
                    {
                        x = 0;
                    }
                }

                if(isYPositive)
                {
                    if(y <= 0)
                    {
                        y = 0;
                    }
                }
                else if(isYPositive == false)
                {
                    if(y >= 0)
                    {
                        y = 0;
                    }
                }

                if(isZPositive)
                {
                    if(z <= 0)
                    {
                        z = 0;
                    }
                }
                else if(isZPositive == false)
                {
                    if(z >= 0)
                    {
                        z = 0;
                    }
                }

                x += - (xCutValue * 0.01f);
                y += - (yCutValue * 0.01f);
                z += - (zCutValue * 0.01f);
                
                swingingAnchor.GetComponent<SpringJoint>().connectedAnchor = new Vector3(x,y,z);
                
                

            }
            else if(trigger.GetStateUp(righthand))
            {
                playerAnchor.GetComponent<Rigidbody>().isKinematic = false;
                
                if(rotateStabilizer.GetComponent<FixedJoint>().connectedBody == null)
                {
                    rotateStabilizer.GetComponent<FixedJoint>().connectedBody = playerRig.GetComponent<Rigidbody>();
                }
                //playerAnchor.GetComponent<Rigidbody>().MovePosition(playerRig.transform.position);
                swingingAnchor.GetComponent<SpringJoint>().connectedBody = playerAnchor.GetComponent<Rigidbody>();
                
                // playerRig.GetComponent<Rigidbody>().velocity = deltaPlayerVel.normalized * 10.0f;
                // foreach (Muscle m in puppetMaster.muscles) 
                // {
                //     m.rigidbody.velocity = deltaPlayerVel.normalized * 10.0f;
                // }
                playerAnchor.transform.position = playerRig.transform.position;
                playerAnchor.transform.rotation = playerRig.transform.rotation;
                

            }
        }
        else if(grab.GetStateUp(righthand))
        {
            Destroy(swingingAnchor.GetComponent<SpringJoint>());
            if(rotateStabilizer.GetComponent<FixedJoint>().connectedBody != null)
            {
                rotateStabilizer.GetComponent<FixedJoint>().connectedBody = null;
            }
            line.enabled = false;
            Player.isHanging = false;
            transform.parent.Find("RightHandGun").GetComponent<Gun_Right>().DontShoot = false;
            transform.parent.Find("RightPortalGun").GetComponent<PortalGun_Right>().DontShoot = false;
            //playerAnchor.GetComponent<Rigidbody>().MovePosition(playerRig.transform.position);
            playerAnchor.transform.position = playerRig.transform.position;
            playerAnchor.transform.rotation = playerRig.transform.rotation;
            
            playerAnchor.GetComponent<Rigidbody>().isKinematic = true;
            
            playerRig.GetComponent<Rigidbody>().velocity = AnchorPreVel;//.normalized * 5.0f;
            foreach (Muscle m in puppetMaster.muscles) 
            {
                m.rigidbody.velocity = AnchorPreVel;//.normalized * 5.0f;
            }
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
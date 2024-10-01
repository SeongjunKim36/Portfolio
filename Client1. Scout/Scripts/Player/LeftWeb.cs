using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using RootMotion.FinalIK;
using RootMotion.Dynamics;

// 좌측 로프 제어
public class LeftWeb : MonoBehaviour
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
    public GameObject leftCrosshair;
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
    private Vector3 prevPlayerVelocity;
    private Vector3 prevAnchorVelocity;
    private bool velocityKeeperOn = true;

    private AudioSource[] player_audio;
    public PlayerSound playerSound;
    
    void Start()
    {
        wallLayer = 1<<LayerMask.NameToLayer("WALL");
        waterLayer = 1<<LayerMask.NameToLayer("Water");
        player_audio = gameObject.GetComponents<AudioSource>();
    }

    void Update()
    {       
        currPlayerVel = playerRig.GetComponent<Rigidbody>().velocity;
        deltaPlayerVel = currPlayerVel - prevPlayerVel;
        prevPlayerVel = currPlayerVel;
        
        RaycastHit hit;
	
	//벽이나 바닥에 레이캐스트 Hit시 
        if(Physics.Raycast(transform.position, transform.up, out hit,30, wallLayer | waterLayer))
        {
            
            leftCrosshair.transform.position = hit.point;
            leftCrosshair.transform.rotation = Quaternion.FromToRotation(hit.point, hit.normal);
            prevPlayerVelocity = playerRig.GetComponent<Rigidbody>().velocity;
            
	    // 로프 
            if(grab.GetStateDown(lefthand))
            {
                Player.isHanging = true;
		
		// 로프 사용 시 총 사용 블락
                transform.parent.Find("LeftHandGun").GetComponent<Gun_Left>().DontShoot = true;
                transform.parent.Find("LeftPortalGun").GetComponent<PortalGun_Left>().DontShoot = true;

                if (hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 4)
                { 
		    // 라인랜더러 활성화
                    line.enabled = true;
                    line.SetPosition(0, transform.position);
                    target = hit.point;
                    
                    line.SetPosition(1, target);
                    line.material.mainTextureOffset = Vector2.zero;
                    fixture = hit.collider.gameObject;
                    swingingAnchor.transform.position = hit.point;
                    playerAnchor.transform.position = playerRig.transform.position;
                    playerAnchor.transform.rotation = playerRig.transform.rotation;
                    
		    // 자연스럽게 매달린 플레이어를 표현하기 위해 Spring Joint와 Fixed Joint 사용 및 수치 조절
                    if (swingingAnchor.GetComponent<Rigidbody>() != null)
                    {
                        player_audio[0].clip =  playerSound.player_rope;
                        player_audio[0].Play();
                        swingingAnchor.AddComponent<SpringJoint>();
                        swingingAnchor.GetComponent<SpringJoint>().connectedBody = playerAnchor.GetComponent<Rigidbody>();
                        rotateStabilizer.GetComponent<FixedJoint>().connectedBody = playerRig.GetComponent<Rigidbody>();
                        swingingAnchor.GetComponent<SpringJoint>().damper = 300.0f;
                        swingingAnchor.GetComponent<SpringJoint>().spring = 500.0f;
                        rotateStabilizer.GetComponent<Rigidbody>().isKinematic = true;
                        playerAnchor.GetComponent<Rigidbody>().isKinematic = false;
                        playerAnchor.GetComponent<Rigidbody>().velocity = prevPlayerVelocity;
                    }
                    else if (hit.collider.gameObject.layer != 9 || hit.collider.gameObject.layer != 4)
                    {
                        player_audio[1].clip =  playerSound.player_ropeFail;
                        player_audio[1].Play();
                    }
                }
            }

            
        }

	
        if(grab.GetState(lefthand) && line.enabled == true)
        {
            line.SetPosition(0, transform.position);
            line.material.mainTextureOffset = new Vector2(line.material.mainTextureOffset.x + Random.Range(0.01f,-0.5f), 0.0f);

            prevAnchorVelocity = playerAnchor.GetComponent<Rigidbody>().velocity;
            
            // 로프를 계속 쥐고 있을때 
            if(trigger.GetState(lefthand))
            {
                
                line.SetPosition(0, transform.position);
                line.material.mainTextureOffset = new Vector2(line.material.mainTextureOffset.x + Random.Range(0.01f,-0.5f), 0.0f);
                if(swingingAnchor.GetComponent<SpringJoint>().connectedBody != null)
                {
                    swingingAnchor.GetComponent<SpringJoint>().connectedBody = null;                    
                }
                if(rotateStabilizer.GetComponent<FixedJoint>().connectedBody != null)
                {
                    rotateStabilizer.GetComponent<FixedJoint>().connectedBody = null;     
                }
		
                playerAnchor.GetComponent<Rigidbody>().isKinematic = true;

                if(velocityKeeperOn)
                {
                    playerRig.GetComponent<Rigidbody>().velocity = prevAnchorVelocity;
                    foreach (Muscle m in puppetMaster.muscles) 
                    {
                        m.rigidbody.velocity = prevAnchorVelocity;
                    }
                    velocityKeeperOn = false;
                }
                 
                jumpFlag = true;

                prevPlayerVelocity = playerRig.GetComponent<Rigidbody>().velocity;

            }
	    
            else if(trigger.GetStateUp(lefthand))
            {
                playerAnchor.transform.position = playerRig.transform.position;
                playerAnchor.transform.rotation = playerRig.transform.rotation;
                playerAnchor.GetComponent<Rigidbody>().isKinematic = false;
                
                
                if(rotateStabilizer.GetComponent<FixedJoint>().connectedBody == null)
                {
                    rotateStabilizer.GetComponent<FixedJoint>().connectedBody = playerRig.GetComponent<Rigidbody>();
                }
                
                swingingAnchor.GetComponent<SpringJoint>().connectedBody = playerAnchor.GetComponent<Rigidbody>();
                playerAnchor.GetComponent<Rigidbody>().velocity = prevPlayerVelocity;
                
                velocityKeeperOn = true;
            }
        }
	//로프 놨을 때 물리 구현 
        else if(grab.GetStateUp(lefthand))
        {
            Destroy(swingingAnchor.GetComponent<SpringJoint>());
            if(rotateStabilizer.GetComponent<FixedJoint>().connectedBody != null)
            {
                rotateStabilizer.GetComponent<FixedJoint>().connectedBody = null;
                playerRig.GetComponent<Rigidbody>().velocity = prevAnchorVelocity;
                    foreach (Muscle m in puppetMaster.muscles) 
                    {
                        m.rigidbody.velocity = prevAnchorVelocity;
                    }
            }
            line.enabled = false;
            Player.isHanging = false;
            transform.parent.Find("LeftHandGun").GetComponent<Gun_Left>().DontShoot = false;
            transform.parent.Find("LeftPortalGun").GetComponent<PortalGun_Left>().DontShoot = false;
            velocityKeeperOn = true;
        }
    }
    void FixedUpdate() 
    {
        //Ragdoll 각각 부분에 Add force
	if (jumpFlag) 
        {
            vrik.solver.locomotion.maxVelocity = 8.0f;
            playerRig.GetComponent<Rigidbody>().AddForce((target - transform.position).normalized * 15f);//, ForceMode.Force );

	    foreach (Muscle m in puppetMaster.muscles) 
            {
		m.rigidbody.AddForce((target - transform.position).normalized * 15f);//, ForceMode.Force);
	    }
            jumpFlag = false;
	}

    }

}


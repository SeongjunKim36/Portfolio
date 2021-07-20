using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using RootMotion.FinalIK;
using RootMotion.Dynamics;

public class ArmSwing : MonoBehaviour
{
    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Input_Sources lefthand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources righthand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean grab = SteamVR_Actions.default_GrabGrip;
    //public SteamVR_Action_Boolean Trigger = SteamVR_Actions.default_Trigger;
    public SteamVR_Action_Pose pose = SteamVR_Actions.default_Pose;
    //public SteamVR_Action_Boolean touchpadClick = SteamVR_Actions.default_TouchPadClick;
    //public SteamVR_Action_Vector2 touchpadPos = SteamVR_Actions.default_TouchPadPos;
    //public SteamVR_Action_Boolean menu = SteamVR_Actions.default_Menu;
    //public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;
    public SteamVR_Action_Boolean touchpadTouch = SteamVR_Actions.default_TouchPadTouch;

    //private GameObject PlayerRig;

    [Header("Speed Settings")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float leftSwingForceMultiplier = 1.5f;
    [SerializeField] private float rightSwingForceMultiplier = 1.5f;

    [Header("Movement Settings")]
	[SerializeField] private bool isToggle = false;

		//-------------------------------------------------------------------------------
		//[HideInInspector]  public eMotionType motionType;
		//[HideInInspector]  public eSwimSettings swimSettings;

    [HideInInspector][SerializeField] private float maxSwimVelocity = 0.35f;
	[HideInInspector][SerializeField] private float swimSlideTime = 0.5f;

	[HideInInspector][SerializeField] private float maxSkiVelocity = 0.2f;
	[HideInInspector][SerializeField] private float SkiSlideTime = 0.25f;

	[HideInInspector][SerializeField] private float maxArmSwingVelocity = 0.2f;
	[HideInInspector][SerializeField] private float ArmSwingSlideTime = 0.25f;


    private float zVelLeft;
	private float zVelRight;
	private float xVelLeft;
	private float xVelRight;
    private float leftSwingForce;
    private float rightSwingForce;
    private float ActiveForce;
    private float maxSwingForce = 10.0f;

    public 	bool toggleOn = false;
	private bool swingingArmBack = false;

    //public Transform playerBody;

    private Rigidbody rb;
    private Vector3 slideVel;
    private bool isMove = false;
    private bool isArmSwing = false;
    public PuppetMaster puppetMaster;
    public VRIK vrik;
    public GameObject leftController;
    public GameObject rightController;
    public GameObject rotateStabilizer;

    public static bool onGround = false;

    private Vector3 bothControllerVel;
    public GameObject staticAnimator;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        zVelLeft = gameObject.transform.InverseTransformDirection(pose.GetVelocity(lefthand)).z;
        zVelRight = gameObject.transform.InverseTransformDirection(pose.GetVelocity(righthand)).z;

        xVelLeft = gameObject.transform.InverseTransformDirection(pose.GetVelocity(lefthand)).x;
        xVelRight = gameObject.transform.InverseTransformDirection(pose.GetVelocity(righthand)).x;

            
        //Check LeftArm Swing force
        float leftPosX = 0.0f;//(pose.GetVelocity(lefthand).x < 0) ?  pose.GetVelocity(lefthand).x * -1 : pose.GetVelocity(lefthand).x;
        float leftPosY = (pose.GetVelocity(lefthand).y < 0) ?  pose.GetVelocity(lefthand).y * -1 : pose.GetVelocity(lefthand).y;
        float leftPosZ = (pose.GetVelocity(lefthand).z < 0) ?  pose.GetVelocity(lefthand).z * -1 : pose.GetVelocity(lefthand).z;

        leftSwingForce = Mathf.Max(leftPosX, leftPosY, leftPosZ) *leftSwingForceMultiplier * 3.0f;

        //Check RightArm Swing force
        float rightPosX = 0.0f;//(pose.GetVelocity(righthand).x < 0) ?  pose.GetVelocity(righthand).x * -1 : pose.GetVelocity(righthand).x;
        float rightPosY = (pose.GetVelocity(righthand).y < 0) ?  pose.GetVelocity(righthand).y * -1 : pose.GetVelocity(righthand).y;
        float rightPosZ = (pose.GetVelocity(righthand).z < 0) ?  pose.GetVelocity(righthand).z * -1 : pose.GetVelocity(righthand).z;

        rightSwingForce = Mathf.Max(rightPosX, rightPosY, rightPosZ) *rightSwingForceMultiplier * 3.0f;

        ActiveForce = leftSwingForce + rightSwingForce;

        
        

        //Set Max Speed
        if(ActiveForce >= maxSwingForce)
        {
            ActiveForce = maxSwingForce;
        }


        if(ActiveForce >= 1.0f)
        {
            //vrik.solver.locomotion.weight = 0;
            isArmSwing = true;
        }
        else if(ActiveForce < 1.0f)
        {
            isArmSwing = false;
            //vrik.solver.locomotion.weight = 1;
        }  


        if(Player.isHanging == false && onGround)
        {
            if(touchpadTouch.GetState(lefthand) || touchpadTouch.GetState(righthand))
            {
                if(rotateStabilizer.GetComponent<FixedJoint>().connectedBody != null)
                {
                    rotateStabilizer.GetComponent<FixedJoint>().connectedBody = null;
                }

                // if(staticAnimator.transform.position.y != 0)
                // {
                //     staticAnimator.transform.position = new Vector3(staticAnimator.transform.position.x, staticAnimator.transform.position.y, staticAnimator.transform.position.z);
                //     //Debug.Log("@@@@@@@@@@@@@@@@@@@@@@");
                // }
                //vrik.solver.locomotion.weight = 0;
                //Quaternion LeftHandRot = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, pose.GetLocalRotation(lefthand).eulerAngles.y, transform.rotation.eulerAngles.z)), Time.deltaTime * 0.5f);
                //Quaternion rightHandRot = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, pose.GetLocalRotation(righthand).eulerAngles.y, transform.rotation.eulerAngles.z)), Time.deltaTime * 0.5f);
                //Quaternion bothHandRot = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, ((pose.GetLocalRotation(lefthand).eulerAngles.y + pose.GetLocalRotation(righthand).eulerAngles.y)/2), transform.rotation.eulerAngles.z)), Time.deltaTime * 0.5f);
                
                //Debug.Log("달려라");
                // rb.velocity = gameObject.transform.forward * moveSpeed * ActiveForce * 0.01f;

                // // Also set velocities for all the muscles
                // foreach (Muscle m in puppetMaster.muscles) 
                // {
                // 	m.rigidbody.velocity = gameObject.transform.forward * moveSpeed * ActiveForce * 0.01f;

                    
                // }

                Vector3 LeftControllerVel = leftController.transform.forward;
                Vector3 rightControllerVel = rightController.transform.forward;
                //Vector3 bothControllerVel = LeftControllerVel + rightControllerVel;
                Vector3 controllerDirection = new Vector3(bothControllerVel.x, 0, bothControllerVel.z);
                Vector3 lerpvel = Vector3.Lerp(transform.position, controllerDirection , Time.deltaTime);

                if(touchpadTouch.GetState(lefthand) && touchpadTouch.GetState(righthand) == false)
                {
                    bothControllerVel = LeftControllerVel * 2.0f;
                    //Debug.Log("@@@@@@@@@@@@@@@@@@@@@@@@@@");
                }
                
                else if(touchpadTouch.GetState(righthand) && touchpadTouch.GetState(lefthand) == false)
                {
                    bothControllerVel = rightControllerVel * 2.0f;
                }

                else if(touchpadTouch.GetState(lefthand) && touchpadTouch.GetState(righthand))
                {
                    bothControllerVel = LeftControllerVel + rightControllerVel;
                }

                
                
                rb.velocity = controllerDirection * moveSpeed * ActiveForce * 0.015f;
                
                // Also set velocities for all the muscles
                foreach (Muscle m in puppetMaster.muscles) 
                {
                    m.rigidbody.velocity = controllerDirection * moveSpeed * ActiveForce * 0.015f;

                    
                }
                
                if(leftSwingForce > 4.0f)
                {
                    slideVel = rb.velocity* ActiveForce;
                }

                }
            
            

        }
        else if(touchpadTouch.GetStateUp(lefthand)&&touchpadTouch.GetStateUp(righthand))
        {
            //vrik.solver.locomotion.weight = 1;
        }
        // else if(pose.GetLocalRotation(lefthand).eulerAngles.z >= 80.0f && Player.isHanging == false)
        // {
        //     isArmSwing = false;
        //     rb.velocity = slideVel;
        //     foreach (Muscle m in puppetMaster.muscles) 
        //     {
		// 		m.rigidbody.velocity = slideVel;

			    
		//     }

            

        // }
        // else if(pose.GetLocalRotation(lefthand).eulerAngles.z < 80.0f && isArmSwing == false && Player.isHanging == false)
        // {
        //     rb.velocity = new Vector3(0,0,0);

        //     foreach (Muscle m in puppetMaster.muscles) 
        //     {
		// 		m.rigidbody.velocity = new Vector3(0,0,0);

			    
		//     }
        // }


        // Quaternion LeftHandRot = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, pose.GetLocalRotation(lefthand).eulerAngles.y, transform.rotation.eulerAngles.z)), Time.deltaTime * 0.5f);
        // Quaternion rightHandRot = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, pose.GetLocalRotation(righthand).eulerAngles.y, transform.rotation.eulerAngles.z)), Time.deltaTime * 0.5f);
        // Quaternion bothHandRot = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, ((pose.GetLocalRotation(lefthand).eulerAngles.y + pose.GetLocalRotation(righthand).eulerAngles.y)/2), transform.rotation.eulerAngles.z)), Time.deltaTime * 0.5f);

        // if(leftSwingForce >= 2.0f || rightSwingForce <= 2.0f)
        // {
        //     transform.rotation = LeftHandRot;
        // }
        // else if(leftSwingForce <= 2.0f || rightSwingForce > 2.0f)
        // {
        //     transform.rotation = rightHandRot;
        // }
        // else if(leftSwingForce >= 2.0f || rightSwingForce >= 2.0f)
        // {
        //     transform.rotation = bothHandRot;
        // }
        
        
        
    }

    IEnumerator moveForTime(float a_timeInMs, float a_multiplier = 1.0f)
    {
		while (swingingArmBack) 
        {
			yield return new WaitForSeconds(a_timeInMs);
			swingingArmBack = false;
			yield return null;
		}
	}

    void UpdateArmSwing()
    {
		if ((zVelLeft   < -maxArmSwingVelocity && zVelRight > maxArmSwingVelocity) ||
			(zVelRight < -maxArmSwingVelocity && zVelLeft   > maxArmSwingVelocity)) 
            {
				swingingArmBack = true;
				StopCoroutine("moveForTime");
				StartCoroutine("moveForTime", ArmSwingSlideTime);
			}
	}


    
    
}

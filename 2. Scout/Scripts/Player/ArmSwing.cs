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
    public SteamVR_Action_Pose pose = SteamVR_Actions.default_Pose;
    public SteamVR_Action_Boolean touchpadTouch = SteamVR_Actions.default_TouchPadTouch;

    [Header("Speed Settings")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float leftSwingForceMultiplier = 1.5f;
    [SerializeField] private float rightSwingForceMultiplier = 1.5f;

    [Header("Movement Settings")]
    [SerializeField] private bool isToggle = false;
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
    public bool toggleOn = false;
    private bool swingingArmBack = false;
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

            
        //왼쪽 팔 스윙 Velocity 계산
        float leftPosX = 0.0f;
        float leftPosY = (pose.GetVelocity(lefthand).y < 0) ?  pose.GetVelocity(lefthand).y * -1 : pose.GetVelocity(lefthand).y;
        float leftPosZ = (pose.GetVelocity(lefthand).z < 0) ?  pose.GetVelocity(lefthand).z * -1 : pose.GetVelocity(lefthand).z;

        leftSwingForce = Mathf.Max(leftPosX, leftPosY, leftPosZ) *leftSwingForceMultiplier * 3.0f;

        //오른쪽 팔 스윙 Velocity 계산
        float rightPosX = 0.0f;
        float rightPosY = (pose.GetVelocity(righthand).y < 0) ?  pose.GetVelocity(righthand).y * -1 : pose.GetVelocity(righthand).y;
        float rightPosZ = (pose.GetVelocity(righthand).z < 0) ?  pose.GetVelocity(righthand).z * -1 : pose.GetVelocity(righthand).z;

        rightSwingForce = Mathf.Max(rightPosX, rightPosY, rightPosZ) *rightSwingForceMultiplier * 3.0f;

        ActiveForce = leftSwingForce + rightSwingForce;    
        

        //최대 속대 세팅
        if(ActiveForce >= maxSwingForce)
        {
            ActiveForce = maxSwingForce;
        }


        if(ActiveForce >= 1.0f)
        {
            isArmSwing = true;
        }
        else if(ActiveForce < 1.0f)
        {
            isArmSwing = false;
        }  


        if(Player.isHanging == false && onGround)
        {
            if(touchpadTouch.GetState(lefthand) || touchpadTouch.GetState(righthand))
            {
                if(rotateStabilizer.GetComponent<FixedJoint>().connectedBody != null)
                {
                    rotateStabilizer.GetComponent<FixedJoint>().connectedBody = null;
                }

                Vector3 LeftControllerVel = leftController.transform.forward;
                Vector3 rightControllerVel = rightController.transform.forward;
                Vector3 controllerDirection = new Vector3(bothControllerVel.x, 0, bothControllerVel.z);
                Vector3 lerpvel = Vector3.Lerp(transform.position, controllerDirection , Time.deltaTime);

                if(touchpadTouch.GetState(lefthand) && touchpadTouch.GetState(righthand) == false)
                {
                    bothControllerVel = LeftControllerVel * 2.0f;
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
                
                // 레그돌 전체에 velocity 적용
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
	if ((zVelLeft   < -maxArmSwingVelocity && zVelRight > maxArmSwingVelocity) || (zVelRight < -maxArmSwingVelocity && zVelLeft > maxArmSwingVelocity)) 
        {
	    swingingArmBack = true;
	    StopCoroutine("moveForTime");
	    StartCoroutine("moveForTime", ArmSwingSlideTime);
	}
    }   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Gun_Right : MonoBehaviour
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

    [HideInInspector]
    public bool DontShoot = false;

    private Transform firePos;
    public GameObject bullet;

    public float fireRate = 0.2f;
    private float nextFire = 0f;

    private void Start()
    {
        firePos = transform.Find("FirePos");
    }

    void Update()
    {
        if (trigger.GetState(righthand) && DontShoot == false)
        {
            if (nextFire <= Time.time)
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        GameObject a = Instantiate(bullet, firePos.position, transform.rotation);
        a.GetComponent<Rigidbody>().velocity = transform.forward * 30;
        nextFire = fireRate + Time.time;

        //haptic.Execute(0f, 0.1f, 80, 0.5f, righthand);
    }

}

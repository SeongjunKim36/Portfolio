using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PortalGun_Left : MonoBehaviour
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

    public GameObject portal1Bullet_prefab;
    public GameObject portal2Bullet_prefab;

    private Transform firepos;
    private GameObject portalgun1;
    private GameObject portalgun2;
    
    void Start()
    {
        portalgun1 = transform.Find("PortalGun1").gameObject;
        portalgun2 = transform.Find("PortalGun2").gameObject;
        firepos = transform.Find("FirePos");
    }

    void Update()
    {
        if(trigger.GetStateDown(lefthand) && DontShoot == false)
        {
            if (portalgun1.activeSelf)
            {
                GameObject bullet = Instantiate(portal1Bullet_prefab, firepos.position, transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = transform.forward * 20f;
                PortalMgr.SwitchPortalGun(portalgun2, portalgun1);
            }
            else if(portalgun2.activeSelf)
            {
                GameObject bullet = Instantiate(portal2Bullet_prefab, firepos.position, transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = transform.forward * 20f;
                PortalMgr.SwitchPortalGun(portalgun1, portalgun2);
            }
        }
    }
}

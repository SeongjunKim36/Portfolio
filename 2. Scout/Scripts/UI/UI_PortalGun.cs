using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class UI_PortalGun : MonoBehaviour
{
    public SteamVR_Input_Sources anyhand = SteamVR_Input_Sources.Any;
    public SteamVR_Input_Sources lefthand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources righthand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_Trigger;
    public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("HAND"))
        {
            if (other.gameObject.name == "LeftHand")
            {
                other.transform.Find("LeftPortalGun").GetComponent<PortalGun_Left>().DontShoot = true;

                if (trigger.GetStateDown(lefthand))
                {
                    other.transform.Find("LeftHandGun").gameObject.SetActive(false);
                    other.transform.Find("LeftPortalGun").gameObject.SetActive(true);
                    haptic.Execute(0f, 0.3f, 80f, 0.5f, lefthand);
                }
            }

            else if (other.gameObject.name == "RightHand")
            {
                other.transform.Find("RightPortalGun").GetComponent<PortalGun_Right>().DontShoot = true;

                if (trigger.GetStateDown(righthand))
                {
                    other.transform.Find("RightHandGun").gameObject.SetActive(false);
                    other.transform.Find("RightPortalGun").gameObject.SetActive(true);
                    haptic.Execute(0f, 0.3f, 80f, 0.5f, lefthand);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("HAND"))
        {
            if (other.gameObject.name == "LeftHand")
                other.transform.Find("LeftPortalGun").GetComponent<PortalGun_Left>().DontShoot = false;
            else if (other.gameObject.name == "RightHand")
                other.transform.Find("RightPortalGun").GetComponent<PortalGun_Right>().DontShoot = false;

        }
    }

}

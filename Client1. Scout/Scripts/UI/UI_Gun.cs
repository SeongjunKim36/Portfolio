using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class UI_Gun : MonoBehaviour
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
            if(other.gameObject.name == "LeftHand")
            {
                other.transform.Find("LeftHandGun").GetComponent<Gun_Left>().DontShoot = true;

                if (trigger.GetStateDown(lefthand))
                {
                    
                    other.transform.Find("LeftPortalGun").gameObject.SetActive(false);
                    other.transform.Find("LeftHandGun").gameObject.SetActive(true);
                    haptic.Execute(0f, 0.3f, 80f, 0.5f, lefthand);
                }
            }
            
            else if (other.gameObject.name == "RightHand")
            {
                other.transform.Find("RightHandGun").GetComponent<Gun_Right>().DontShoot = true;

                if (trigger.GetStateDown(righthand))
                {
                    other.transform.Find("RightPortalGun").gameObject.SetActive(false);
                    other.transform.Find("RightHandGun").gameObject.SetActive(true);
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
                other.transform.Find("LeftHandGun").GetComponent<Gun_Left>().DontShoot = false;
            else if (other.gameObject.name == "RightHand")
                other.transform.Find("RightHandGun").GetComponent<Gun_Right>().DontShoot = false;

        }
    }
}

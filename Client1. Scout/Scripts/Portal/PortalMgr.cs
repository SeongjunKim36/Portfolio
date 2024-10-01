using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMgr : MonoBehaviour
{
    //포탈 생성시 총 변경
    public static void SwitchPortalGun(GameObject activeGun, GameObject inactiveGun)
    {
        activeGun.SetActive(true);
        inactiveGun.SetActive(false);
    }
}

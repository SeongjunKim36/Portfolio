using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    private Transform playerCamTr;
    void Start()
    {
        playerCamTr = GameObject.FindGameObjectWithTag("Player").transform.Find("Camera (eye)");
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(playerCamTr.rotation.eulerAngles.x, playerCamTr.rotation.eulerAngles.y,0f));
        
    }
}

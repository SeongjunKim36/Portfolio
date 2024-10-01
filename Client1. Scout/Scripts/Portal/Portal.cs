using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Camera targetCam;

    private Transform playerCamTr;
    private float view = 60;
    void Start()
    {
        playerCamTr = GameObject.FindGameObjectWithTag("Player").transform.Find("Camera (eye)");
    }


    void Update()
    {
        transform.LookAt(playerCamTr);

        //시야각
        view = 150f - Mathf.Clamp(Vector3.Distance(transform.position, playerCamTr.position)*10f, 0f, 60f);
        targetCam.fieldOfView = view;
    }
}

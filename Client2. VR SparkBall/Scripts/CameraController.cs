using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Photon.MonoBehaviour
{
    public GameObject cam;
    public Transform robot_tr;
    //PhotonView photonView;
    void Start()
    {
        //photonView = gameObject.GetComponent<PhotonView>();
        if(photonView.isMine)
        {
            cam.GetComponent<Camera>().enabled = true;
            cam.GetComponent<AudioListener>().enabled = true;
        }

    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraTr;
    void Start()
    {
        
    }

    void Update()
    {
        gameObject.transform.position = cameraTr.transform.position;
        gameObject.transform.rotation = cameraTr.transform.rotation;
    }
}

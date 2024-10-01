using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutCameraFollow : MonoBehaviour
{
    public Transform camTr;
    private Vector3 aimPos;

    private Quaternion aimRot;
    public float aimdistance = 0.3f;
    public float damping = 5.0f;



    void Start()
    {


       

    }

    void LateUpdate()
    {
        aimPos = camTr.position + camTr.forward * aimdistance;
        aimRot = camTr.rotation;
        
        transform.position = Vector3.Lerp(transform.position, new Vector3(aimPos.x, aimPos.y, aimPos.z), Time.deltaTime * damping);
        transform.rotation = Quaternion.Lerp(transform.rotation, aimRot, Time.deltaTime * damping);
    }
}

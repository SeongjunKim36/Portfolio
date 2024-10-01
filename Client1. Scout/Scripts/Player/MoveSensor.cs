using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSensor : MonoBehaviour
{
    //private float sensorDistance = 2.5f;
    //public LineRenderer line;
    private int wallMask;
    private int groundMask;
    void Start()
    {
        wallMask = 1<<LayerMask.NameToLayer("WALL");
        groundMask = 1<<LayerMask.NameToLayer("Water");
    }

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up,  out hit, 10f, wallMask | groundMask))
        {
            //line.enabled = true;
            //Debug.Log(hit.distance);
            if(hit.distance <= 2.2f)
            {
                ArmSwing.onGround = true;
            }
            else if(hit.distance > 2.2f)
            {
                ArmSwing.onGround = false;
            }
        }
    }
}

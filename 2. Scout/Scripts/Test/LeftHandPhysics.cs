using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandPhysics : MonoBehaviour
{
    public Transform leftHand;
    
    void Update()
    {
        leftHand.position = Vector3.Lerp(leftHand.position, transform.position, Time.deltaTime*100);
    }
}

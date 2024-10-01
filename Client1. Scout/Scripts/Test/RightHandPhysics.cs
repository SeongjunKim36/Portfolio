using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandPhysics : MonoBehaviour
{
    public GameObject rightHand;
    void Update()
    {
        rightHand.transform.position = Vector3.Lerp(rightHand.transform.position, transform.position, Time.deltaTime*100);

    }
}

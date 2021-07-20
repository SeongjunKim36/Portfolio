using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Transform dummyTransform;

    void LateUpdate()
    {
        //transform.position = new Vector3(dummyTransform.position.x, dummyTransform.position.y -0.5f, dummyTransform.position.z);
        transform.position = dummyTransform.position;
    }
}

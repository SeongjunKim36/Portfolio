using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Transform dummyTransform;

    void LateUpdate()
    {
        transform.position = dummyTransform.position;
    }
}

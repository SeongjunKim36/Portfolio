using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationChanger : MonoBehaviour
{
    public Transform anchor;
    void Start()
    {
        
    }

    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position,anchor.position,Time.deltaTime * 10);
        transform.position = anchor.position;

    }
    void LateUpdate() {

        //transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
        
    }
}

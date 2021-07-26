using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    Rigidbody rb;
    //TimeManager tm;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0f, 100f,100f));
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            rb.AddForce(new Vector3(50f, 50f,50f));
        }
    }
}

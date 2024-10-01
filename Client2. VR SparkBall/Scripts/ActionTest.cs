using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ActionTest : MonoBehaviour
{
    public float force;
    private float actualForce;
    Rigidbody rb;
    TimeManager tm;

    public static Vector3 normalSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(new Vector3(0f, 10f,10f));
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddForce(Physics.gravity * TimeManager.GetInstance().myTimeScale);
        normalSpeed = rb.velocity;
        Vector3 velocity = rb.velocity.normalized * TimeManager.GetInstance().myTimeScale;
        rb.velocity = velocity*10;
        //Debug.Log(rb.velocity);
        

    }
}

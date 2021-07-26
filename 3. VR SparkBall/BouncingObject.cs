using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingObject : MonoBehaviour
{
    public float force;
    private float actualForce;
    Rigidbody rb;
    TimeManager tm;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Physics.gravity * TimeManager.GetInstance().myTimeScale);
        Vector3 velocity = rb.velocity * TimeManager.GetInstance().myTimeScale;
        rb.velocity = velocity;

    }
}

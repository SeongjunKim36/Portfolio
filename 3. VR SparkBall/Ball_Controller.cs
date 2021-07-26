using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ball_Controller : MonoBehaviour

{

    
    private Transform ball_tr;
    private Rigidbody ball_rigi;
    private Rigidbody Hit_Ball;
    Vector3 lastVelocity;


    private Vector3 nowTr;
    private Vector3 oldTr;
    private float speed;

    void Start()
    {
        speed = 1.0f;
        ball_rigi = this.gameObject.GetComponent<Rigidbody>();
        
        oldTr = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = ball_rigi.velocity;
        //nowTr = transform.position;
        //var distance = (nowTr - oldTr);
        //ball_rigi.velocity = distance / Time.deltaTime *speed;
        
        //oldTr = nowTr;
        //if(Input.GetMouseButtonDown(0))
        //{
        //    ball_rigi.AddForce(-5, -5, -5);
        //}

    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.CompareTag("WALL"))
        {
            ContactPoint cp = coll.contacts[0];
            
            lastVelocity = Vector3.Reflect(lastVelocity.normalized, cp.normal);
            ball_rigi.velocity = lastVelocity*20.0f;

     
        }

    }



}

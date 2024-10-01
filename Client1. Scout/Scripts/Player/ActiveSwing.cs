using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using RootMotion.FinalIK;
using RootMotion.Dynamics;

public class ActiveSwing : MonoBehaviour
{
    public GameObject anchor;
    private Rigidbody anchorRb;

    private Vector3 prevVel;
    private Vector3 currVel;
    private Vector3 deltaVel;

    private bool hanging;
    public PuppetMaster puppetMaster;

    void Start() 
    {
        anchorRb = anchor.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(anchor.transform.position);

	foreach (Muscle m in puppetMaster.muscles) 
        {
	    m.rigidbody.AddForce(anchor.transform.position);
	}

        if(gameObject.GetComponent<Rigidbody>().velocity.magnitude == 10.0f)
        {
            deltaVel = gameObject.GetComponent<Rigidbody>().velocity;
        }

        if(gameObject.GetComponent<Rigidbody>().velocity.magnitude > 10.0f)
        {
            gameObject.GetComponent<Rigidbody>().velocity = deltaVel;

            // 레그돌 각각의 부위에 벨로시티 조절
            foreach (Muscle m in puppetMaster.muscles) 
            {
                m.rigidbody.velocity = deltaVel;                    
            }
        }
                
    }    
}

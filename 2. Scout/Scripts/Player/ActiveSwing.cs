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
        // currVel = anchorRb.velocity;
        // deltaVel = currVel - prevVel;
        // prevVel = currVel;

        


        // gameObject.GetComponent<Rigidbody>().velocity = deltaVel *30.0f;

		// 	// Also set velocities for all the muscles
		// 	foreach (Muscle m in puppetMaster.muscles) 
        //     {
		// 		m.rigidbody.velocity = deltaVel *30.0f;

			    
		//     }

        //Debug.Log(gameObject.GetComponent<Rigidbody>().velocity.magnitude);

        
            gameObject.GetComponent<Rigidbody>().AddForce(anchor.transform.position);

			// Also set velocities for all the muscles
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
                //deltaVel = gameObject.GetComponent<Rigidbody>().velocity;

                gameObject.GetComponent<Rigidbody>().velocity = deltaVel;

                // Also set velocities for all the muscles
                foreach (Muscle m in puppetMaster.muscles) 
                {
                    m.rigidbody.velocity = deltaVel;

                    
                }
            }
                
    }

    // void FixedUpdate() 
    // {
    //     //prevVel = anchorRb.velocity;
        

    //     Debug.Log(deltaVel);

    //     if (hanging) 
    //     {
	// 		//rb.velocity = velocity;
            
	//     }
    // }
    
}

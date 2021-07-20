using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyControl : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // void OnTriggerEnter(Collider other) 
    // {
    //     if(other.gameObject.layer == 0 && LeftWeb.isSwinging == false)
    //     {
    //         Debug.Log("@@@@@@@@@@@@@@@");
    //         if(gameObject.GetComponent<Rigidbody>() != null)
    //         {
    //             Destroy(gameObject.GetComponent<Rigidbody>());
    //         }
            
    //     }
    // }

    private void OnCollisionEnter(Collision other) {
        // if(other.gameObject.layer == 0 && LeftWeb.isSwinging == false)
        // {
        //     Debug.Log("@@@@@@@@@@@@@@@");
        //     if(gameObject.GetComponent<Rigidbody>() != null)
        //     {
        //         Destroy(gameObject.GetComponent<Rigidbody>());
        //     }
            
        // }
        
    }

    
}

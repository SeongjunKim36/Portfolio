using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward *1000);
            Debug.Log("@@@@@@@@@@@@@@@@@@@@");
        }
    }
}

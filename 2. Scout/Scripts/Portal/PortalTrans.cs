using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrans : MonoBehaviour
{
    public Transform targetPortalTr;
    public static bool isTransporting = true;
    void Start()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10) //transportable
        {
            if(isTransporting == true)
            {
                isTransporting = false;
                other.gameObject.transform.position = targetPortalTr.position;
                Invoke("DelayPotal", 0.3f);
            }
        }
    }
    
    void DelayPotal()
    {
        isTransporting = true;
    }
    
}

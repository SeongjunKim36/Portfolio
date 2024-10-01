using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetZoneEnter : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            TutorialManager.zoneEnterIndex++;
            
            Destroy(gameObject, 1f);
        }
    }
}

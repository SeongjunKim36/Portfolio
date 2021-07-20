using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTargetGrip : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other) 
    {
        TutorialManager.ropeTargetIndex++;
    }
}

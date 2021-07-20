using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranformSynchro : MonoBehaviour
{
    public GameObject puppet;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = puppet.transform.position;
    }
}

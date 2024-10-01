using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    public float gravity = -9.8f;
    void Start()
    {
        
    }

    void Update()
    {
        Physics.gravity = new Vector3(0,gravity,0);
    }
}

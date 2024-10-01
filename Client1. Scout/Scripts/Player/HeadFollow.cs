using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollow : MonoBehaviour
{
    public Transform headTr;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(headTr.position.x, transform.position.y, headTr.position.z);
    }
}

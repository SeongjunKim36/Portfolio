using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadControl : MonoBehaviour
{
    public GameObject head;
    public GameObject body;
    
    void Update()
    {
        head.transform.position = Vector3.Lerp(head.transform.position, transform.position, Time.deltaTime*100);
        body.transform.position = new Vector3(transform.position.x,body.transform.position.y,transform.position.z); 
    }
}

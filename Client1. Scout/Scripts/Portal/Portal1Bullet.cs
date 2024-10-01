using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal1Bullet : MonoBehaviour
{
    private GameObject portal1;

    void Start()
    {
        portal1 = GameObject.FindGameObjectWithTag("PORTAL").transform.GetChild(0).gameObject;
        Invoke("OpenPortal",1.5f);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9 || other.gameObject.layer == 4)
        {
            OpenPortal();
        }
    }
    
    void OpenPortal()
    {
        portal1.transform.position = transform.position - transform.forward + transform.up;
        portal1.SetActive(true);
        Destroy(this.gameObject);
    }
}

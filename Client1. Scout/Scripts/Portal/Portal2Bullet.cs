using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal2Bullet : MonoBehaviour
{
    private GameObject portal2;

    void Start()
    {
        portal2 = GameObject.FindGameObjectWithTag("PORTAL").transform.GetChild(1).gameObject;
        Invoke("OpenPortal", 1.5f);
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
        portal2.transform.position = transform.position - transform.forward + transform.up;
        portal2.SetActive(true);
        Destroy(this.gameObject);
    }
}

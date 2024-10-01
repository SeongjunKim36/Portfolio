using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBuilding : MonoBehaviour
{
    public GameObject Replacement;
    // void OnTriggerEnter(Collider col)
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.parent == null || col.transform.parent.parent == null)
            return;
        if (col.transform.parent.parent.gameObject.CompareTag("BOSS"))
        {
            GameObject.Instantiate(Replacement, transform.position, transform.rotation);
            
            GameObject cam = GameObject.FindGameObjectWithTag("Player").transform.Find("Camera (eye)").gameObject;
            cam.GetComponent<CameraShake>().shake = Mathf.Clamp(Vector3.Distance(transform.position, cam.transform.position)/10f,0.5f,2f);
            // cam.GetComponent<CameraShake>().shake = 1f;
            cam.GetComponent<CameraShake>().enabled = true;
            

            Destroy(gameObject);
        }
    }
}

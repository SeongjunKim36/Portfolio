using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HitBall : MonoBehaviour
{

    public Vector3 curpos;
    public Vector3 oldpos;
    public Vector3 vel;

    void Start()
    {
        
    }
  


    // Update is called once per frame
    void Update()
    {
        GripballMove();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Yun_Test + SuperHot");
        }
    }



    public void GripballMove()
    {
        curpos = transform.position;
        vel = (curpos - oldpos) / Time.deltaTime;

        oldpos = curpos;

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.CompareTag("BALL"))
    //    {
    //        Debug.Log("gg");

    //        other.gameObject.GetComponent<Rigidbody>().velocity = vel;
    //    }
    //}
}

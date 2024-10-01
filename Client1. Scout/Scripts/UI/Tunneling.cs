using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tunneling : MonoBehaviour
{
    public  Image tunnelImage;
    public Rigidbody playerRb;
   // private float speed = 300f;
    private bool isRunning = false;
    

    Vector3 defaultscale = new Vector3(5, 5, 5);
    Vector3 tunnelscale = new Vector3(1, 1, 1);
    private float speed;



    void Update()
    {
        
        Vector3 currentPos = playerRb.transform.position;
        Vector3 newPos = currentPos;
        speed = playerRb.velocity.magnitude;
        TunnelEffect();
        if (speed > 2)
        {
            isRunning = true;    
        }
        else
        {
            isRunning = false;
        }
    }

    void TunnelEffect()
    {
        //Debug.Log(playerRb.velocity.magnitude);
        //Debug.Log(isRunning);
        
        if (isRunning)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, tunnelscale, Time.deltaTime* speed);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, defaultscale, Time.deltaTime*2f);
        }
    }

  
    
}

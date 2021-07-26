using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float fall = 0;
    public bool touchFloor = false;
    
    private float fallingTime = 1.0f;
    private int floorLayer;
    private int middleLayer;
    TetrisControl tetcon;
    
    void Start()
    {
        floorLayer = 1<<LayerMask.NameToLayer("FLOOR");
        middleLayer = 1<<LayerMask.NameToLayer("MIDDLE");
        
        StartCoroutine(TopBlockFall());
        StartCoroutine(RightBlockFall());
        StartCoroutine(LeftBlockFall());
        
        
    }
    
    IEnumerator TopBlockFall()
    {
        if(gameObject.tag == "TOP")
        {
            while(!touchFloor)
            {
                yield return new WaitForSeconds(fallingTime);
                transform.position -= new Vector3 (0, 1, 0);
            }
        }        
    }
    IEnumerator RightBlockFall()
    {
        if(gameObject.tag == "RIGHT")
        {
            while(!touchFloor)
            {
                yield return new WaitForSeconds(fallingTime);
                transform.position -= new Vector3 (1, 0, 0);
            }
        }        
    }
    IEnumerator LeftBlockFall()
    {
        if(gameObject.tag == "LEFT")
        {
            while(!touchFloor)
            {
                yield return new WaitForSeconds(fallingTime);
                transform.position += new Vector3 (1, 0, 0);
            }
        }        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 8 && gameObject.tag == "TOP")
        {
            touchFloor = true;
        }
        if(other.gameObject.layer == 9 && gameObject.tag == "LEFT")
        {
            touchFloor = true;
        }
        if(other.gameObject.layer == 9 && gameObject.tag == "RIGHT")
        {
            touchFloor = true;
        }
    }
    
    
}

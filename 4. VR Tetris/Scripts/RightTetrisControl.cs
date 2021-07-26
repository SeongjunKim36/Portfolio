using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTetrisControl : MonoBehaviour
{
    public static int gridX = 10;
    public static int gridY = 10;
    public static int gridZ = 10;
    

    float fall = 0;
    public bool touchFloor = false;    
    private float fallingTime = 1.0f;
    
    void Update() 
    {
        TestInput();
        
    }

    private void OnCollisionEnter(Collision other) {
        
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
    }

    

    void TestInput()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(0,0,-1);
            if(CheckIsValidPositon())
            {
                GameManager.UpdateGridRight(this);
                
            }
            else
            {
                transform.position += new Vector3(0,0,1);
            }
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(0,0,1);
            if(CheckIsValidPositon())
            {
                GameManager.UpdateGridRight(this);
                
            }
            else
            {
                transform.position += new Vector3(0,0,-1);
            }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0,1,0);
            if(CheckIsValidPositon())
            {
                GameManager.UpdateGridRight(this);
            }
            else
            {
                transform.position += new Vector3(0,-1,0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position -= new Vector3(0,1,0);
            if(CheckIsValidPositon())
            {
                GameManager.UpdateGridRight(this);
                
            }
            else
            {
                transform.position -= new Vector3(0,-1,0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Space)||Time.time - fall >= fallingTime)
        {
            transform.position += new Vector3(-1,0,0);
            fall = Time.time;
            if(CheckIsValidPositon())
            {
                GameManager.UpdateGridRight(this);
                
            }
            else
            {
                transform.position += new Vector3(1,0,0);
                GameManager.DeleteRaw();
            }
        }
    }

    bool CheckIsValidPositon()
    {
        foreach(Transform cube in transform)
        {
            Vector3 pos = GameManager.Round(cube.position);   
            if(GameManager.CheckIsInsideRightGrid(pos)==false)
            {
                return false;
            }
            if(GameManager.GetTransformAtGridPosition(pos) != null 
            && GameManager.GetTransformAtGridPosition(pos).parent != transform)
            {
                return false;
            }       
        }
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisControl : MonoBehaviour
{


    float fall = 0;
    public bool touchFloor = false;
    
    private float fallingTime = 1.0f;
    

    void Start() 
    {
        
    
    }
    void Update() 
    {
        MoveTetris();
        
    }

    private void OnCollisionEnter(Collision other) {
        
        //gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //gameObject.GetComponent<BoxCollider>().isTrigger = false;
    }

    

    void MoveTetris()
    {
        if(PadController.TopTetrisMoved[0])
        {
            //transform.position += new Vector3(1,0,0);
            if(CheckIsValidPositon())
            {
                GameManager.UpdateGrid(this);
                PadController.TopTetrisMoved[0] = false;                
            }
            else
            {
                transform.position += new Vector3(1,0,0);
                PadController.TopTetrisMoved[0] = false;
            }
        }
        else if(PadController.TopTetrisMoved[1])
        {
            //transform.position -= new Vector3(1,0,0);
            if(CheckIsValidPositon())
            {
                GameManager.UpdateGrid(this);
                PadController.TopTetrisMoved[1] = false;
                
            }
            else
            {
                transform.position -= new Vector3(1,0,0);
                PadController.TopTetrisMoved[1] = false;
            }
        }
        else if(PadController.TopTetrisMoved[2])
        {
            //transform.position += new Vector3(0,0,1);
            if(CheckIsValidPositon())
            {
                GameManager.UpdateGrid(this);
                PadController.TopTetrisMoved[2] = false;
            }
            else
            {
                transform.position += new Vector3(0,0,1);
                PadController.TopTetrisMoved[2] = false;
            }
        }
        else if(PadController.TopTetrisMoved[3])
        {
            //transform.position -= new Vector3(0,0,1);
            if(CheckIsValidPositon())
            {
                GameManager.UpdateGrid(this);
                PadController.TopTetrisMoved[3] = false;
                
            }
            else
            {
                transform.position -= new Vector3(0,0,1);
                PadController.TopTetrisMoved[3] = false;
            }
        }
        else if(OVRInput.GetDown(OVRInput.Button.One)||Time.time - fall >= fallingTime)
        {
            transform.position += new Vector3(0,-1,0);
            fall = Time.time;
            if(CheckIsValidPositon())
            {
                GameManager.UpdateGrid(this);
                
            }
            else
            {
                transform.position += new Vector3(0,1,0);
                GameManager.DeleteRaw();
                enabled = false;
                PadController.Tetris = PadController.initializer;
                
                FindObjectOfType<GameManager>().SpawnNextTetris();
            }
        }
    }

    void RotateTetris()
    {
        if(PadController.TopTetrisRotated[0])
        {
            if(CheckIsValidPositon())
            {
                GameManager.UpdateGrid(this);
                PadController.TopTetrisRotated[0] = false;                
            }
            else
            {
                transform.Rotate(0,-90,0, Space.World);
                PadController.TopTetrisRotated[0] = false;
            }
        }
        else if(PadController.TopTetrisRotated[1])
        {
            
            if(CheckIsValidPositon())
            {
                GameManager.UpdateGrid(this);
                PadController.TopTetrisRotated[1] = false;
                
            }
            else
            {
                transform.Rotate(0,-90,0, Space.World);
                PadController.TopTetrisRotated[1] = false;
            }
        }
        else if(PadController.TopTetrisRotated[2])
        {
            
            if(CheckIsValidPositon())
            {
                GameManager.UpdateGrid(this);
                PadController.TopTetrisRotated[2] = false;
            }
            else
            {
                transform.Rotate(-90,0,0, Space.World);
                PadController.TopTetrisRotated[2] = false;
            }
        }
        else if(PadController.TopTetrisRotated[3])
        {
            
            if(CheckIsValidPositon())
            {
                GameManager.UpdateGrid(this);
                PadController.TopTetrisRotated[3] = false;
                
            }
            else
            {
                transform.Rotate(90,0,0, Space.World);
                PadController.TopTetrisRotated[3] = false;
            }
        }
    }

    bool CheckIsValidPositon()
    {
        foreach(Transform cube in transform)
        {
            Vector3 pos = GameManager.Round(cube.position);   
            if(GameManager.CheckIsInsideGrid(pos)==false)
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

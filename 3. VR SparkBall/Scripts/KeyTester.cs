using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyTester : MonoBehaviour
{
    TimeManager TM;

    float targetScale;
    float lerpSpeed = 2;
    
    public ActionTest SuperHot;

    private Vector3 prevPosition;
    private Vector3 currPostion;
    private Vector3 deltaPosition;
    public Rigidbody ball;

    //private TimeManager TM;

    
    private bool isSuper = false;
    void Start()
    {
        TM = TimeManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // if(mouseX != 0 || mouseY !=0 )
        // {
            
        //     targetScale = 0.5f;
        //     lerpSpeed = 10;
        // }
        // else 
        // {
        //     if(Input.anyKey)
        //     {
        //         targetScale = 1;
        //         lerpSpeed = 10;
        //     }
        //     else
        //     {
        //         targetScale = 0;
        //         lerpSpeed = 4;
        //     }

        // TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);
        // }

        
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("SuperHot_Test3");
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            SuperHot.enabled = false;
        }


        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            SuperHot.enabled = true;
            isSuper = true;
            targetScale = 0.05f;
            lerpSpeed = 0.8f;
            //Debug.Log("SuperHot on!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            //TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);
            //ball.velocity = ActionTest.normalSpeed;
            
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            targetScale = 1.0f;
            lerpSpeed = 0.5f;
            
            
            //Debug.Log("SuperHot on!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            //TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);
            //SuperHot.enabled = false;
        }
        // targetScale = 1f;
        // lerpSpeed = 4;
        // Debug.Log("SuperHot On@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        // TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);


        if(isSuper == true)
        {
            
            // currPostion = tr.position;
            
            // deltaPosition = currPostion - prevPosition;

            // prevPosition = currPostion ;

            
                
            //Debug.Log(deltaPosition.x +"얼마나와");
            if(mouseX != 0 || mouseY !=0)
            {
                //SuperHot.enabled = false;
                targetScale = 1.0f;
                lerpSpeed = 0.8f;
                //Debug.Log("SuperHot off!!!!!!!!!!!!!!!!!!!");
                //Debug.Log(deltaPosition);
                
                //isMove = false;
                //Debug.Log("오나?");
                //TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);

            }
            
                else if(Input.GetKeyDown(KeyCode.Delete))
                {
                        targetScale = 1f;
                        lerpSpeed = 30;
                        isSuper = false;
                        //TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);
                        SuperHot.enabled = false;
                }
                else
                {
                    //SuperHot.enabled = true;
                    targetScale = 0.05f;
                    lerpSpeed = 0.5f;
                    //Debug.Log("SuperHot On@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                    //TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);

                }
                TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);
                //Debug.Log("오고있니");
            
            
        }
        
        

        

        

        


        


        

        // if(mouseX != 0 || mouseY !=0)
        // {
        //     SuperHot.enabled = false;
        //     targetScale = 0.5f;
        //     lerpSpeed = 10;
        //     Debug.Log("SuperHot off!!!!!!!!!!!!!!!!!!!");

        //     TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);

        // }
        // else 
        // {
        //     if(Input.GetKeyDown(KeyCode.Delete))
        //     {
        
        //         //SuperHot.enabled = false;
        //         Debug.Log("SuperHot off@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        
            
        //         targetScale = 1;
        //         lerpSpeed = 10;
        //     }
        //     else
        //     {
        //         //SuperHot.enabled = true;
        //         targetScale = 0;
        //         lerpSpeed = 4;
        //         Debug.Log("SuperHot On@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        //     }
        //     TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);
        //     Debug.Log("오고있니");
        // }
        
        
    }
}

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

    
    private bool isSuper = false;
    void Start()
    {
        TM = TimeManager.GetInstance();
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
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
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            targetScale = 1.0f;
            lerpSpeed = 0.5f;
            
        }


        if(isSuper == true)
        {
            if(mouseX != 0 || mouseY !=0)
            {
                targetScale = 1.0f;
                lerpSpeed = 0.8f;
            }
            
                else if(Input.GetKeyDown(KeyCode.Delete))
                {
                        targetScale = 1f;
                        lerpSpeed = 30;
                        isSuper = false;
                        SuperHot.enabled = false;
                }
                else
                {
                    targetScale = 0.05f;
                    lerpSpeed = 0.5f;

                }
                TM.myTimeScale = Mathf.Lerp(TM.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);
            
        }
        
        
    }
}

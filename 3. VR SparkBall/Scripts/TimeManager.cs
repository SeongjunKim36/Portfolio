using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float myDelta;
    public float myFixedDelta;
    public float myTimeScale = 1;

    public static TimeManager instance;

    public static TimeManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
    }

    void FixedUpdate() 
    {
        myFixedDelta = Time.fixedDeltaTime * myTimeScale;
        
    }
    void Update()
    {
        myDelta = Time.deltaTime * myTimeScale;
    }
}

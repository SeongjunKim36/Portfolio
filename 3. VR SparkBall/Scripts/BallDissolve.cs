using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDissolve : MonoBehaviour
{
    public Material dissolveMat;
    private float dissolve_Value;
    private float start_value;
    public float end_value;

    void Start()
    {
        start_value = 0;
        dissolveMat.SetFloat("_DissolveValue", start_value / end_value);

    }
    public float Start_Value
    {
        get
        {
            return start_value;
        }
        set
        {
            start_value = value;
        }
    }
    private void Update()
    {
        
        DissolveBall(start_value);

    }

    void DissolveBall(float _stat_value)
    {
        if (_stat_value >0)
        {
            start_value -= 0.5f;
            dissolveMat.SetFloat("_DissolveValue", start_value / end_value);
        }
    }

}

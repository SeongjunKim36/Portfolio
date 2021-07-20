using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cam;

    public float shake = 1f;

    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (cam == null)
        {
            //cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
    }

    void OnEnable()
    {
        originalPos = cam.localPosition;
    }

    void Update()
    {
        if (shake > 0)
        {
            cam.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shake = 0f;
            cam.localPosition = originalPos;
            this.enabled = false;
        }
    }


}
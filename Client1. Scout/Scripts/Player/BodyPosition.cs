using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPosition : MonoBehaviour
{
    public Transform cameraTr;
    private Vector3 PrevCamPos;
    private Vector3 CurrCamPos;
    private Vector3 deltaCamPos;
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.6f, transform.position.z);
    }

    void Update()
    {
        CurrCamPos = cameraTr.position;
        deltaCamPos = CurrCamPos - PrevCamPos;
        PrevCamPos = CurrCamPos;
        transform.position += deltaCamPos;
    }
}

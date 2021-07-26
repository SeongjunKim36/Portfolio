using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsulePlayer : MonoBehaviour


{
    private Transform tr;
    private Rigidbody rb;
    
    private float MoveSpeed = 300f;
    private float dirX;
    private float dirY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
    }

    private void Update()
    {
        CapsuleMove();
    }

    // 실제 Rigidbody 적용부분 이동
    void CapsuleMove()
    {
        dirX = Input.GetAxis("Horizontal");
        dirY = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(dirX, 0f, dirY) * Time.deltaTime * MoveSpeed;

    }
}

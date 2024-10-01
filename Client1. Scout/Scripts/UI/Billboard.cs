using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform camTr;
    private Transform tr;

    private float scale_x;
    private float scale_y;
    private WaitForSeconds sec = new WaitForSeconds(0.002f);

    void OnEnable()
    {
        StartCoroutine(OpenBoard());
    }

    void OnDisable()
    {
        StartCoroutine(CloseBoard());
    }
    void Start()
    {
        camTr = Camera.main.GetComponent<Transform>();
        tr = GetComponent<Transform>();
    }
    IEnumerator OpenBoard()
    {
        for (int i = 1; i <= 100; i+=4)
        {
            scale_x = 0.2f;
            scale_y = 0.01f * i;
            transform.localScale = new Vector3(scale_x, scale_y, 1f);
            yield return null;
        }
        for (int i = 20; i <= 100; i+=4)
        {
            scale_x = 0.01f * i;
            scale_y = 1f;
            transform.localScale = new Vector3(scale_x, scale_y, 1f);
            yield return null;
        }
    }

    IEnumerator CloseBoard()
    {
        for (int i = 100; i >= 20; i--)
        {
            scale_x = 0.01f * i;
            scale_y = 1f;
            transform.localScale = new Vector3(scale_x, scale_y, 1f);
            yield return null;
        }
        for (int i = 100; i >= 1; i--)
        {
            scale_x = 0.2f;
            scale_y = 0.01f * i;
            transform.localScale = new Vector3(scale_x, scale_y, 1f);
            yield return null;
        }

    }

    // Update is called once per frame
    void LateUpdate()
    {
        tr.LookAt(camTr.position);
    }
}

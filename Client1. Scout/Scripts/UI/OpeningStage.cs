using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OpeningStage : MonoBehaviour
{
    public GameObject welcome;
    public GameObject spark;
    public GameObject blockout;
    private Collider coll;
    void Start()
    {
        coll = GetComponent<Collider>();

        StartCoroutine(Opening());
        
    }

    public IEnumerator Opening()
    {
        yield return new WaitForSeconds(4.0f);
        welcome.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        coll.attachedRigidbody.useGravity = true;
        yield return new WaitForSeconds(2.5f);
        spark.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        blockout.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);


    }
}

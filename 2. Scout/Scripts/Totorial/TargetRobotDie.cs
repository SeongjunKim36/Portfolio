using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRobotDie : MonoBehaviour
{
    private Animator anim;
    private GameObject smoke;
    private GameObject spark;
    public Transform playerRig;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        smoke = transform.GetChild(0).gameObject;
        spark = transform.GetChild(1).gameObject;
        smoke.GetComponent<ParticleSystem>().Stop();
        spark.GetComponent<ParticleSystem>().Stop();
    }

    void Update()
    {
        transform.LookAt(playerRig.position);
    }

    private void OnTriggerEnter(Collider other) 
    {
        anim.SetTrigger("BattleDie");
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        smoke.GetComponent<ParticleSystem>().Play();
        Invoke("PlayDieParticle", 2.0f);
        TutorialManager.targetIndex++;
        Destroy(gameObject,2.2f);
    }

    void PlayDieParticle()
    {
        
        spark.GetComponent<ParticleSystem>().Play();
    }
}

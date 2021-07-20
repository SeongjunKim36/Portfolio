using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class GunAttack : MonoBehaviour
{
    public float randomPercent = 10;
    private Transform playerTr;

    void Start()
    {
        transform.GetComponent<AudioSource>().pitch *= 1 + Random.Range(-randomPercent / 100, randomPercent / 100);
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.transform.parent == null || other.transform.parent.parent == null)
            return;
        if (other.transform.parent.parent.CompareTag("BOSS"))
        {
            other.transform.parent.parent.gameObject.GetComponent<BossController>().hp--;

            if(other.gameObject.CompareTag("BOSSHEAD"))
            {
                other.transform.parent.parent.gameObject.GetComponent<BossController>().hp--;

                VisualEffect effect = other.transform.parent.Find("ironreaver03d").Find("HeadShot").GetComponent<VisualEffect>();
                
                effect.Play();
            }
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClawAttack : MonoBehaviour
{
    private bool active = false;

    private int ClawDamage;

    void OnEnable()
    {
        active = true;
    }

    void OnDisable()
    {
        active = false;
    }

    void Start()
    {
        ClawDamage = GameObject.FindGameObjectWithTag("BOSS").GetComponent<BossController>().ClawDamage;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && active)
        {
            other.gameObject.GetComponent<Player>().hp -= ClawDamage;
        }
    }
}

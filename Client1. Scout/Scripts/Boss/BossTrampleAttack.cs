using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrampleAttack : MonoBehaviour
{
    private int TrampleDamage;
    void Start()
    {
        TrampleDamage = GameObject.FindGameObjectWithTag("BOSS").GetComponent<BossController>().TrampleDamage;
        Force();
        Destroy(this.gameObject,5f);
    }


    void Force()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, 8f);
        foreach (Collider coll in colls)
        {
            //BlockMgr blockMgr = coll.GetComponent<BlockMgr>();

            if(coll.gameObject.CompareTag("DEBRIS") || coll.gameObject.CompareTag("Player"))
            {
                Player player = coll.GetComponent<Player>();
                Rigidbody rb = coll.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(1500.0f, transform.position, 8f, 0f);
                }
                if (player != null)
                {
                    player.hp -= TrampleDamage;
                }
            }
            
        }
     }
}

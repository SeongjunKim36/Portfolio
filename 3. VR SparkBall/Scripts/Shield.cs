using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 쉴드 컨트롤 스크립트
public class Shield : Photon.PunBehaviour
{
    private Vector3 curpos;
    private Vector3 oldpos;
    private Vector3 vel;
    private Vector3 r_dir;
    private Rigidbody hit_rigi;

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BALL"))
        {
            hit_rigi = coll.gameObject.GetComponent<Rigidbody>();
            Transform tr = this.gameObject.transform;
            hit_rigi.velocity = Vector3.forward * 30.0f;
            photonView.RPC("Rpc_Shiled", PhotonTargets.All, hit_rigi.velocity, tr.position, tr.rotation);

            this.gameObject.SetActive(false);
        }
    }


    [PunRPC]
    void Rpc_Shiled(Vector3 _hitVel, Vector3 _hitpos, Quaternion _hitrot)
    {
        GameObject ballInHand = GameObject.FindGameObjectWithTag("BALL").gameObject;

        ballInHand.transform.position = _hitpos;
        ballInHand.transform.rotation = _hitrot;
        hit_rigi.velocity = _hitVel;
    }

}

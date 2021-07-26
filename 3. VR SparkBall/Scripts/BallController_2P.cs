using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
public class BallController_2P : Photon.PunBehaviour

{

    private Transform ball_tr;
    private Rigidbody ball_rigi;
    private Rigidbody Hit_Ball;
    Vector3 lastVelocity;


    private Vector3 nowTr;
    private Vector3 oldTr;
    private float speed;


    public Collider col;
    public MeshRenderer mesh;
    public GameObject ball;
    //public GameObject stikeZone;

    public Text scoreText1P; 
    public Text scoreText2P;

    public Text superHot;
    private int Score2P;

    private HitBall Hit_Pose;
    private BallDissolve ballDissolve_2P;

    private bool Check_BdoyHitBall;
    void Start()
    {
        speed = 1.0f;
        ball_rigi = this.gameObject.GetComponent<Rigidbody>();
        ball_tr = gameObject.GetComponent<Transform>();
        ballDissolve_2P = gameObject.GetComponent<BallDissolve>();

        //테스트용. VR로 볼때는 애드포스 주석처리하고 시작!
        //ball_rigi.AddForce(Vector3.forward * 150.0f);


        oldTr = transform.position;

        Score2P = 0;
        SetCountText2P();
    
    }


    void Update()
    {
        lastVelocity = ball_rigi.velocity;

    }

    private void OnCollisionEnter(Collision coll)
    {

        if (coll.collider.CompareTag("FrontCOLL"))
        {
            Check_BdoyHitBall = true;
        }
        else if (coll.collider.CompareTag("BACKCOLL"))
        {
            Check_BdoyHitBall = false;
        }


        if (coll.collider.CompareTag("WALL"))
        {
            ContactPoint cp = coll.contacts[0];


            Transform wall_Tr = coll.gameObject.transform;

            lastVelocity = Vector3.Reflect(lastVelocity.normalized, cp.normal);
            ball_rigi.velocity = lastVelocity * 20.0f;
        }
        
        // 스트라이크존 타격시 (점수추가/슈퍼핫획득/공위치리셋)
        if (coll.collider.CompareTag("STRIKE2P"))
        {
            Score2P++;
            SetCountText2P();
            Debug.Log("2P STRIKE!"+ Score2P);
            resetBall();
         
        }
        // 상대방 타격시 (점수추가/공위치리셋)
        if (coll.collider.CompareTag("PLAYER1") && Check_BdoyHitBall == true)
        {
            Score2P ++;
            SetCountText2P();
            Debug.Log("2P HIT OPPONENT!" + Score2P);
            resetBall();

        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("KNUCKLE"))
        {
            Transform hitTr = other.gameObject.transform;

            Vector3 hitVel = other.gameObject.GetComponent<HitBall>().vel;
            photonView.RPC("Rpc_Hit", PhotonTargets.All, hitVel, hitTr.position, hitTr.rotation);
        }
    }


    [PunRPC]
    void Rpc_Hit(Vector3 _hitVel, Vector3 _hitpos, Quaternion _hitrot)
    {
        ball_tr.position = _hitpos;
        ball_tr.rotation = _hitrot;
        ball_rigi.velocity = _hitVel;
    }

    void resetBall()
    {

        mesh = this.gameObject.GetComponent<MeshRenderer>();
        mesh.enabled = false;
        ball_tr.position = new Vector3(3.5f, 9f, 39f);
        ball_rigi.velocity = lastVelocity * 0.0f;

        Debug.Log("2P RESET BALL!!!!");
        activateBall();
        ballDissolve_2P.Start_Value = 100;

    }

    void activateBall()
    {
        mesh.enabled = true;
        Debug.Log("2P RELOAD!!!!!");
    }

    void SetCountText2P()
    {
        scoreText1P.text = Score2P.ToString();
        scoreText2P.text = Score2P.ToString();
    }

}

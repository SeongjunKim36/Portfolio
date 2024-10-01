using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Forge3D;


public class BossAnim : MonoBehaviour
{
    private Transform target;
    private Transform bossTr;

    public GameObject claw_prefab;
    public GameObject laser_prefab;
    public GameObject rock_prefab;

    private Transform LowAttackClawPos;
    private Transform HighAttackClawPos;
    private Transform HighAttackLaserPos;
    private Transform LowAttackTramplePos;

    private BossClawAttack lHandCollider;
    private BossClawAttack rHandCollider;
    private GameObject rFootCollider;
    private GameObject rLegCollider;


    private AudioSource[] bossAudio;
    private BossSound bossSound;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        bossTr = transform.parent;
        lHandCollider = transform.Find("bone046").GetComponent<BossClawAttack>();
        rHandCollider = transform.Find("bone059").GetComponent<BossClawAttack>();
        rFootCollider = transform.Find("bone017").gameObject;
        rLegCollider = transform.Find("bone011").gameObject;
        LowAttackClawPos = transform.Find("LowAttackClawPos");
        HighAttackClawPos = transform.Find("HighAttackClawPos");
        HighAttackLaserPos = transform.Find("HighAttackLaserPos");
        LowAttackTramplePos = transform.Find("LowAttackTramplePos");

        bossAudio = transform.parent.GetComponents<AudioSource>();
        bossSound = transform.parent.GetComponent<BossSound>();
    }

    void AnimEnd()
    {
        //공격 형태 초기화
        GetComponentInParent<BossController>().attackStyle = 0;
        GetComponentInParent<BossController>().shortAttack = 0;
        GetComponentInParent<BossController>().endPattern = true;
        
    }
    
    void LowAttackClaw(int count)
    {
        //클로 공격 유효화
        lHandCollider.enabled = true;
        rHandCollider.enabled = true;

        if (count == 1)
            EffectPrefab(claw_prefab, LowAttackClawPos.GetChild(0));
        else if (count == 2)
            EffectPrefab(claw_prefab, LowAttackClawPos.GetChild(1));
        else if (count == 3)
            EffectPrefab(claw_prefab, LowAttackClawPos.GetChild(2));
        else if (count == 4)
            EffectPrefab(claw_prefab, LowAttackClawPos.GetChild(3));

        bossAudio[1].clip = bossSound.sound_claw;
        bossAudio[1].Play();
    }
    void ClawOff()
    {
        //클로 공격 무효화
        lHandCollider.enabled = false;
        rHandCollider.enabled = false;
    }

    void LowAttackTrample()
    {

        rFootCollider.SetActive(false);
        rLegCollider.SetActive(false);
        Instantiate(rock_prefab, LowAttackTramplePos.position, LowAttackTramplePos.rotation);

        bossAudio[1].clip = bossSound.sound_trample;
        bossAudio[1].Play();
    }
    void TrampleOff()
    {
        //발 콜라이더 on
        rFootCollider.SetActive(true);
        rLegCollider.SetActive(true);
    }

    void LowAttackCannon()
    {
        for (int i = 0; i < 2; i++)
        {
            transform.Find("bone024").Find("cannon").Find("MAIN").gameObject.GetComponent<F3DMissileLauncher>().ProcessInput(1);
        }
    }
    void HighAttackClaw(int count)
    {
        //클로 공격 유효화
        lHandCollider.enabled = true;
        rHandCollider.enabled = true;

        if (count == 1)
            EffectPrefab(claw_prefab, HighAttackClawPos.GetChild(0));
        else if (count == 2)
            EffectPrefab(claw_prefab, HighAttackClawPos.GetChild(1));

        bossAudio[1].clip = bossSound.sound_claw;
        bossAudio[1].Play();
    }
    void HighAttackLaser()
    {
        Instantiate(laser_prefab, HighAttackLaserPos.position, HighAttackLaserPos.rotation);
    }
    void HighAttackCannon()
    {
        StartCoroutine(GuidedCannon());
    }

    void EffectPrefab(GameObject effect,Transform tr)
    {
        Instantiate(claw_prefab, tr.position, tr.rotation);
    }

    IEnumerator GuidedCannon()
    {
        for(int i=0; i<5; i++)
        {
            transform.Find("bone024").Find("cannon").Find("MAIN").gameObject.GetComponent<F3DMissileLauncher>().ProcessInput(2);
            yield return new WaitForSeconds(0.15f);
        }
    }
    
    void DeadAnim()
    {
        Collider[] collider = GetComponentsInChildren<Collider>();

        foreach(Collider col in collider)
        {
            col.enabled = false;
        }
    }

    void StartAnim()
    {
        Collider[] collider = GetComponentsInChildren<Collider>();

        foreach (Collider col in collider)
        {
            col.enabled = true;
        }
    }

    
}

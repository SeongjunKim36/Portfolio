using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.Burst;

public class BossController : MonoBehaviour
{
    //보스 스테이터스
    public int hp = 500;
    private int maxhp;
    public int ClawDamage = 10;
    public int LaserDamage = 10;
    public int CannonDamage = 5;
    public int TrampleDamage = 20;
    
    //시작 후 변신시간
    public float startTime = 7f;

    private Transform target;
    private NavMeshAgent agent;
    private Animator anim;

    private bool loading = true;
    [HideInInspector]
    public bool endPattern = true;

    //보스와 타겟 거리
    private float bossToTarget;
    //타겟의 높이
    private float targetHigh;
    //공격 범위
    private float shortAttackRange = 8f;
    private float longAttackRange = 14f;

    //상단하단 공격패턴 정할때
    private float attackToTargetY = 4f;
    //Running 애니메이션이 종료되는 거리
    private float runningStopDis = 7f;
    
    //상태값
    private string state; //현재 작동 상태
    private string action; //다음 실행할 행동
    [HideInInspector]
    public int attackStyle = 0; //상단공격=1,하단공격=2
    [HideInInspector]
    public int shortAttack = 0; //근거리=1 , 장거리=2
    private bool isPhaseChange = false;
    private int prevHp;

    const string stateIdle = "Idle";
    const string stateHit = "Hit";
    const string stateRun = "Run";
    const string stateDead = "Dead";
    const string stateStart = "Start";
    const string stateAttack = "Attack";


    public Light sun;
    private AudioSource[] bossAudio;
    private BossSound bossSound;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        maxhp = hp;
        Invoke("StartWork", startTime);
        bossAudio = GetComponents<AudioSource>();
        bossSound = GetComponent<BossSound>();
        bossAudio[0].clip = bossSound.sound_base;
        bossAudio[0].Play();
    }
    void StartWork()
    {
        anim.SetBool("isStarting", true);
        loading = false;
    }

    [BurstCompile]
    void Update()
    {
        //보스 등장시간
        if (loading)
            return;

        //거리 체크
        Vector3 a = new Vector3(target.position.x, 0, target.position.z);
        Vector3 b = new Vector3(transform.position.x, 0, transform.position.z);
        bossToTarget = Vector3.Distance(a, b);

        //타겟 높낮이 체크
        targetHigh = target.position.y;

        //현재 상태값 체크
        StateCheck();

        //패턴 종료후 다음액션 실행
        if (action==stateAttack && !endPattern)
            return;
        
        //다음 행동 결정
        NextAction();

        switch (action)
        {
            case stateIdle:
                break;
            case stateHit:
                break;
            case stateDead:
                break;
            case stateRun:
                if (state == stateIdle || state == stateRun)
                {
                    Running();
                }
                break;
            case stateAttack:
                    StartCoroutine(Turnning());
                    if (shortAttack==1)
                    {
                        ShortAttack();
                    }
                    else if(shortAttack==2)
                    {
                        LongAttack();
                    }
                break;
        }
        endPattern = false;
    }
    
    
    void StateCheck()
    {
        //보스 2페이즈 진입
        if (!isPhaseChange && hp <= maxhp / 2)
        {
            StartCoroutine(PhaseChange());
            StartCoroutine(LightChange());

            StartCoroutine(DestroySound());
        }
        //Dead 체크
        else if (hp <= 0)
        {
            loading = true;
            agent.enabled = false;
            anim.SetBool("isStarting", false);
            AttackAnim("isDead");

            StartCoroutine(DestroySound());
        }
        //hit 체크
        else if (hp != prevHp && hp != maxhp && hp % (maxhp/10) == 0)
        {
            prevHp = hp;
            loading = true;
            agent.enabled = false;

            AttackAnim("Hit");
            Invoke("EndHit", 1.5f);

            bossAudio[1].loop = false;
            bossAudio[1].clip = bossSound.sound_hit;
            bossAudio[1].Play();
        }
        

        //현재 상태값
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            state = stateIdle;
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            state = stateHit;
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            state = stateRun;
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
            state = stateDead;
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Start"))
            state = stateStart;
        else
            state = stateAttack;
    }
    void EndHit()
    {
        agent.enabled = true;
        loading = false;
    }

    void NextAction()
    {
        //공격
        if (attackStyle == 0)
        {
            attackStyle = isPhaseChange ? Random.Range(1, 3) : Random.Range(1, 2);

            if (attackStyle == 1)
            {//근거리
             //스탑포인트 업데이트
                runningStopDis = shortAttackRange - 1f;
                //사거리가 닿을때
                if (bossToTarget <= shortAttackRange)
                {
                    action = stateAttack;
                }
                //사거리가 닿지 않을때
                else if (bossToTarget > shortAttackRange)
                {
                    action = stateRun;
                }
                shortAttack = 1;
            }
            else if (attackStyle == 2)
            {//원거리
             //스탑포인트 업데이트
                runningStopDis = longAttackRange - 1f;
                //사거리 닿을때
                if (bossToTarget <= longAttackRange)
                {
                    action = stateAttack;
                }
                //사거리가 닿지 않을때
                else if (bossToTarget > longAttackRange)
                {
                    action = stateRun;
                }
                shortAttack = 2;
            }
        }
    }

    void Running()
    {
        if (bossToTarget <= runningStopDis)
        {
            attackStyle = 0;
            shortAttack = 0;
            anim.SetBool("isRunning", false);
            StartCoroutine(StopWalk());

            bossAudio[1].loop = false;
        }
        else
        {
            if (bossAudio[1].clip != bossSound.sound_run)
            {
                bossAudio[1].clip = bossSound.sound_run;
                bossAudio[1].loop = true;

                bossAudio[1].Play();
            }
            agent.SetDestination(target.position);
            anim.SetBool("isRunning", true);
        }
    }

    IEnumerator StopWalk()
    { //애니메이션 멈추고 미끄러지는 현상 방지
        yield return new WaitForSeconds(0.25f);
        agent.velocity = Vector3.zero;
    }

    //제자리에서 플레이어를 향해 회전
    IEnumerator Turnning()
    {
        for (int i = 0; i < 60; i++)
        {
            Vector3 tmp = target.position - transform.position;
            tmp.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(tmp), Time.deltaTime * 3f);
            yield return null;
        }
    }

    void ShortAttack()
    {
        if (targetHigh >= attackToTargetY)
        {//상단 공격
            AttackAnim("HighAttack_Claw");
        }
        else
        {//하단 공격
            float attack = Random.Range(0, 2);
            if (attack == 0)
            {
                AttackAnim("LowAttack_Claw");
            }
            else if (attack == 1)
            {
                AttackAnim("LowAttack_Trample");
            }
        }
    }

    void LongAttack()
    {
        if (target.position.y >= attackToTargetY)
        {//상단 공격
            float attack = Random.Range(0, 2);
            if (attack == 0)
            {
                AttackAnim("HighAttack_Laser");
            }
            else if (attack == 1)
            {
                AttackAnim("HighAttack_Cannon");
            }
        }
        else
        {//하단 공격
            float attack = Random.Range(0, 2);
            if (attack == 0)
            {
                AttackAnim("LowAttack_Canon");
            }
            else if (attack == 1)
            {
                AttackAnim("HighAttack_Cannon");
            }
        }
    }

    IEnumerator PhaseChange()
    {
        loading = true;
        AttackAnim("isDead");
        agent.enabled = false;

        yield return new WaitForSeconds(6f);
        transform.GetChild(0).Find("ironreaver03_canon").gameObject.SetActive(true);
        yield return new WaitForSeconds(9f);

        agent.enabled = true;
        isPhaseChange = true;
        loading = false;
    }

    IEnumerator LightChange()
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);

        //yield return new WaitForSeconds(1f);

        for (int i = 255; i >= 0; i--)
        {//white >> black
            sun.color = new Color(i / 255f, i / 255f, i / 255f);
            yield return wait;
        }

        yield return new WaitForSeconds(2f);

        for (int i = 1; i <= 255; i++)
        {//black >> red
            sun.color = new Color(i / 255f, 1 / 255f, 1 / 255f);
            yield return wait;
        }
        for (int i = 1; i <= 255; i++)
        {//red >> white
            sun.color = new Color(1f, i / 255f, i / 255f);
            yield return wait;
        }
    }

    void AttackAnim(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    IEnumerator DestroySound()
    {
        yield return new WaitForSeconds(0.5f);

        bossAudio[0].mute = true;
        bossAudio[1].loop = false;
        bossAudio[1].clip = bossSound.sound_destroy;
        bossAudio[1].Play();

        yield return new WaitForSeconds(5f);
        
        if(hp > 0)
            bossAudio[0].mute = false;
    }

}

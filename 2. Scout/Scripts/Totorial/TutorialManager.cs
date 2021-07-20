using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TutorialManager : MonoBehaviour
{
    public SteamVR_Action_Boolean system = SteamVR_Actions.default_System;
    public SteamVR_Input_Sources anyhand = SteamVR_Input_Sources.Any;
    public SteamVR_Input_Sources lefthand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources righthand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_Trigger;
    public SteamVR_Action_Boolean touchpadClick = SteamVR_Actions.default_TouchPadClick;
    public SteamVR_Action_Boolean menu = SteamVR_Actions.default_Menu;

    public int tutorialIndex = 0;
    public int waypointIndex = 0;
    public int uiIndex = 0;
    private bool complete = true;
    private bool pass = false;

    public GameObject playerRig;
    private Transform cameraTr;
    public GameObject LeftLaserRope;
    public GameObject RightLaserRope;
    public GameObject gunInventory;

    public static int targetIndex = 0;
    public static int zoneEnterIndex = 0;
    public static int ropeTargetIndex = 0;
    public static int puzzleIndex = 9;
    
    private bool tutorialOn = false;

    private Animator anim;

    public GameObject tutorial;
    public GameObject tutorial14;

    public Vector3[] waypoints;

    private GameObject[] tutorialUI = new GameObject[16];

    private AudioSource sound;

    float scale_x;
    float scale_y;

    void Start()
    {
        playerRig.GetComponent<ArmSwing>().enabled = false;
        cameraTr = playerRig.transform.Find("Camera (eye)");
        LeftLaserRope.SetActive(false);
        RightLaserRope.SetActive(false);
        gunInventory.SetActive(false);

        anim = gameObject.GetComponent<Animator>();

        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            tutorialUI[i] = transform.GetChild(1).GetChild(i).gameObject;
            tutorialUI[i].SetActive(false);
        }
        anim.SetBool("Open_Anim", true);

        OpenUI();
        sound = gameObject.AddComponent<AudioSource>();
        sound.clip = Resources.Load("Done") as AudioClip;
    }

    void Update()
    {
        
        transform.LookAt(playerRig.transform.position);
        transform.position = waypoints[waypointIndex];
        //단계별 미션 완료 체크
        MissionCheck();
        
        if (!complete)
            return;
        
        //총쏘기
        if (tutorialIndex == 0 && (touchpadClick.GetStateDown(anyhand) || pass))
        {
            Debug.Log("총쏘기");
            
            CloseUI();
            OpenUI();
            
            tutorialIndex++;
        }
        else if (tutorialIndex == 1 && (touchpadClick.GetStateDown(anyhand) || pass))
        {
            CloseUI();
            OpenUI();

            tutorialIndex++;
        }
        //걷기
        else if (tutorialIndex == 2 && (touchpadClick.GetStateDown(anyhand) || pass))
        {
            Debug.Log("걷기");
            CloseUI();
            OpenUI();

            playerRig.GetComponent<ArmSwing>().enabled = true;
            tutorialIndex++;
        }
        else if (tutorialIndex == 3 && (touchpadClick.GetStateDown(anyhand) || pass))
        {
            CloseUI();
            waypointIndex++;
            OpenUI();

            tutorialIndex++;
           
        }
        //로프
        else if (tutorialIndex == 4 && (touchpadClick.GetStateDown(anyhand) || pass))
        {
            Debug.Log("로프");
            CloseUI();
            waypointIndex++;
            OpenUI();

            LeftLaserRope.SetActive(true);
            RightLaserRope.SetActive(true);
            tutorialIndex++;
        }
        
        else if (tutorialIndex == 5 && (touchpadClick.GetStateDown(anyhand) || pass))
        {
            CloseUI();
            OpenUI();

            tutorialIndex++;
        }
        else if (tutorialIndex == 6 && (touchpadClick.GetStateDown(anyhand) || pass))
        {
            CloseUI();
            waypointIndex++;
            OpenUI();

            tutorialIndex++;
        }
        //스카우트 창
        else if (tutorialIndex == 7 && (touchpadClick.GetStateDown(anyhand) || pass))
        {
            Debug.Log("스카우트 창");
            CloseUI();
            OpenUI();

            tutorialIndex++;
        }
        else if (tutorialIndex == 8 && (touchpadClick.GetStateDown(anyhand) || pass))
        {
            CloseUI();
            OpenUI();

            tutorialIndex++;
            
        }
        //포탈
        else if (tutorialIndex == 9 && (touchpadClick.GetStateDown(anyhand) || pass))
        {
            Debug.Log("포탈");
            
            CloseUI();
            OpenUI();

            gunInventory.SetActive(true);
            tutorialIndex++;
        }
        else if (tutorialIndex == 10 && (touchpadClick.GetStateDown(anyhand) || pass))
        {
            CloseUI();
            OpenUI();

            tutorialIndex++;
        }
        else if (tutorialIndex == 11 && (touchpadClick.GetStateDown(anyhand) || pass))
        {
            CloseUI();
            OpenUI();

            tutorialIndex++;
        }
        else if (tutorialIndex == 12 && (touchpadClick.GetStateDown(anyhand) || pass))
        {
            CloseUI();
            waypointIndex++;
            OpenUI();

            tutorialIndex++;
        }
        //아이템수집
        else if (tutorialIndex == 13 && (touchpadClick.GetStateDown(anyhand) || pass))
        {
            Debug.Log("아이템수집하기");
            CloseUI();
            waypointIndex++;
            OpenUI();

            tutorialIndex++;
        }
        else if (tutorialIndex == 14 && (touchpadClick.GetStateDown(anyhand) || pass))
        {
            Debug.Log("튜토리얼 종료");
            CloseUI();
            waypointIndex++;
            OpenUI();

        }

        pass = false;
    }



    void MissionCheck()
    {
        complete = false;

        if (tutorialIndex == 2)
        {
            if (targetIndex >= 3)
            {
                complete = true;
                pass = true;
                sound.Play();
            }
        }
        else if (tutorialIndex == 4)
        {
            if (zoneEnterIndex == 1)
            {
                complete = true;
                pass = true;
                sound.Play();
            }
                
        }
        else if (tutorialIndex == 7)
        {
            if (zoneEnterIndex == 2)
            {
                complete = true;
                pass = true;
                sound.Play();
            }
        }
        else if (tutorialIndex == 9)
        {
            if (menu.GetStateDown(anyhand))
            {
                complete = true;
                pass = true;
                sound.Play();
            }
        }
        else if (tutorialIndex == 13)
        {
            if (zoneEnterIndex == 3)
            {
                complete = true;
                pass = true;
                sound.Play();
            }  
        }
        else if (tutorialIndex == 14)
        {
            if (puzzleIndex <= 0)
            {
                complete = true;
                pass = true;
                sound.Play();
            }  
        }
        else
            complete = true;
    }

    void OpenUI()
    {
        StartCoroutine(OpenBoard());
        tutorialUI[uiIndex].SetActive(true);
    }

    void CloseUI()
    {
        StartCoroutine(CloseBoard());
        tutorialUI[uiIndex].SetActive(false);
        uiIndex++;
    }

    IEnumerator OpenBoard()
    {
        for (int i = 1; i <= 100; i += 6)
        {
            scale_x = 0.02f;
            scale_y = 0.001f * i;
            transform.GetChild(1).localScale = new Vector3(scale_x, scale_y, 0.1f);
            yield return null;
        }
        for (int i = 20; i <= 100; i += 6)
        {
            scale_x = 0.001f * i;
            scale_y = 0.1f;
            transform.GetChild(1).localScale = new Vector3(scale_x, scale_y, 0.1f);
            yield return null;
        }
    }

    IEnumerator CloseBoard()
    {
        for (int i = 100; i >= 20; i-=6)
        {
            scale_x = 0.001f * i;
            scale_y = 0.1f;
            transform.GetChild(1).localScale = new Vector3(scale_x, scale_y, 0.1f);
            yield return null;
        }
        for (int i = 100; i >= 1; i-=6)
        {
            scale_x = 0.02f;
            scale_y = 0.001f * i;
            transform.GetChild(1).localScale = new Vector3(scale_x, scale_y, 0.1f);
            yield return null;
        }

    }
}

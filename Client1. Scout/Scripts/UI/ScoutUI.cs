using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;


public class ScoutUI : MonoBehaviour
{

    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Action_Boolean menu = SteamVR_Actions.default_Menu;
    public SteamVR_Action_Boolean touchpadClick = SteamVR_Actions.default_TouchPadClick;

    private Player playerHP;
    private BossController bossHP;
    private Transform playerTr;
    private Transform bossTr;


    public Image playerBar;
    public Image bossBar;

    //public GameObject enemyDetected;
    public GameObject UIs;

    public bool scoutUIActivation = false;


    public float distance;
    public float closeDistance = 5.0f;

    private void Start()
    {
        // enemyDetected.SetActive(false);

        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        bossHP = GameObject.FindGameObjectWithTag("BOSS").GetComponent<BossController>();
        playerTr = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        bossTr = GameObject.FindGameObjectWithTag("BOSS").gameObject.transform;

        UIs.gameObject.SetActive(false);

    }

    void Update()
    {


        scoutActivate();
        // bossdetection();
        hpUpdate();
    }

    void scoutActivate()
    {
        // UIs = GetComponentInChildren<GameObject>();

        // if(Input.GetKeyDown(KeyCode.Space))
        if (touchpadClick.GetStateDown(hand))
        {
            scoutUIActivation = true;
            //Debug.Log(scoutUIActivation + "ScoutUI on");
            UIs.gameObject.SetActive(true);



        }

        else if (touchpadClick.GetStateUp(hand))
        {
            scoutUIActivation = false;
            //Debug.Log(scoutUIActivation + "ScoutUI off");
            UIs.gameObject.SetActive(false);
        }
    }

    void hpUpdate()
    {
        float pHP = playerHP.hp / 100f;
        playerBar.fillAmount = pHP;


        float eHP = bossHP.hp / 500f;
        bossBar.fillAmount = eHP; ;

        //Debug.Log(pHP);
        //Debug.Log(eHP);
    }

    /* void bossdetection()
     {
             Vector3 offset = bossTr.position - playerTr.position;
             float sqrLen = offset.sqrMagnitude;

             if (sqrLen < closeDistance * closeDistance && scoutUIActivation == true)
             {

                 Debug.Log("Boss profile UI Activated");
                 enemyDetected.gameObject.SetActive(true);
             }
             else
             {
                 Debug.Log("Boss profile UI Dectivated");
                 enemyDetected.gameObject.SetActive(false);

             }
         }

     */
}
    
    /* distance method
    { 
        distance = Vector3.Distance(player.transform.position, boss.transform.position);

        if(distance <3)
        {
            Debug.Log("Boss profile UI Activated");
        }
    } 
    */




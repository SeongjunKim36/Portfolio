using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{
   
    public Transform Rocks; //물리공격용 장애물 또는 보상 프리팹
    public Transform spawnPoint;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BUTTON")
        {
            Debug.Log("SPAWNER BTN TAGGED");
            Transform t = Instantiate(Rocks);
            t.position = spawnPoint.position;
        }
    }
}

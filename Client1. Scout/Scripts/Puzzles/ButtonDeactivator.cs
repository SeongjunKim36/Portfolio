using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDeactivator : MonoBehaviour
{
   
    public Transform Barrier; //물리공격용 장애물 또는 보상 프리팹



    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "BUTTON")
        {
            Barrier.gameObject.SetActive(false);

        }
    }

    private void OnTriggerExit(Collider other)
        {
            if (other.tag == "BUTTON")
            {
                Barrier.gameObject.SetActive(true);

            }
        }
}

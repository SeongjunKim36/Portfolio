using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleItems : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            transform.parent.GetComponent<PuzzleManager>().AddPuzzle();
            Destroy(this.gameObject);
        }
    }
}

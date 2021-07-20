using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int maxPuzzleCount;
    public int puzzleCount;
    
    public GameObject barrier;

    private AudioSource sound;
    public AudioClip sound_puzzle;
    public AudioClip sound_barrier;
    void Start()
    {
        sound = GetComponent<AudioSource>();
        maxPuzzleCount = transform.childCount;
        puzzleCount = maxPuzzleCount - transform.childCount;
    }

    public void AddPuzzle()
    {
        StartCoroutine(PuzzleCount());
    }
    IEnumerator PuzzleCount()
    {
        yield return new WaitForSeconds(0.1f);
        
        puzzleCount = maxPuzzleCount - transform.childCount;

        if (puzzleCount >= maxPuzzleCount)
        {
            barrier.GetComponent<BreakableWindow>().enabled = true;
            barrier.transform.GetChild(0).gameObject.SetActive(false);

            sound.clip = sound_barrier;
            sound.Play();
        }
        else
        {
            sound.clip = sound_puzzle;
            sound.Play();
            Debug.Log(puzzleCount + " / " + maxPuzzleCount);
        }

        TutorialManager.puzzleIndex = transform.childCount;

    }
}

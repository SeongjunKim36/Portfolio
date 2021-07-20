using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDebris : MonoBehaviour
{
    private AudioSource sound;

    void Start()
    {
        sound = gameObject.AddComponent<AudioSource>();
        sound.clip = Resources.Load("Destroy_Structure") as AudioClip;
        sound.Play();
        Destroy(this.gameObject, 6f);
    }

    
}

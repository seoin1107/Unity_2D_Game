using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance { get; private set; }

    public AudioClip BoosRoomBgm;
    //public AudioClip ClearBgm;
    AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f;
        audioSource.loop = true;

        PlaySound(BoosRoomBgm);
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void StopSound()
    { 
        audioSource.Stop();
    }
}

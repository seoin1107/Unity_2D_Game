using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }

    public AudioClip playerAttack;
    public AudioClip playerParry;
    public AudioClip playerDead;
    public AudioClip playerDamage;
    public AudioClip playerJump;

/*    public AudioClip monsterAttack;
    public AudioClip monsterDead;
    public AudioClip monsterDamage;*/
    AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.3f;
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}

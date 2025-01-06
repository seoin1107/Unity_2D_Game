using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EFFECTManager : MonoBehaviour
{
    public static EFFECTManager Instance { get; private set; }

    public AudioClip GhoulAttack;
    public AudioClip EffectAttack;
    public AudioClip lighting;

    public AudioClip BossAttack;
    public AudioClip BossDead;

    public AudioClip DarkBringerAttak;
    public AudioClip DarkBringerAttak_Hard;

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

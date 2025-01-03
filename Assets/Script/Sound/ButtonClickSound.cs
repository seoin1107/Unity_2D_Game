using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // EventTrigger를 사용하기 위해 필요

public class ButtonClickSound : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public AudioClip sound1; 
    public AudioClip sound2; 


    private AudioSource audioSource; // 오디오 소스

    void Start()
    {
        // AudioSource 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
    }
    // 버튼을 누를 때 호출
    public void OnPointerDown(PointerEventData eventData)
    {
        PlaySound(sound1);
    }

    // 버튼에서 뗄 때 호출
    public void OnPointerUp(PointerEventData eventData)
    {
        PlaySound(sound2);
    }

    // 사운드 재생 함수
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip); // 지정된 클립 재생
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // EventTrigger�� ����ϱ� ���� �ʿ�

public class ButtonClickSound : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public AudioClip sound1; 
    public AudioClip sound2; 


    private AudioSource audioSource; // ����� �ҽ�

    void Start()
    {
        // AudioSource ������Ʈ ��������
        audioSource = GetComponent<AudioSource>();
    }
    // ��ư�� ���� �� ȣ��
    public void OnPointerDown(PointerEventData eventData)
    {
        PlaySound(sound1);
    }

    // ��ư���� �� �� ȣ��
    public void OnPointerUp(PointerEventData eventData)
    {
        PlaySound(sound2);
    }

    // ���� ��� �Լ�
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip); // ������ Ŭ�� ���
        }
    }
}

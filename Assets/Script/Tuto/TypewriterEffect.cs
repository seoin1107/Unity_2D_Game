using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    //NPC대화 타이핑하듯 보이는 효과
    [SerializeField]
    private float typewriterSpeed = 4.0f;

    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        return StartCoroutine(TypeText(textToType, textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty; //시작시 빈 상자


        float t = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            t += Time.deltaTime + typewriterSpeed; //타이핑 속도
            charIndex = Mathf.FloorToInt(t); //속도의 소숫점 버림
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            textLabel.text = textToType.Substring(0, charIndex);

            yield return null;
        }
        textLabel.text = textToType;
    }
}

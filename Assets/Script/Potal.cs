using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Potal : MonoBehaviour
{
    public GameObject targetObj;
    public GameObject toObj;
    public Image Cover;
    float time = 0.0f;
    float F_time = 1.0f;
    bool IsTeleport = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            targetObj = collision.gameObject;
            StartCoroutine(PotalRoutine());
        }
    }

    //ÇÑ ¾À ³»ÀÇ Æ÷Å» ½Ã½ºÅÛ
    IEnumerator PotalRoutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                yield return StartCoroutine(FadeIn());
                //Æ÷Å»ÀÌµ¿
                targetObj.transform.position = toObj.transform.position;

                yield return StartCoroutine(FadeOut());
            }
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        Cover.gameObject.SetActive(true);
        IsTeleport = true;
        time = 0f;
        Color alpha = Cover.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Cover.color = alpha;
            yield return null;
        }
        time = 0f;
    }
    IEnumerator FadeOut() 
    {
        Color alpha = Cover.color;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Cover.color = alpha;
            yield return null;

        }
        Cover.gameObject.SetActive(false);
        IsTeleport = false;
        yield return null;
    }
}

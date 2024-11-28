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
    float F_time = 0.7f;
    public bool IsTeleport { get; private set; }
    private bool Inpotal = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            targetObj = collision.gameObject;
            Inpotal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Inpotal = false;
        }
    }

    private void Update()
    {
        if (Inpotal && !IsTeleport && Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(PotalRoutine());
        }
    }

    //ÇÑ ¾À ³»ÀÇ Æ÷Å» ½Ã½ºÅÛ
    IEnumerator PotalRoutine()
    {
        IsTeleport = true;
        yield return StartCoroutine(FadeOut());

        //Æ÷Å»ÀÌµ¿
        targetObj.transform.position = toObj.transform.position ;

        yield return new WaitForSeconds(1.0f);

        yield return StartCoroutine(FadeIn());

        yield return new WaitForSeconds(F_time);

        IsTeleport = false;
    }

    IEnumerator FadeOut()
    {
        Cover.gameObject.SetActive(true);
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
    IEnumerator FadeIn() 
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public Image Cover;
    float time = 0.0f;
    float F_time = 0.7f;
    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }
    IEnumerator FadeFlow()
    {
        Cover.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Cover.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0,1,time);
            Cover.color = alpha;
            yield return null;
        }
        time = 0f;

        yield return new WaitForSeconds(0.5f);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1,0,time);
            Cover.color = alpha;
            yield return null;
            
        }
        Cover.gameObject.SetActive(false);
        yield return null;
    }

}

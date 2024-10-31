using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public Image Cover;
    float time = 0.0f;
    float F_time = 1.0f;
    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }
    IEnumerator FadeFlow()
    {
        float a = 1.0f;
        time = 0.0f;
        Cover.gameObject.SetActive(true);
        Cover.color = new Vector4(0, 0, 0, a);

        //while(a < 1.0f)
        //{
        //    time = Time.deltaTime / F_time;
        //    a = Mathf.Lerp(0, 1, time);
        //    Cover.color = alpha;
        //}
        //time = 0.0f;

        yield return new WaitForSeconds(0.7f);

        while (a >= 0.0f)
        {
            Cover.color = new Vector4(0, 0, 0, a);
            a -= 0.02f;
            yield return null;
            //time = Time.deltaTime / F_time;
            //alpha.a = Mathf.Lerp(1, 0, time);
            //Cover.color = alpha;
        }
        Cover.gameObject.SetActive(false);
        yield return null;
    }

}

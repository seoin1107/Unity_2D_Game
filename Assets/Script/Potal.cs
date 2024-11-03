using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Potal : FadeInOut
{
    public GameObject targetObj;
    public GameObject toObj;
    public float delay = 0.5f;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            targetObj = collision.gameObject;
            StartCoroutine(PotalRoutine());
        }
    }

    //public void PotalSystem()
    //{
    //    if (Input.GetKeyDown(KeyCode.G))
    //    {
    //        StartCoroutine(PotalRoutine());
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StopAllCoroutines();
        }
    }

    //ÇÑ ¾À ³»ÀÇ Æ÷Å» ½Ã½ºÅÛ
    IEnumerator PotalRoutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                //Æ÷Å»ÀÌµ¿
                targetObj.transform.position = toObj.transform.position + Vector3.down * 1.5f;
            }
            yield return null;
        }
    }

    //¾À°£ÀÇ ÀÌµ¿
    public void ScenesCtrl()
    {
        SceneManager.LoadScene("Base");
    }
}

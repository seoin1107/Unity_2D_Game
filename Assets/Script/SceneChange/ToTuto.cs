using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Totuto : MonoBehaviour
{
    public void GameStart()
    {
        StartCoroutine(ChangeScene());


    }
    IEnumerator ChangeScene()
    {
        while (true)
        {
            
                yield return new WaitForSeconds(0.7f);
                Loading.nScene = 0;
                SceneManager.LoadScene(1);
            
            yield return null;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StopAllCoroutines();
        }
    }
}

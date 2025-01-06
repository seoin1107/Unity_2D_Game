using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToBase : MonoBehaviour
{
    //public BattleSystem2D battleSystem2d;
    //public CharacterStatus characterStat;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(ChangeScene());
        }
    }
    IEnumerator ChangeScene()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                yield return new WaitForSeconds(0.7f);
                
                Loading.nScene = 2;
                SceneManager.LoadScene(1);
            }            
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

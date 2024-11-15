using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToBase : MonoBehaviour
{
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
                SceneManager.LoadScene("2D_GAME_Origin");
            }
            yield return null;
        }
    }
}

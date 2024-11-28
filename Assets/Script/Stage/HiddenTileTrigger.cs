using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenTileTrigger : MonoBehaviour
{
    public GameObject targetObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            targetObject.SetActive(true);
        }
    }
}

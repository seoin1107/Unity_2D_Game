using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoSign : MonoBehaviour
{
    public GameObject tatgetOB;
    public Image myImage1;
    public Image myImage2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            myImage1.gameObject.SetActive(true);
            myImage2.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            myImage1.gameObject.SetActive(false);
            myImage2.gameObject.SetActive(false);
        }
    }

}

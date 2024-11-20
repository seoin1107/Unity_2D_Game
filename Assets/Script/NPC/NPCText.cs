using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCText : MonoBehaviour
{
    //ø©±‚∞° NPC∏‡∆Æ
    [SerializeField] private TMP_Text textLabel;

    void Start()
    {
        
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    GetComponent<TypewriterEffect>().Run("Hello, asdfasfa kanskldfn asdfas" +
        //    "\nadafasdf", textLabel);
        //}
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.layer == LayerMask.NameToLayer("Player") && Input.GetKeyDown(KeyCode.G))
    //    {
    //        GetComponent<TypewriterEffect>().Run("Hello, asdfasfa kanskldfn asdfas" +
    //        "\nadafasdf", textLabel);
    //    }
    //}
}

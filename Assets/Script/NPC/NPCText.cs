using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCText : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;

    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GetComponent<TypewriterEffect>().Run("Hello, asdfasfa kanskldfn asdfas" +
            "\nadafasdf", textLabel);
        }
    }
}

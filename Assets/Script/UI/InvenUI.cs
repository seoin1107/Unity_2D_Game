using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class InvenUI : MonoBehaviour
{

    public static InvenUI Instance
    {
        get; set;
    }

    public Card[] equipCard = new Card[3];


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InvenUI : MonoBehaviour
{
    public CharacterStatus characterStatus;

    public static InvenUI Instance
    {
        get; set;
    }

    public Card[] equipCard = new Card[3] ;

    public CardInventory cardInven;


    public void GetEqiupCard()
    {
         
    }

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

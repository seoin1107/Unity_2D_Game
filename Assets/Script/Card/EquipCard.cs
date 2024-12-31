using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipCard : CardIcon
{
    public CharacterStatus player;
    public CardData[] cardDataList;
    public CardSlot[] equipCard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame  
    void Update()
    {
        
    }

    void CheckEquipeCard()
    {
        //player.equipCard[0].myChild.GetComponent<CardIcon>().myData
    }
}

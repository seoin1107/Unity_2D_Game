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

    public void CheckEquipeCard(int i)
    {
        if (i != 0)
        {
            
            player.characterStat.addAtk += CardInventory.Instance.cardDataList[i-1].atk_add;
            player.characterStat.addHp += CardInventory.Instance.cardDataList[i-1].hp_add;

        }
    }
}

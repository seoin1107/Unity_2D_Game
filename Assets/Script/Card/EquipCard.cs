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
            player.characterStat.mulAtk += CardInventory.Instance.cardDataList[i - 1].atk_per;
            player.characterStat.mulHp += CardInventory.Instance.cardDataList[i - 1].hp_per;
            player.characterStat.skillCool += CardInventory.Instance.cardDataList[i - 1].skill_cool;
            player.characterStat.skillDamage += CardInventory.Instance.cardDataList[i - 1].skill_dm;
            player.characterStat.hitRecover += CardInventory.Instance.cardDataList[i - 1].hit_re;
            player.characterStat.hpRegen += CardInventory.Instance.cardDataList[i - 1].hp_re;
            player.characterStat.drain += CardInventory.Instance.cardDataList[i - 1].atk_ab;



        }
    }
}

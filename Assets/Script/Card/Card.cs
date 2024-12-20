using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Card : CardData
{


    public CardData cardData;

    public Card[] cardList = new Card[12];
    void SetCardData(int in_cardNo, int in_cardRarity, float in_hp_per, float in_atk_per, float in_hp_add, float in_atk_add, float in_hp_re,
        float in_atk_ab, float in_hit_re, float in_skill_dm, float in_skill_cool)
    {
        cardNo = in_cardNo;
        cardRarity = in_cardRarity;
        hp_per = in_hp_per;
        atk_per = in_atk_per;
        hp_add = in_hp_add;
        atk_add = in_atk_add;
        hp_re = in_hp_re;
        atk_ab = in_atk_ab;
        hit_re = in_hit_re;
        skill_dm = in_skill_dm;
        skill_cool = in_skill_cool;
    }

    public void InitializeCard()
    {
        cardList[0].SetCardData (1, 1, 0, 0, 0, 5, 0, 0, 0, 0, 0);
        cardList[1].SetCardData (2, 1, 0, 0, 10, 0, 0, 0, 0, 0, 0);
        cardList[2].SetCardData (3, 1, 0, 0, 0, 0, 0.01f, 0, 0, 0, 0);

        cardList[3].SetCardData (4, 2, 0, 0, 0, 0, 0, 0.03f, 0, 0, 0);
        cardList[4].SetCardData (5, 2, 0, 0, 0, 0, 0, 0, 0.1f, 0, 0);
        cardList[5].SetCardData (6, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0.25f);

        cardList[6].SetCardData (7, 3, 0.2f, 0.2f, 0, 0, 0, 0, 0, 0, 0); 
        cardList[7].SetCardData (8, 3, 0, 0, 0, 0, 0.05f, 0.2f, 0, 0, 0);

        cardList[8].SetCardData (9, 4, 0, 0, 10, 0, 0, 0, 0, -0.5f, 1);
        cardList[9].SetCardData (10, 4, 1, -0.5f, 10, 0, 0, 0, 0, 0, 0);
        cardList[10].SetCardData(11, 4, -0.5f, 1, 10, 0, 0, 0, 0, 0, 0);
        cardList[11].SetCardData(12, 4, 0, 0, 10, 0, 0, 0, 0, 1, -0.5f);

    }


    private void Start()
    {
        

    }


}

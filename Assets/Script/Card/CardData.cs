using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



[CreateAssetMenu(fileName = "CardData", menuName = "Card/CardData", order = 1)]

public class CardData : ScriptableObject
{
    public Sprite icon;
    //카드 정보
    public int cardNo;
    public int cardRarity;
    //카드 효과
    public float hp_per;         //체력%
    public float atk_per;        //공격력%
    public float hp_add;         //체력+
    public float atk_add;        //공격력+
    public float hp_re;          //체력 재생
    public float atk_ab;         //공격시 흡혈
    public float hit_re;         //피격무적
    public float skill_dm;       //스킬 데미지
    public float skill_cool;      //스킬 쿨타임


}

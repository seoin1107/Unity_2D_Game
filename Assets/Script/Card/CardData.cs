using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



[CreateAssetMenu(fileName = "CardData", menuName = "/CardData", order = 1)]

public class CardData : ScriptableObject
{
    public Sprite icon;
    //카드 정보
    protected int cardNo;
    protected int cardRarity;
    //카드 효과
    protected  float hp_per;         //체력%
    protected float atk_per;        //공격력%
    protected float hp_add;         //체력+
    protected float atk_add;        //공격력+
    protected float hp_re;          //체력 재생
    protected float atk_ab;         //공격시 흡혈
    protected float hit_re;         //피격무적
    protected float skill_dm;       //스킬 데미지
    protected float skill_cool;      //스킬 쿨타임


}

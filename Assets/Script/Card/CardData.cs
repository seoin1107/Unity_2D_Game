using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



[CreateAssetMenu(fileName = "CardData", menuName = "Card/CardData", order = 1)]

public class CardData : ScriptableObject
{
    public Sprite icon;
    //ī�� ����
    public int cardNo;
    public int cardRarity;
    //ī�� ȿ��
    public float hp_per;         //ü��%
    public float atk_per;        //���ݷ�%
    public float hp_add;         //ü��+
    public float atk_add;        //���ݷ�+
    public float hp_re;          //ü�� ���
    public float atk_ab;         //���ݽ� ����
    public float hit_re;         //�ǰݹ���
    public float skill_dm;       //��ų ������
    public float skill_cool;      //��ų ��Ÿ��


}

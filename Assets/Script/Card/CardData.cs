using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



[CreateAssetMenu(fileName = "CardData", menuName = "/CardData", order = 1)]

public class CardData : ScriptableObject
{
    public Sprite icon;
    //ī�� ����
    protected int cardNo;
    protected int cardRarity;
    //ī�� ȿ��
    protected  float hp_per;         //ü��%
    protected float atk_per;        //���ݷ�%
    protected float hp_add;         //ü��+
    protected float atk_add;        //���ݷ�+
    protected float hp_re;          //ü�� ���
    protected float atk_ab;         //���ݽ� ����
    protected float hit_re;         //�ǰݹ���
    protected float skill_dm;       //��ų ������
    protected float skill_cool;      //��ų ��Ÿ��


}

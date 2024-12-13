using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardEffectType
{
    HP_PERCENT,        // 1ü�� %
    ATK_PERCENT,       // 2���ݷ� %
    HP_PLUS,           // 3ü�� +
    ATK_PLUS,          // 4���ݷ� +
    SKILL_DAMAGE,      // 5��ų ������
    SKILL_COOL,        // 6��ų ��Ÿ��
    HP_REGEN,          // 7ü�� ���
    LIFESTEAL,         // 8����
    INVINCIBLE_ON_HIT, // 9�ǰ� ����
    ATTACK_SPEED       // 10���ݼӵ�
}

public class CardData : MonoBehaviour
{
    public int cardNo;              // ī�� ��ȣ
    public int cardRare;            // ī�� ��͵�
    public CardEffectType[] effectTypes; // ī���� ȿ�� ���� (�ϳ� �̻��� ȿ���� ����)
    public float[] effectValues;    // ī���� ȿ�� �� (effectTypes �迭�� ���� �����)

    // ī�� ������
    public CardData(int no, int rare, CardEffectType[] types, float[] values)
    {
        cardNo = no;
        cardRare = rare;
        effectTypes = types;
        effectValues = values;
    }
}

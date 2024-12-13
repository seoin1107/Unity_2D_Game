using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardEffectType
{
    HP_PERCENT,        // 1체력 %
    ATK_PERCENT,       // 2공격력 %
    HP_PLUS,           // 3체력 +
    ATK_PLUS,          // 4공격력 +
    SKILL_DAMAGE,      // 5스킬 데미지
    SKILL_COOL,        // 6스킬 쿨타임
    HP_REGEN,          // 7체력 재생
    LIFESTEAL,         // 8흡혈
    INVINCIBLE_ON_HIT, // 9피격 무적
    ATTACK_SPEED       // 10공격속도
}

public class CardData : MonoBehaviour
{
    public int cardNo;              // 카드 번호
    public int cardRare;            // 카드 희귀도
    public CardEffectType[] effectTypes; // 카드의 효과 종류 (하나 이상의 효과를 가짐)
    public float[] effectValues;    // 카드의 효과 값 (effectTypes 배열에 맞춰 적용됨)

    // 카드 생성자
    public CardData(int no, int rare, CardEffectType[] types, float[] values)
    {
        cardNo = no;
        cardRare = rare;
        effectTypes = types;
        effectValues = values;
    }
}

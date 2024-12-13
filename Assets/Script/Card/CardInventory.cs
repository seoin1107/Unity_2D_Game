using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInventory : MonoBehaviour
{
    private List<CardData> allCards = new List<CardData>(); // 전체 카드 목록
    private List<CardData> equippedCards = new List<CardData>(); // 장착된 카드 목록
    private const int maxCards = 3; // 최대 3개의 카드 장착 가능

    // 카드 장착
    public bool EquipCard(int cardNo)
    {
        CardData card = GetCardByNumber(cardNo);
        if (card != null && equippedCards.Count < maxCards)
        {
            equippedCards.Add(card);
            return true;
        }
        else
        {
             return false;
        }
    }

    // 카드 해제
    public void UnequipCard(int cardNo)
    {
        CardData card = GetCardByNumber(cardNo);
        if (card != null)
        {
            equippedCards.Remove(card);
        }
    }

    // 카드 번호로 카드 찾기
    public CardData GetCardByNumber(int cardNo)
    {
        return allCards.Find(card => card.cardNo == cardNo); // 카드 번호로 카드 찾기
    }

    // 장착된 카드들의 효과를 캐릭터 상태에 적용
    public void ApplyCardEffects(CharacterStatus characterStatus, CardData card)
    {
        for (int i = 0; i < card.effectTypes.Length; i++)
        {
            // 카드 효과를 캐릭터에 반영하는 로직
            ApplyCardEffect(characterStatus, card.effectTypes[i], card.effectValues[i]);
        }
    }

    // 카드 효과를 캐릭터 스탯에 적용
    private void ApplyCardEffect(CharacterStatus characterStatus, CardEffectType effectType, float effectValue)
    {
        switch (effectType)
        {
            case CardEffectType.HP_PERCENT:
                characterStatus.characterStat.maxHP += characterStatus.characterStat.baseHP * (effectValue / 100);
                break;

            case CardEffectType.ATK_PERCENT:
                characterStatus.characterStat.totalAtk += characterStatus.characterStat.baseAtk * (effectValue / 100);
                break;

            case CardEffectType.HP_PLUS:
                characterStatus.characterStat.maxHP += effectValue;
                break;

            case CardEffectType.ATK_PLUS:
                characterStatus.characterStat.totalAtk += effectValue;
                break;

            case CardEffectType.SKILL_DAMAGE:
                characterStatus.characterStat.skillDamage += effectValue;
                break;

            case CardEffectType.SKILL_COOL:
                characterStatus.characterStat.skillCool -= effectValue;  // 스킬 쿨타임 감소
                if (characterStatus.characterStat.skillCool < 0) characterStatus.characterStat.skillCool = 0; // 최소 0으로 제한
                break;

            case CardEffectType.HP_REGEN:
                characterStatus.characterStat.hpRegen += effectValue;
                break;

            case CardEffectType.LIFESTEAL:
                characterStatus.characterStat.drain += effectValue;
                break;

            case CardEffectType.INVINCIBLE_ON_HIT:
                characterStatus.characterStat.hpOption1 = true;  // 예: 피격시 일정 시간 동안 무적 (옵션 1을 true로 설정)
                break;

            case CardEffectType.ATTACK_SPEED:
                characterStatus.characterStat.atkSpeed += effectValue;
                break;
        }

        // 스탯 갱신
        characterStatus.UpdateStatus();
    }

    // 카드 인벤에 카드 추가
    public void AddCard(CardData card)
    {
        allCards.Add(card);
    }
}

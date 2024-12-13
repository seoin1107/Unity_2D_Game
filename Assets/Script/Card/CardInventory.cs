using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInventory : MonoBehaviour
{
    private List<CardData> allCards = new List<CardData>(); // ��ü ī�� ���
    private List<CardData> equippedCards = new List<CardData>(); // ������ ī�� ���
    private const int maxCards = 3; // �ִ� 3���� ī�� ���� ����

    // ī�� ����
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

    // ī�� ����
    public void UnequipCard(int cardNo)
    {
        CardData card = GetCardByNumber(cardNo);
        if (card != null)
        {
            equippedCards.Remove(card);
        }
    }

    // ī�� ��ȣ�� ī�� ã��
    public CardData GetCardByNumber(int cardNo)
    {
        return allCards.Find(card => card.cardNo == cardNo); // ī�� ��ȣ�� ī�� ã��
    }

    // ������ ī����� ȿ���� ĳ���� ���¿� ����
    public void ApplyCardEffects(CharacterStatus characterStatus, CardData card)
    {
        for (int i = 0; i < card.effectTypes.Length; i++)
        {
            // ī�� ȿ���� ĳ���Ϳ� �ݿ��ϴ� ����
            ApplyCardEffect(characterStatus, card.effectTypes[i], card.effectValues[i]);
        }
    }

    // ī�� ȿ���� ĳ���� ���ȿ� ����
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
                characterStatus.characterStat.skillCool -= effectValue;  // ��ų ��Ÿ�� ����
                if (characterStatus.characterStat.skillCool < 0) characterStatus.characterStat.skillCool = 0; // �ּ� 0���� ����
                break;

            case CardEffectType.HP_REGEN:
                characterStatus.characterStat.hpRegen += effectValue;
                break;

            case CardEffectType.LIFESTEAL:
                characterStatus.characterStat.drain += effectValue;
                break;

            case CardEffectType.INVINCIBLE_ON_HIT:
                characterStatus.characterStat.hpOption1 = true;  // ��: �ǰݽ� ���� �ð� ���� ���� (�ɼ� 1�� true�� ����)
                break;

            case CardEffectType.ATTACK_SPEED:
                characterStatus.characterStat.atkSpeed += effectValue;
                break;
        }

        // ���� ����
        characterStatus.UpdateStatus();
    }

    // ī�� �κ��� ī�� �߰�
    public void AddCard(CardData card)
    {
        allCards.Add(card);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public struct CardData
{
    public string cardName;
    public int cardNumber;
    public int cardGrade;
    public string cardEffect;
}

public class Card : MonoBehaviour
{

    public CardData[] cardList = new CardData[100];



}

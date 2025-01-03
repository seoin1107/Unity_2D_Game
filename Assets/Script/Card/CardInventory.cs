using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInventory : MonoBehaviour
{
    public CharacterStatus player;
    public static CardInventory Instance
    {
        get; private set;
    }

    public CardSlot[] mySlots;

    public CardData[] cardDataList;
    CardSlot FindBlankSlot()
    {
        int[] excludedSlots = { 0,1,2 }; 

        foreach (CardSlot s in mySlots)
        {
            int index = Array.IndexOf(mySlots, s);
            if (Array.Exists(excludedSlots, excludedIndex => excludedIndex == index))
            {
                continue; 
            }

            if (s.myChild == null)
            {
                return s;
            }
        }
        return null; 
    }

    public void GetCardInfoFromSlots()
    {
        int[] targetSlots = { 0, 1, 2 }; 
        foreach (int slotIndex in targetSlots)
        {
            if (slotIndex >= 0 && slotIndex < mySlots.Length)
            {
                CardSlot slot = mySlots[slotIndex];
                if (slot.myChild != null)
                {                 
                    CardIcon cardIcon = slot.myChild.GetComponent<CardIcon>();
                    if (cardIcon != null)
                    {
                        player.characterStat.eqiupCard[slotIndex] = cardIcon.myData.cardNo;
                    }
                    
                }
                else
                {
                    player.characterStat.eqiupCard[slotIndex] = 0;
                }

            }            
        }
    }

   
    public void AddItem(int i)
    {
        CardSlot slot = FindBlankSlot();

        if (slot == null)
        {
            Debug.LogError("No blank slot found!");
            return;
        }

       
        GameObject cardIconObject = Instantiate(Resources.Load<GameObject>("Prefabs/CardIcon"), slot.transform);
        cardIconObject.GetComponent<CardIcon>().SetData(cardDataList[i]);

     
        DragCard dragCard = cardIconObject.GetComponent<DragCard>();
        if (dragCard == null)
        {
            Debug.LogError("DragCard component not found on the instantiated object!");
            return;
        }

      
    }
   
    void Start()
    {
         Instance = this;
        gameObject.SetActive(false);
        mySlots = GetComponentsInChildren<CardSlot>();
        GetCardInfoFromSlots();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            AddItem(UnityEngine.Random.Range(0, cardDataList.Length));
        }
        GetCardInfoFromSlots();
    }
}

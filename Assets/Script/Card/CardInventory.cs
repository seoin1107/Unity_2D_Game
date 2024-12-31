using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInventory : MonoBehaviour
{
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

    public void AddItem(int i)
    {
        CardSlot slot = FindBlankSlot(); // �� ���� ã��

        // �� ������ ã�� ���� ���
        if (slot == null)
        {
            Debug.LogError("No blank slot found!");
            return; // �� ������ ������ �� �̻� �������� ����
        }

        // ī�� ������ �ν��Ͻ��� ���Կ� ����
        GameObject cardIconObject = Instantiate(Resources.Load<GameObject>("Prefabs/CardIcon"), slot.transform);
        cardIconObject.GetComponent<CardIcon>().SetData(cardDataList[i]);

        // DragCard ������Ʈ ��������
        DragCard dragCard = cardIconObject.GetComponent<DragCard>();
        if (dragCard == null)
        {
            Debug.LogError("DragCard component not found on the instantiated object!");
            return;
        }

        // myChild�� CardIcon Ÿ������ Ȯ�� �� �����ϰ� SetData ȣ��
        //CardIcon cardIcon = slot.myChild as CardIcon;  // myChild�� CardIcon Ÿ������ ĳ����
        //if (cardIcon == null)
        //{
        //    Debug.LogError("Slot's myChild is not a CardIcon or is null!");
        //    return; // myChild�� CardIcon�� �ƴ϶�� ó�� �ߴ�
        //}

        // ī�� ������ ����
        //cardIcon.SetData(cardDataList[i]);
    }
    //public void AddItem(int i)
    //{
    //    CardSlot slot = FindBlankSlot();
    //    if (slot != null)
    //    {
    //        Instantiate(Resources.Load<GameObject>("Prefabs/CardIcon"), slot.transform).GetComponent<DragCard>();
    //        (slot.myChild as CardIcon).SetData(cardDataList[i]);
    //    }
    //}
    // Start is called before the first frame update
    void Start()
    {
         Instance = this;
        gameObject.SetActive(false);
        mySlots = GetComponentsInChildren<CardSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            AddItem(UnityEngine.Random.Range(3, cardDataList.Length));
        }
    }
}

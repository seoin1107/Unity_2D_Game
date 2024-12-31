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
        CardSlot slot = FindBlankSlot(); // 빈 슬롯 찾기

        // 빈 슬롯을 찾지 못한 경우
        if (slot == null)
        {
            Debug.LogError("No blank slot found!");
            return; // 빈 슬롯이 없으면 더 이상 진행하지 않음
        }

        // 카드 아이콘 인스턴스를 슬롯에 생성
        GameObject cardIconObject = Instantiate(Resources.Load<GameObject>("Prefabs/CardIcon"), slot.transform);
        cardIconObject.GetComponent<CardIcon>().SetData(cardDataList[i]);

        // DragCard 컴포넌트 가져오기
        DragCard dragCard = cardIconObject.GetComponent<DragCard>();
        if (dragCard == null)
        {
            Debug.LogError("DragCard component not found on the instantiated object!");
            return;
        }

        // myChild가 CardIcon 타입인지 확인 후 안전하게 SetData 호출
        //CardIcon cardIcon = slot.myChild as CardIcon;  // myChild가 CardIcon 타입으로 캐스팅
        //if (cardIcon == null)
        //{
        //    Debug.LogError("Slot's myChild is not a CardIcon or is null!");
        //    return; // myChild가 CardIcon이 아니라면 처리 중단
        //}

        // 카드 데이터 설정
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

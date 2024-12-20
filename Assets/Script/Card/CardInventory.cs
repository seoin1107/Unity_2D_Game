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
        foreach (CardSlot s in mySlots)
        {
            if (s.myChild == null)
            {
                return s;
            }
        }
        return null;
    }


    public void AddItem(int i)
    {
        CardSlot slot = FindBlankSlot();
        if (slot != null)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/CardIcon"), slot.transform).GetComponent<DragCard>();
            (slot.myChild as CardIcon).SetData(cardDataList[i]);
        }
    }
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
            AddItem(Random.Range(0, cardDataList.Length));
        }
    }
}

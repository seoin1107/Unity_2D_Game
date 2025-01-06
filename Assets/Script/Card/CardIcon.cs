using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardIcon : DragCard
{
    public CardData myData;

    public void SetData(CardData data)
    {
        myData = data;
        myImage.sprite = myData.icon;
        transform.parent.GetComponent<CardSlot>().myChild = this;
    }
    public CardData GetCardData()
    {
        return myData;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetData(myData);
    }

    // Update is called once per frame
    void Update()
    {
    }
}

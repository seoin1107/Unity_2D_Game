using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour//, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] public TMPro.TMP_Text textCardInfo;
    public CardIcon myData;
    public static CardInfo Instance
    {
        get; set;
    }

    /*public void OnBeginDrag(PointerEventData eventData)
    {
        myImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        myImage.raycastTarget = true;
        transform.localPosition = Vector3.zero;
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        if (myData.myData.atk_add != 0)
        {
            textCardInfo.text += $"공격력 : +{myData.myData.atk_add}\n";
        }
        if (myData.myData.hp_add != 0)
        {
            textCardInfo.text += $"체력 : +{myData.myData.hp_add}\n";
        }
        if (myData.myData.atk_per != 0)
        {
            textCardInfo.text += $"공격력 : X{1 + myData.myData.atk_per}\n";
        }
        if (myData.myData.hp_per != 0)
        {
            textCardInfo.text += $"체력 : X{1 + myData.myData.hp_per}\n";
        }
        if (myData.myData.skill_cool != 0)
        {
            textCardInfo.text += $"스킬 쿨타임 : {myData.myData.skill_cool*100}%\n";
        }
        if (myData.myData.skill_dm != 0)
        {
            textCardInfo.text += $"스킬 공격력 : X{1+myData.myData.skill_dm}\n";
        }
        if (myData.myData.atk_ab != 0)
        {
            textCardInfo.text += $"흡혈 : +{myData.myData.atk_ab*100}%\n";
        }
        if (myData.myData.hit_re != 0)
        {
            textCardInfo.text += $"피격시 무적 : +{myData.myData.hit_re}초\n";
        }
        Instance = this;
        gameObject.SetActive(false);
        
    }


    void Update()
    {
       
    }
}

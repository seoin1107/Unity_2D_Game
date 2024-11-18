using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class StatusUI : MonoBehaviour
{

    public static StatusUI Instance
    {
        get; set;
    }

    [SerializeField] public TMPro.TMP_Text textLeft;
    [SerializeField] public TMPro.TMP_Text textHp;
    [SerializeField] public TMPro.TMP_Text textAtk;
    [SerializeField] public TMPro.TMP_Text textUtil;
    [SerializeField] public TMPro.TMP_Text textTotalAtk;
    [SerializeField] public TMPro.TMP_Text textMaxHp;

    public float leftPoint;
    public Button hpUpButton;
    public Button hpDownButton;
    public Button atkUpButton;
    public Button atkDownButton;
    public Button utilUpButton;
    public Button utilDownButton;


    public UnityAction closeAlram;
    public CharacterStatus player;


    private void Start()
    {
        Instance = this;
        gameObject.SetActive(false);
        player.UpdateStatus();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && transform.gameObject.activeSelf == false)
        {
            transform.gameObject.SetActive(true);
            UpdateStatusUI();
        }
        if (Input.GetKeyDown(KeyCode.P) && transform.gameObject.activeSelf == true)
        {
            transform.gameObject.SetActive(false);
        }

        if (leftPoint == 0.0f)
        {
            hpUpButton.interactable = false;
            atkUpButton.interactable = false;
            utilUpButton.interactable = false;
        }
        else
        {
            hpUpButton.interactable = true;
            atkUpButton.interactable = true;
            utilUpButton.interactable = true;
        }
        if (player.hpPoint == 0)
        {
            hpDownButton.interactable = false;
        }
        else hpDownButton.interactable = true; 
        if (player.atkPoint == 0)
        {
            atkDownButton.interactable = false;
        }
        else atkDownButton.interactable = true; 
        if (player.utilPoint == 0)
        {
            utilDownButton.interactable = false;
        }
        else utilDownButton.interactable = true;


        
    }


    public void UpdateStatusUI()
    {
        leftPoint = player.totalPoint - player.hpPoint - player.atkPoint - player.utilPoint;
        textLeft.text = $"Left Point : {leftPoint}";
        textHp.text = player.hpPoint.ToString();
        textAtk.text = player.atkPoint.ToString();
        textUtil.text = player.utilPoint.ToString();
        textTotalAtk.text = $"Total Atk : {player.totalAtk}";
        textMaxHp.text = $"Max Hp : {player.maxHp}";
    }

    public  void AddHp()
    {  
        player.hpPoint++;
        player.UpdateStatus();
        UpdateStatusUI(); 
    }
    public  void AddAtk()
    {
        player.atkPoint++;
        player.UpdateStatus();
        UpdateStatusUI();
    }
    public void AddUtil()
    {
        player.utilPoint++;
        player.UpdateStatus();
        UpdateStatusUI();
    }
    public void SubHp()
    {
        player.hpPoint--;
        player.UpdateStatus();
        UpdateStatusUI();
    }
    public void SubAtk()
    {
        player.atkPoint--;
        player.UpdateStatus();
        UpdateStatusUI();
    }
    public void SubUtil()
    {
        player.utilPoint--;
        player.UpdateStatus();
        UpdateStatusUI();
    }


}


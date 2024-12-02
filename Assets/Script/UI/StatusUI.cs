using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;



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
        if (!File.Exists(Application.dataPath + "/Data/Save/tempSave.dat")) {
            PlayerInitialize();
        }
        else
        {
            player.characterStat = FileManager.LoadFromJson<Stat>(Application.dataPath + "/Data/Save/tempSave.dat");
        }
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
        if (player.characterStat.hpPoint == 0)
        {
            hpDownButton.interactable = false;
        }
        else hpDownButton.interactable = true; 
        if (player.characterStat.atkPoint == 0)
        {
            atkDownButton.interactable = false;
        }
        else atkDownButton.interactable = true; 
        if (player.characterStat.utilPoint == 0)
        {
            utilDownButton.interactable = false;
        }
        else utilDownButton.interactable = true;


        
    }


    public void UpdateStatusUI()
    {
        leftPoint = player.characterStat.totalPoint - player.characterStat.hpPoint - player.characterStat.atkPoint - player.characterStat.utilPoint;
        textLeft.text = $"Left Point : {leftPoint}";
        textHp.text = player.characterStat.hpPoint.ToString();
        textAtk.text = player.characterStat.atkPoint.ToString();
        textUtil.text = player.characterStat.utilPoint.ToString();
        textTotalAtk.text = $"Total Atk : {player.characterStat.totalAtk}";
        textMaxHp.text = $"Max Hp : {player.characterStat.maxHP}";
    }

    public  void AddHp()
    {  
        player.characterStat.hpPoint++;
        player.UpdateStatus();
        UpdateStatusUI(); 
    }
    public  void AddAtk()
    {
        player.characterStat.atkPoint++;
        player.UpdateStatus();
        UpdateStatusUI();
    }
    public void AddUtil()
    {
        player.characterStat.utilPoint++;
        player.UpdateStatus();
        UpdateStatusUI();
    }
    public void SubHp()
    {
        player.characterStat.hpPoint--;
        player.UpdateStatus();
        UpdateStatusUI();
    }
    public void SubAtk()
    {
        player.characterStat.atkPoint--;
        player.UpdateStatus();
        UpdateStatusUI();
    }
    public void SubUtil()
    {
        player.characterStat.utilPoint--;
        player.UpdateStatus();
        UpdateStatusUI();
    }

    public void PlayerInitialize() //플레이어 스텟 초기화
    {
        player.characterStat.baseHP = 20;
        player.characterStat.baseAtk = 10;
        player.characterStat.moveSpeed = 5.0f;
        player.characterStat.atkSpeed = 0;
        player.characterStat.attackRange = 1.0f;
        player.characterStat.level = 1;
        player.characterStat.hpRegen = 0;
        player.characterStat.totalPoint = 1;
        player.characterStat.atkPoint = 0;
        player.characterStat.hpPoint = 0;
        player.characterStat.utilPoint = 0;
        player.characterStat.hitRecover = 0.5f;
        player.characterStat.skillCool = 1.0f;
        player.characterStat.skillDamage = 1.0f;
        player.characterStat.drain = 0;
        player.characterStat.dodgeTime = 0.2f;
        player.characterStat.dodgeCool = 5.0f;
        player.characterStat.parryingTime = 0.2f;
        player.characterStat.parryingCool = 2.0f;
        player.characterStat.needExp = 10;
        player.characterStat.curExp = 0;
        player.characterStat.eqiupCard = new int[3] { 0, 0, 0 };
    }


}


using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting;



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
    [SerializeField] public TMPro.TMP_Text textPointOption;


    public float leftPoint;
    public Button hpUpButton;
    public Button hpDownButton;
    public Button atkUpButton;
    public Button atkDownButton;
    public Button utilUpButton;
    public Button utilDownButton;


    bool hpMaxPoint = false;


    public UnityAction closeAlram;

    public CharacterStatus player;

    private void Start()
    {
        Instance = this;
        gameObject.SetActive(false);
        if (!File.Exists(Application.dataPath + "/Data/Save/tempSave.dat"))
        {
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
        OnOffHpOption();
        OnOffAtkOption();
        OnOffUtilOption();
        player.UpdateStatus();
        TextUpdate();
    }


    public void AddHp()
    {
        player.characterStat.hpPoint++;
        player.UpdateStatus();
        UpdateStatusUI();
    }
    public void AddAtk()
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
        player.characterStat.CanJump = 1;
        player.characterStat.needExp = 10;
        player.characterStat.curExp = 0;
        player.characterStat.eqiupCard = new int[3] { 0, 0, 0 };

        player.characterStat.hpOption1 = false;
        player.characterStat.hpOption2 = false;
        player.characterStat.hpOption3 = false;

        player.characterStat.atkOption1 = false;
        player.characterStat.atkOption2 = false;
        player.characterStat.atkOption3 = false;

        player.characterStat.utilOption1 = false;
        player.characterStat.utilOption2 = false;
        player.characterStat.utilOption3 = false;
    }


    private string[] HPOptions = new string[]
    {
        "최대 체력 +20\n",//0
        "체력 재생 +1%, 피격시 무적 +1s\n",//1
        "3번째 공격마다 최대체력 25% 추가 데미지\n"//2
    };
    private string[] ATKOptions = new string[]
    {
        "추가 공격력 +20%\n",//3
        "공격/이동 속도 +20%\n",//4
        "5회 공격 명중 시 공격력 +10 / 공격 속도 +20%\n"//5
    };
    private string[] UtilOptions = new string[]
    {
        "이동 속도 +50%\n",//6
        "더블 점프\n",//7
        "회피 무적 시간 +0.1s / 패링, 회피 쿨타임 -50%\n"//8
    };



    public void TextUpdate()
    {
        string Options = null;
        if (player.characterStat.hpOption1 == true)
        {
            Options += HPOptions[0];
        }
        if (player.characterStat.hpOption2 == true)
        {
            Options += HPOptions[1];
        }
        if (player.characterStat.hpOption3 == true)
        {
            Options += HPOptions[2];
        }
        if (player.characterStat.atkOption1 == true)
        {
            Options += ATKOptions[0];
        }
        if (player.characterStat.atkOption2 == true)
        {
            Options += ATKOptions[1];
        }
        if (player.characterStat.atkOption3 == true)
        {
            Options += ATKOptions[2];
        }
        if (player.characterStat.utilOption1 == true)
        {
            Options += UtilOptions[0];
        }
        if (player.characterStat.utilOption2 == true)
        {
            Options += UtilOptions[1];
        }
        if (player.characterStat.utilOption3 == true)
        {
            Options += UtilOptions[2];
        }
        leftPoint = player.characterStat.totalPoint - player.characterStat.hpPoint - player.characterStat.atkPoint - player.characterStat.utilPoint;
        textLeft.text = $"Left Point : {leftPoint}";
        textHp.text = player.characterStat.hpPoint.ToString();
        textAtk.text = player.characterStat.atkPoint.ToString();
        textUtil.text = player.characterStat.utilPoint.ToString();
        textTotalAtk.text = $"Total Atk : {player.characterStat.totalAtk}";
        textMaxHp.text = $"Max Hp : {player.characterStat.maxHP}";
        textPointOption.text = Options;
    }


    public void OnOffHpOption()
    {
        if (player.characterStat.hpPoint == 10 && player.characterStat.hpOption1 == false)
        {
            player.characterStat.hpOption1 = true;
            player.UpdateStatus();
        }
        if (player.characterStat.hpPoint == 20 && player.characterStat.hpOption2 == false)
        {
            player.characterStat.hpOption2 = true;
            player.characterStat.hpRegen += 0.1f;
            player.characterStat.hitRecover += 1;
        }
        if (player.characterStat.hpPoint == 30 && player.characterStat.hpOption3 == false)
        {
            player.characterStat.hpOption3 = true;
        }

        if (player.characterStat.hpPoint == 9 && player.characterStat.hpOption1 == true)
        {
            player.characterStat.hpOption1 = false;
            player.UpdateStatus();
        }
        if (player.characterStat.hpPoint == 19 && player.characterStat.hpOption2 == true)
        {
            player.characterStat.hpOption2 = false;
            player.characterStat.hpRegen -= 0.1f;
            player.characterStat.hitRecover -= 1;
        }
        if (player.characterStat.hpPoint == 29 && player.characterStat.hpOption3 == true)
        {
            player.characterStat.hpOption3 = false;
        }
    }

    public void OnOffAtkOption()
    {
        if (player.characterStat.atkPoint == 10 && player.characterStat.atkOption1 == false)
        {
            player.characterStat.atkOption1 = true;
            player.UpdateStatus();
        }
        if (player.characterStat.atkPoint == 20 && player.characterStat.atkOption2 == false)
        {
            player.characterStat.atkOption2 = true;

        }
        if (player.characterStat.atkPoint == 30 && player.characterStat.atkOption3 == false)
        {
            player.characterStat.atkOption3 = true;
        }

        if (player.characterStat.atkPoint == 9 && player.characterStat.atkOption1 == true)
        {
            player.characterStat.atkOption1 = false;
            player.UpdateStatus();
        }
        if (player.characterStat.atkPoint == 19 && player.characterStat.atkOption2 == true)
        {
            player.characterStat.atkOption2 = false;

        }
        if (player.characterStat.atkPoint == 29 && player.characterStat.atkOption3 == true)
        {
            player.characterStat.atkOption3 = false;
        }
    }

    public void OnOffUtilOption()
    {
        if (player.characterStat.utilPoint == 10 && player.characterStat.utilOption1 == false)
        {
            player.characterStat.utilOption1 = true;
            player.UpdateStatus();
        }
        if (player.characterStat.utilPoint == 20 && player.characterStat.utilOption2 == false)
        {
            player.characterStat.utilOption2 = true;

        }
        if (player.characterStat.utilPoint == 30 && player.characterStat.utilOption3 == false)
        {
            player.characterStat.utilOption3 = true;
        }

        if (player.characterStat.utilPoint == 9 && player.characterStat.utilOption1 == true)
        {
            player.characterStat.utilOption1 = false;
            player.UpdateStatus();
        }
        if (player.characterStat.utilPoint == 19 && player.characterStat.utilOption2 == true)
        {
            player.characterStat.utilOption2 = false;

        }
        if (player.characterStat.utilPoint == 29 && player.characterStat.utilOption3 == true)
        {
            player.characterStat.utilOption3 = false;
        }
    }
}


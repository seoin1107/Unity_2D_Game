using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]

public class MenuUI : MonoBehaviour
{
    
    public CharacterStatus mySave;
    string filePath = Application.dataPath + "/Data/Save/PlayerSave.dat"; //���̺� ���� ���

    public static MenuUI Instance
    {
        get;  set;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(filePath)) //���� ����� ���� ������ �÷��̾� ��������ȭ
        {
            mySave.characterStat = FileManager.LoadFromJson<Stat>(filePath);
        }
        else // ���� ����� �⺻ �������� �ʱ�ȭ
        {
            PlayerInitialize();
        }

        Instance = this;
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveData()
    {
        FileManager.SaveToJson<Stat>(filePath, mySave.characterStat);
    }

    public void PlayerInitialize() //�÷��̾� ���� �ʱ�ȭ
    {
        mySave.characterStat.baseHP = 20;
        mySave.characterStat.baseAtk = 10;
        mySave.characterStat.moveSpeed = 5.0f;
        mySave.characterStat.atkSpeed = 0;
        mySave.characterStat.attackRange = 1.0f;
        mySave.characterStat.level = 1;
        mySave.characterStat.hpRegen = 0;
        mySave.characterStat.totalPoint = 1;
        mySave.characterStat.atkPoint = 0;
        mySave.characterStat.hpPoint = 0;
        mySave.characterStat.utilPoint = 0;
        mySave.characterStat.hitRecover = 0.5f;
        mySave.characterStat.skillCool = 1.0f;
        mySave.characterStat.skillDamage = 1.0f;
        mySave.characterStat.drain = 0;
        mySave.characterStat.dodgeTime = 0.2f;
        mySave.characterStat.dodgeCool = 5.0f;
        mySave.characterStat.parryingTime = 0.2f;
        mySave.characterStat.parryingCool = 2.0f;
        mySave.characterStat.needExp = 10;
        mySave.characterStat.curExp = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]

public class MenuUI : MonoBehaviour
{
    
    public CharacterStatus mySave;
    string filePath = Application.dataPath + "/Data/Save/PlayerSave.dat"; //세이브 파일 경로

    public static MenuUI Instance
    {
        get;  set;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(filePath)) //파일 존재시 파일 정보로 플레이어 정보동기화
        {
            mySave.characterStat = FileManager.LoadFromJson<Stat>(filePath);
        }
        else // 파일 부재시 기본 스탯으로 초기화
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

    public void PlayerInitialize() //플레이어 스텟 초기화
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

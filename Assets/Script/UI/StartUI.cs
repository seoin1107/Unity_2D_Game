using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{

    public CharacterStatus mySave;
    public string filePath = Application.dataPath + "/Data/Save/"; //세이브 파일 경로



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OnStartNewGame()
    {
      //신변경 -> 플레이어초기화

    }

    public void OnOffLoadGameMenu()
    {
        if (LoadGameUI.Instance != null)
        {
            LoadGameUI.Instance.gameObject.SetActive(!LoadGameUI.Instance.gameObject.activeSelf);
        }
    }

    public void OnOptionMenu()
    {

    }

    public void OnExitGame()
    {

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
        mySave.characterStat.hpReCool = 1.0f;
        mySave.characterStat.needExp = 10;
        mySave.characterStat.curExp = 0;
        mySave.characterStat.eqiupCard = new int[3] {0,0,0};
    }

}

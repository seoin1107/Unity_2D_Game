using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Totuto : MonoBehaviour
{

    public CharacterStatus mySave;
    public void GameStart()
    {


        StartCoroutine(ChangeScene());


    }
    IEnumerator ChangeScene()
    {
        while (true)
        {
            
                yield return new WaitForSeconds(0.7f);
                Loading.nScene = 0;
                SceneManager.LoadScene(1);
            yield return null;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StopAllCoroutines();
        }
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
        mySave.characterStat.eqiupCard = new int[3] { 0, 0, 0 };
    }
}

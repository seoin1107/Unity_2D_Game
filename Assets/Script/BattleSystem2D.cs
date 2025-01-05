using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[System.Serializable]
//public struct BattleStat
//{
//    public float totalAtk;
//    public float attackRange;
//    public float atkSpeed;
//    public float maxHP;
//    public float curHP;
//}

interface IDeathAlarm
{
    UnityAction deathAlarm { get; set; }
}
interface IDamage // 추상함수만을 가지는 = 부모로만 존재
{
    void OnDamage(float dmg);
}
interface ILive
{
    bool IsLive { get; }
}


public class BattleSystem2D : Movement2D, IDamage, IDeathAlarm, ILive
{
    //public Stat battleStat;
    /*    public CharacterStatus characterStatus;*/
    protected float playTime;

    public UnityAction deathAlarm { get; set; }

    public bool IsLive
    {
        get => characterStat.curHP > 0 ? true : false;
    }
    protected virtual void OnDead()
    {
        deathAlarm?.Invoke();
    }


    public void OnDamage(float dmg)
    {
        // IsParry가 true인 경우 데미지 무효화
        if (myAnim.GetBool(animData.IsParry))
        {
            Debug.Log("패링성공");
            myAnim.SetTrigger(animData.OnParring);
            //작동엔 문제가 없으나 작은오류가 발생 이후 코루틴형식으로 변경해야할듯
            return;
        }
        characterStat.curHP -= dmg;
        if (characterStat.curHP > 0.0f)
        {
            myAnim.SetTrigger(animData.OnDamage);
            if (gameObject.CompareTag("Player"))
            {
                SFXManager.Instance.PlaySound(SFXManager.Instance.playerDamage);
            }
        }
        else
        {
            myAnim.SetTrigger(animData.OnDead);
            OnDead();
            Player2D playerScript = gameObject.GetComponent<Player2D>();
            Picking pickingScript = gameObject.GetComponent<Picking>();
            if (playerScript != null)
            {
                playerScript.enabled = false; // Player2D 스크립트 비활성화
            }
            if (pickingScript != null)
            {
                pickingScript.enabled = false;
            }
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            myRigid.gravityScale = 0.0f;
            myRigid.velocity = Vector2.zero;
            if (gameObject.CompareTag("Player"))
            {
                SFXManager.Instance.PlaySound(SFXManager.Instance.playerDead);
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

interface IDeathAlarm
{
    UnityAction deathAlarm { get; set; }
}


interface ILive
{
    bool IsLive { get; }
}


interface IDamage // 추상함수만을 가지는 = 부모로만 존재
{
    void OnDamage(float dmg);
}

interface IBattle : ILive, IDamage, IDeathAlarm
{

}

// 직렬화은 파일로저장한다는 의미
public class BattleSystem : CharacterStatus, IBattle
{
    public CharacterStatus battleStat;
    protected float playTime = 0.0f;
    public GameObject myTarget;

    public UnityAction deathAlarm { get; set; }

    public bool IsLive
    {
        get
        {
            return battleStat.curHp > 0.0f;
        }
    }

    protected void OnReset()
    {

        battleStat.curHp = battleStat.maxHp;
    }

    protected virtual void OnDead()
    {
        deathAlarm?.Invoke();
    }

    public void OnDamage(float dmg)
    {
        battleStat.curHp -= dmg;
        if (battleStat.curHp <= 0.0f)
        {
            myAnim.SetTrigger(animData.OnDead);
            OnDead();


        }
        else
        {
            myAnim.SetTrigger(animData.OnDamage);
        }

        myAnim.SetTrigger(animData.OnDamage);

    }

    public void OnAttack()
    {
        myTarget.GetComponent<IDamage>().OnDamage(battleStat.atkSpeed);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public struct BattleStat
{
    public float AP;
    public float AttackRange;
    public float AttackDelay;
    public float maxHP;
    public float curHP;
}
interface IDeathAlarm
{
    UnityAction deathAlarm { get; set; }
}
interface IDamage // 추상함수만을 가지는 = 부모로만 존재
{
    void OnDamage(float dmg);
}


public class BattleSystem2D : Movement2D, IDamage, IDeathAlarm
{
    public BattleStat battleStat;
    protected float playTime;

    public UnityAction deathAlarm { get; set; }
    protected virtual void OnDead()
    {
        deathAlarm?.Invoke();
    }


    public void OnDamage(float dmg)
    {
        // IsParry가 true인 경우 데미지 무효화
        if (myAnim.GetBool(animData.IsParry))
        {
            myAnim.SetTrigger(animData.OnParring);
            Debug.Log("Parried! Damage avoided.");
            return;
        }
        battleStat.curHP -= dmg;
        if (battleStat.curHP > 0.0f)
        {
            myAnim.SetTrigger(animData.OnDamage);
        }
        else
        {
            myAnim.SetTrigger(animData.OnDead);
            OnDead();
        }
    }
}


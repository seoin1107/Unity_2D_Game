using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stat
{

    public float baseHP;
    public float maxHP;
    public float curHP;


    public float baseAtk;
    public float totalAtk;


    public float moveSpeed;
    public float atkSpeed;
    public float attackRange;


    public int level;

    public float hpRegen;

    public float totalPoint;
    public float atkPoint;
    public float hpPoint;
    public float utilPoint;

    public float hitRecover;
    public float skillCool;
    public float skillDamage;

    public float drain;

    public float dodgeTime;
    public float dodgeCool;
    public float parryingTime;
    public float parryingCool;

    public int needExp;
    public int curExp;

    public int[] eqiupCard;
}


public class CharacterStatus : AnimatorProperty
{
    public Stat characterStat;

 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpdateStatus()
    {
        characterStat.totalAtk = characterStat.baseAtk + characterStat.atkPoint;
        characterStat.maxHP = characterStat.baseHP+ characterStat.hpPoint *2;
        characterStat.curHP = characterStat.maxHP;
    }
    public void LevelUp(Stat stat)
    {
        while (stat.needExp <= stat.curExp)
        {
            stat.curExp -= stat.needExp;
            stat.level++;
            stat.needExp += 5;
            stat.totalPoint += 1;
        }       
    }
}
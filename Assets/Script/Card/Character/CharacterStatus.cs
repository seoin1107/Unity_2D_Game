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
    public float curDodgeCool;
    public float parryingTime;
    public float parryingCool;
    public float curParryingCool;

    public int CanJump;
    public int jumpCount;

    public int needExp;
    public int curExp;

    public int[] eqiupCard;

    public bool hpOption1;
    public bool hpOption2;
    public bool hpOption3;

    public bool atkOption1;
    public bool atkOption2;
    public bool atkOption3;

    public bool utilOption1;
    public bool utilOption2;
    public bool utilOption3;
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
        characterStat.moveSpeed = 5;
        characterStat.atkSpeed = 1;
        characterStat.CanJump = 1;
        if(characterStat.atkOption1 == true)
        {
            characterStat.totalAtk *= 1.2f;
        }
        if (characterStat.atkOption2 == true)
        {
            characterStat.atkSpeed *= 1.2f;
            characterStat.moveSpeed *= 1.2f;
        }
        characterStat.maxHP = characterStat.baseHP+ characterStat.hpPoint *2;
        if (characterStat.hpOption1 == true)
        {
            characterStat.maxHP += 20;
        }
        characterStat.curHP = characterStat.maxHP;
        if(characterStat.utilOption1 == true)
        {
            characterStat.moveSpeed *= 1.5f;
        }
        if (characterStat.utilOption2 == true)
        {
            characterStat.CanJump += 1;
        }

        

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
    public Stat GetStat()
    {
        return characterStat;
    }
}
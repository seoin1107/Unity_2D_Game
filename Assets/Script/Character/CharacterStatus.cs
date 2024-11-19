using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stat : AnimatorProperty
{


    public int level = 1;

    public float baseHp = 20;
    public float maxHp;
    public float curHp;


    public float baseAtk = 10;
    public float totalAtk = 0;

    public float totalPoint = 1;
    public float atkPoint = 0;
    public float hpPoint = 0;
    public float utilPoint = 0;

    public float moveSpeed = 5.0f;
    public float atkSpeed = 0;
    public float hpRegen = 0;

    public float hitRecover = 0.5f;
    public float skillCool = 1.0f;
    public float skillDamage = 1.0f;

    public float drain = 0;

    public float dodgeTime = 0.2f;
    public float dodgeCool = 5.0f;
    public float parryingTime = 0.2f;
    public float parryingCool = 2.0f;

    public int needExp = 10;
    public int curExp = 0;


}

public class CharacterStatus : Stat
{
   
    
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
        totalAtk = baseAtk + atkPoint;
        maxHp = baseHp + hpPoint *2;

    }
    public void LevelUp()
    {
        while (needExp <= curExp)
        {
            curExp -= needExp;
            level++;
            needExp += 5;
            totalPoint += 1;
        }       
    }
}
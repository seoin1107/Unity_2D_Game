using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stat : MonoBehaviour
{
    public int level = 1;

    public int baseHp = 20;
    public int maxHp;
    public int curHp;


    public int baseAtk = 10;
    public int totalAtk = 0;


    public int statAtk = 0;
    public int statHp = 0;
    public int statUtil = 0;

    public float moveSpeed = 1.0f;
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

    public void LevelUp()
    {
        
    }

}
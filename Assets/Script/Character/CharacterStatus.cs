using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    class Stat
    {
        public int level;

        public int baseHp;
        public int maxHp;
        public int curHp;

        public int baseAtk;


        public int statAtk;
        public int statHp;
        public int statUtil;

        public float moveSpeed;
        public float atkSpeed;
        public float hpRegen;

        public float hitRecover;
        public float skillCool;
        public float skillDamage;

        public float drain;
        public float dodgeTime;
        public float dodgeCool;
        public float parryingTime;
        public float parryingCool;



    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
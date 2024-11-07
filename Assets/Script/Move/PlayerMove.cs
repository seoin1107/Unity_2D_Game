using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : Movement
{
    //public LayerMask moveMask; // 이동을 위한 레이어 마스크
    //public LayerMask attackMask; // 공격을 위한 레이어 마스크
    //public UnityEvent<Vector2> moveAction; // 이동 방향 전달을 위한 이벤트
   // public UnityEvent<GameObject> attackAction; // 공격 대상 전달을 위한 이벤트


    void Start()
    {

    }

    void Update()
    {
        curSpaceCool += Time.deltaTime; //스페이스 쿨타임 계산
        OnMove();
        OnJump();
        OnAttack();
        LevelUp();////임시
    }

}



using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : Movement
{
    //public LayerMask moveMask; // �̵��� ���� ���̾� ����ũ
    //public LayerMask attackMask; // ������ ���� ���̾� ����ũ
    //public UnityEvent<Vector2> moveAction; // �̵� ���� ������ ���� �̺�Ʈ
   // public UnityEvent<GameObject> attackAction; // ���� ��� ������ ���� �̺�Ʈ


    void Start()
    {

    }

    void Update()
    {
        curSpaceCool += Time.deltaTime; //�����̽� ��Ÿ�� ���
        OnMove();
        OnJump();
        OnAttack();
        LevelUp();////�ӽ�
    }

}



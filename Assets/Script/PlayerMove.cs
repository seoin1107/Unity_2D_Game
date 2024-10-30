using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : Movement
{
    public LayerMask moveMask; // �̵��� ���� ���̾� ����ũ
    public LayerMask attackMask; // ������ ���� ���̾� ����ũ
    public UnityEvent<Vector2> moveAction; // �̵� ���� ������ ���� �̺�Ʈ
    public UnityEvent<GameObject> attackAction; // ���� ��� ������ ���� �̺�Ʈ

    void Start()
    {
        moveSpeed = 5.0f; // �÷��̾� �̵� �ӵ�
    }

    void Update()
    {
        OnMove();
        OnJump();
        OnAttack();
    }

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sencor : MonoBehaviour
{
    public bool isFalling = false; // isFalling ���� ����

    // �浹 üũ�� ���� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� ���̾ "Player"���� Ȯ��
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isFalling = true; // isFalling ���� true�� ����

        }
    }
}


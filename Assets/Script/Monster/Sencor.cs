using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sencor : MonoBehaviour
{
    public BattleWall battleWall; // BattleWall ��ũ��Ʈ ����

    // Start is called before the first frame update
    void Start()
    {
        // �ʿ��� �ʱ�ȭ
    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾ �����ϴ� ������ ���� (��: Ʈ���� ���)
    }

    // �÷��̾ ���� ������ ������ ȣ��Ǵ� �޼���
    void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾ ������ ���� ��
        if (other.CompareTag("Player"))
        {
            // BattleWall�� isFalling�� true�� ����
            if (battleWall != null)
            {
                battleWall.isFalling = true;
            }
        }
    }
}

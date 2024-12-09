using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<GameObject> monsters; // �濡 �ִ� ���͵�
    public RoomManager roomManager; // �ش� ���� RoomManager

    void Start()
    {
        // �濡 �ִ� ���͵� �ʱ�ȭ
        if (monsters == null) monsters = new List<GameObject>();

        // �� �Ŵ��� ���� ����
        if (roomManager == null) roomManager = GetComponentInParent<RoomManager>();
    }

    void Update()
    {
        // ���� ���� üũ
        CheckMonsters();
    }

    // ���� ���� üũ (��� ���Ͱ� óġ�Ǿ����� Ȯ��)
    void CheckMonsters()
    {
        // ���� ����Ʈ���� ���� ���ʹ� ����
        monsters.RemoveAll(monster => monster == null);

        // ���Ͱ� ��� óġ�Ǿ����� ���� ����
        if (monsters.Count == 0 && roomManager != null)
        {
            roomManager.UnlockDoor(); // ���Ͱ� ��� óġ�Ǹ� �� ����
        }
    }
}

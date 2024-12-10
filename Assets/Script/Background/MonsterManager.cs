using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<GameObject> monsters; // �濡 �ִ� ���͵�
    public RoomManager roomManager; // �ش� ���� RoomManager

    void Start()
    {
        if (monsters == null) monsters = new List<GameObject>();
        if (roomManager == null) roomManager = GetComponentInParent<RoomManager>();
    }

    void Update()
    {
        CheckMonsters();
    }

    // ���� ���� üũ
    void CheckMonsters()
    {
        monsters.RemoveAll(monster => monster == null); // ���� ���� ����

        if (monsters.Count == 0 && roomManager != null && !roomManager.isRoomCleared)
        {
            roomManager.ClearRoom(); // ���Ͱ� ��� óġ�Ǹ� �� Ŭ���� ó��
        }
    }
}

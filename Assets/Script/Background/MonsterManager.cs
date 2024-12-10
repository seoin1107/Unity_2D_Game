using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<GameObject> monsters; // 방에 있는 몬스터들
    public RoomManager roomManager; // 해당 방의 RoomManager

    void Start()
    {
        if (monsters == null) monsters = new List<GameObject>();
        if (roomManager == null) roomManager = GetComponentInParent<RoomManager>();
    }

    void Update()
    {
        CheckMonsters();
    }

    // 몬스터 상태 체크
    void CheckMonsters()
    {
        monsters.RemoveAll(monster => monster == null); // 죽은 몬스터 제거

        if (monsters.Count == 0 && roomManager != null && !roomManager.isRoomCleared)
        {
            roomManager.ClearRoom(); // 몬스터가 모두 처치되면 방 클리어 처리
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<GameObject> monsters; // 방에 있는 몬스터들
    public RoomManager roomManager; // 해당 방의 RoomManager

    void Start()
    {
        // 방에 있는 몬스터들 초기화
        if (monsters == null) monsters = new List<GameObject>();

        // 방 매니저 참조 연결
        if (roomManager == null) roomManager = GetComponentInParent<RoomManager>();
    }

    void Update()
    {
        // 몬스터 상태 체크
        CheckMonsters();
    }

    // 몬스터 상태 체크 (모든 몬스터가 처치되었는지 확인)
    void CheckMonsters()
    {
        // 몬스터 리스트에서 죽은 몬스터는 제거
        monsters.RemoveAll(monster => monster == null);

        // 몬스터가 모두 처치되었으면 방을 열기
        if (monsters.Count == 0 && roomManager != null)
        {
            roomManager.UnlockDoor(); // 몬스터가 모두 처치되면 방 해제
        }
    }
}

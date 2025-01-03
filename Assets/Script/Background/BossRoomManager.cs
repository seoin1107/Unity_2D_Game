using UnityEngine;

public class BossRoomManager : RoomManager
{
    public GameObject boss; // 보스 오브젝트
    public float doorMoveDistance = 5.0f; // 문이 이동할 거리
    public float doorMoveSpeed = 5.0f; // 문이 이동하는 속도

    protected override void Start()
    {
        
        SetCameraPriority(defaultCamera, HighPriority); // 기본 카메라 활성화
        SetCameraPriority(virtualCamera, LowPriority); // 방 카메라는 비활성화
        if (boss != null)
        {
            boss.SetActive(false); // 보스는 초기 상태에서 비활성화
        }
    }

    public override void EnterRoom()
    {
        base.EnterRoom();
        if (boss != null && !isRoomCleared)
        {
            StartBossFight(); // 보스전 시작
        }
        LockDoors();
    }

    public override void ExitRoom()
    {
        //if (!isRoomCleared) return; // 보스를 처치하지 않았다면 나갈 수 없음
        base.ExitRoom(); // 부모 클래스의 ExitRoom 호출
    }

    public override void ClearRoom()
    {
        isRoomCleared = true; // 보스 처치 기록
        if (boss != null)
        {
            boss.SetActive(false); // 보스 비활성화
        }
        UnlockDoors();
    }

    protected override void LockDoors()
    {
        foreach (var door in doors)
        {
            StartCoroutine(MoveDoor(door.transform, Vector3.down * doorMoveDistance)); // 문을 아래로 이동
        }
    }

    protected override void UnlockDoors()
    {
        foreach (var door in doors)
        {
            StartCoroutine(MoveDoor(door.transform, Vector3.up * doorMoveDistance)); // 문을 위로 이동
        }
    }

    private void StartBossFight()
    {
        if (boss != null)
        {
            boss.SetActive(true); // 보스 활성화
        }
    }

    private System.Collections.IEnumerator MoveDoor(Transform doorTransform, Vector3 targetOffset)
    {
        Vector3 startPos = doorTransform.position;
        Vector3 targetPos = startPos + targetOffset;
        float elapsedTime = 0f;

        while (elapsedTime < doorMoveDistance / doorMoveSpeed)
        {
            doorTransform.position = Vector3.Lerp(startPos, targetPos, (elapsedTime * doorMoveSpeed) / doorMoveDistance);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        doorTransform.position = targetPos; // 최종 위치 보정
    }
}

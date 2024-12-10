using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> doors; // 방 입구와 출구
    public CinemachineVirtualCamera virtualCamera; // 해당 방의 가상 카메라
    public CinemachineVirtualCamera defaultCamera; // 플레이어의 기본 카메라
    public PolygonCollider2D roomCollider; // 방의 폴리곤 콜라이더
    public bool isRoomCleared = false; // 방 클리어 상태 추적
    private const int HighPriority = 20; // 활성화된 카메라 우선순위
    private const int LowPriority = 10; // 비활성화된 카메라 우선순위

    void Start()
    {
        if (doors == null || doors.Count == 0)
        {
            doors = new List<GameObject>();
            foreach (var door in GetComponentsInChildren<Door>())
            {
                doors.Add(door.gameObject);
            }
        }

        UnlockDoors(); // 초기 상태에서 문을 엶
        SetCameraPriority(defaultCamera, HighPriority); // 기본 카메라 활성화
        SetCameraPriority(virtualCamera, LowPriority); // 방 카메라는 비활성화
    }

    // 방에 입장할 때 호출되는 함수
    public void EnterRoom()
    {
        if (isRoomCleared) return; // 이미 클리어한 방이면 아무 동작 안 함

        SetCameraPriority(virtualCamera, HighPriority); // 방 카메라 활성화
        SetCameraPriority(defaultCamera, LowPriority); // 기본 카메라 비활성화
        LockDoors(); // 문 잠금
    }

    // 방을 나갈 때 호출되는 함수
    public void ExitRoom()
    {
        if (isRoomCleared) return; // 이미 클리어한 방이면 카메라 전환 막기

        SetCameraPriority(defaultCamera, HighPriority); // 기본 카메라 활성화
        SetCameraPriority(virtualCamera, LowPriority); // 방 카메라 비활성화
        UnlockDoors(); // 문 열기
    }

    // 방 클리어 후 문 열기
    public void ClearRoom()
    {
        isRoomCleared = true;
        UnlockDoors(); // 몬스터 처치 후 문 열기
    }

    // 문 잠금
    public void LockDoors()
    {
        foreach (var door in doors)
        {
            door.SetActive(true); // 문 활성화 (잠금)
        }
    }

    // 문 열기
    public void UnlockDoors()
    {
        foreach (var door in doors)
        {
            door.SetActive(false); // 문 비활성화 (열기)
        }
    }

    // 폴리곤 콜라이더를 벗어나면 기본 카메라로 전환
    public void CheckExitRoom(Collider2D playerCollider)
    {
        if (isRoomCleared && !roomCollider.bounds.Contains(playerCollider.bounds.min))
        {
            SetCameraPriority(defaultCamera, HighPriority); // 기본 카메라 활성화
            SetCameraPriority(virtualCamera, LowPriority); // 방 카메라 비활성화
        }
    }

    // 카메라 우선순위 설정
    private void SetCameraPriority(CinemachineVirtualCamera camera, int priority)
    {
        if (camera != null)
        {
            camera.Priority = priority;
        }
    }
}

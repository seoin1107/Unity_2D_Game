using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> doors; // 방 입구와 출구
    public CinemachineVirtualCamera virtualCamera; // 해당 방의 가상 카메라
    public CinemachineVirtualCamera defaultCamera; // 플레이어의 기본 카메라
    public bool isRoomCleared = false; // 방 클리어 상태 추적

    protected const int HighPriority = 20; // 활성화된 카메라 우선순위
    protected const int LowPriority = 10; // 비활성화된 카메라 우선순위

    protected virtual void Start()
    {
        UnlockDoors(); // 초기 상태에서 문을 엶
        SetCameraPriority(defaultCamera, HighPriority); // 기본 카메라 활성화
        SetCameraPriority(virtualCamera, LowPriority); // 방 카메라는 비활성화
    }

    public virtual void EnterRoom()
    {
        if (isRoomCleared) return;

        SetCameraPriority(virtualCamera, HighPriority); // 방 카메라 활성화
        SetCameraPriority(defaultCamera, LowPriority); // 기본 카메라 비활성화
        LockDoors(); // 문 잠금
    }

    public virtual void ExitRoom()
    {
        if (!isRoomCleared) return;

        SetCameraPriority(defaultCamera, HighPriority); // 기본 카메라 활성화
        SetCameraPriority(virtualCamera, LowPriority); // 방 카메라 비활성화
    }

    public virtual void ClearRoom()
    {
        isRoomCleared = true;
        UnlockDoors(); // 몬스터 처치 후 문 열기
    }

    protected virtual void LockDoors()
    {
        foreach (var door in doors)
        {
            door.SetActive(true); // 문 활성화 (잠금)
        }
    }

    protected virtual void UnlockDoors()
    {
        foreach (var door in doors)
        {
            door.SetActive(false); // 문 비활성화 (열기)
        }
    }

    protected void SetCameraPriority(CinemachineVirtualCamera camera, int priority)
    {
        if (camera != null)
        {
            camera.Priority = priority;
        }
    }
}

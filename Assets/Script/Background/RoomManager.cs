using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> doors; // 방 입구와 출구
    public CinemachineVirtualCamera virtualCamera; // 해당 방의 가상 카메라
    private CinemachineVirtualCamera currentCamera; // 현재 활성화된 카메라
    public CinemachineVirtualCamera defaultCamera; // 플레이어의 기본 카메라
    public PolygonCollider2D roomCollider; // 방의 폴리곤 콜라이더
    public bool isRoomCleared = false; // 방 클리어 상태 추적

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
    }

    // 방에 입장할 때 호출되는 함수
    public void EnterRoom()
    {
        if (isRoomCleared) return; // 이미 클리어한 방이면 아무 동작 안 함

        ActivateCamera(); // 방에 들어오면 카메라 변경
        LockDoors(); // 문 잠금
    }

    // 방을 나갈 때 호출되는 함수
    public void ExitRoom()
    {
        if (isRoomCleared) return; // 이미 클리어한 방이면 카메라 전환 막기

        DeactivateCamera(); // 기존 카메라로 복귀
        UnlockDoors(); // 문 열기
    }

    // 카메라 활성화
    void ActivateCamera()
    {
        if (currentCamera != null)
        {
            currentCamera.gameObject.SetActive(false); // 이전 카메라는 비활성화
        }

        if (virtualCamera != null)
        {
            virtualCamera.gameObject.SetActive(true); // 새로운 카메라 활성화
            currentCamera = virtualCamera; // 현재 카메라 갱신
        }
    }

    // 기본 카메라로 복귀
    void DeactivateCamera()
    {
        if (currentCamera != null)
        {
            currentCamera.gameObject.SetActive(false); // 현재 카메라 비활성화
        }

        if (defaultCamera != null)
        {
            defaultCamera.gameObject.SetActive(true); // 기본 카메라 활성화
            currentCamera = defaultCamera; // 기본 카메라로 갱신
        }
    }

    
    public void LockDoors()
    {
        foreach (var door in doors)
        {
            door.SetActive(true); // 문 활성화 (잠금)
        }
    }
    // 방 클리어 후 문 열기
    public void UnlockDoors()
    {
        foreach (var door in doors)
        {
            door.SetActive(false); // 문 비활성화 (열기)
        }
    }

    // 몬스터를 처치했을 때 호출되는 함수
    public void ClearRoom()
    {
        isRoomCleared = true;
        UnlockDoors(); // 몬스터 처치 후 문 열기
    }

    // 폴리곤 콜라이더를 벗어나면 카메라 전환
    public void CheckExitRoom(Collider2D playerCollider)
    {
        if (isRoomCleared && !roomCollider.bounds.Contains(playerCollider.bounds.min))
        {
            DeactivateCamera(); // 원래 카메라로 전환
        }
    }
}

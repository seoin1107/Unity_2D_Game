using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> doors; // �� �Ա��� �ⱸ
    public CinemachineVirtualCamera virtualCamera; // �ش� ���� ���� ī�޶�
    public CinemachineVirtualCamera defaultCamera; // �÷��̾��� �⺻ ī�޶�
    public bool isRoomCleared = false; // �� Ŭ���� ���� ����

    protected const int HighPriority = 20; // Ȱ��ȭ�� ī�޶� �켱����
    protected const int LowPriority = 10; // ��Ȱ��ȭ�� ī�޶� �켱����

    protected virtual void Start()
    {
        UnlockDoors(); // �ʱ� ���¿��� ���� ��
        SetCameraPriority(defaultCamera, HighPriority); // �⺻ ī�޶� Ȱ��ȭ
        SetCameraPriority(virtualCamera, LowPriority); // �� ī�޶�� ��Ȱ��ȭ
    }

    public virtual void EnterRoom()
    {
        if (isRoomCleared) return;

        SetCameraPriority(virtualCamera, HighPriority); // �� ī�޶� Ȱ��ȭ
        SetCameraPriority(defaultCamera, LowPriority); // �⺻ ī�޶� ��Ȱ��ȭ
        LockDoors(); // �� ���
    }

    public virtual void ExitRoom()
    {
        if (!isRoomCleared) return;

        SetCameraPriority(defaultCamera, HighPriority); // �⺻ ī�޶� Ȱ��ȭ
        SetCameraPriority(virtualCamera, LowPriority); // �� ī�޶� ��Ȱ��ȭ
    }

    public virtual void ClearRoom()
    {
        isRoomCleared = true;
        UnlockDoors(); // ���� óġ �� �� ����
    }

    protected virtual void LockDoors()
    {
        foreach (var door in doors)
        {
            door.SetActive(true); // �� Ȱ��ȭ (���)
        }
    }

    protected virtual void UnlockDoors()
    {
        foreach (var door in doors)
        {
            door.SetActive(false); // �� ��Ȱ��ȭ (����)
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

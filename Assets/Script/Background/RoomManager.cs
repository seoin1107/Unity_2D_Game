using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> doors; // �� �Ա��� �ⱸ
    public CinemachineVirtualCamera virtualCamera; // �ش� ���� ���� ī�޶�
    public CinemachineVirtualCamera defaultCamera; // �÷��̾��� �⺻ ī�޶�
    public PolygonCollider2D roomCollider; // ���� ������ �ݶ��̴�
    public bool isRoomCleared = false; // �� Ŭ���� ���� ����
    private const int HighPriority = 20; // Ȱ��ȭ�� ī�޶� �켱����
    private const int LowPriority = 10; // ��Ȱ��ȭ�� ī�޶� �켱����

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

        UnlockDoors(); // �ʱ� ���¿��� ���� ��
        SetCameraPriority(defaultCamera, HighPriority); // �⺻ ī�޶� Ȱ��ȭ
        SetCameraPriority(virtualCamera, LowPriority); // �� ī�޶�� ��Ȱ��ȭ
    }

    // �濡 ������ �� ȣ��Ǵ� �Լ�
    public void EnterRoom()
    {
        if (isRoomCleared) return; // �̹� Ŭ������ ���̸� �ƹ� ���� �� ��

        SetCameraPriority(virtualCamera, HighPriority); // �� ī�޶� Ȱ��ȭ
        SetCameraPriority(defaultCamera, LowPriority); // �⺻ ī�޶� ��Ȱ��ȭ
        LockDoors(); // �� ���
    }

    // ���� ���� �� ȣ��Ǵ� �Լ�
    public void ExitRoom()
    {
        if (isRoomCleared) return; // �̹� Ŭ������ ���̸� ī�޶� ��ȯ ����

        SetCameraPriority(defaultCamera, HighPriority); // �⺻ ī�޶� Ȱ��ȭ
        SetCameraPriority(virtualCamera, LowPriority); // �� ī�޶� ��Ȱ��ȭ
        UnlockDoors(); // �� ����
    }

    // �� Ŭ���� �� �� ����
    public void ClearRoom()
    {
        isRoomCleared = true;
        UnlockDoors(); // ���� óġ �� �� ����
    }

    // �� ���
    public void LockDoors()
    {
        foreach (var door in doors)
        {
            door.SetActive(true); // �� Ȱ��ȭ (���)
        }
    }

    // �� ����
    public void UnlockDoors()
    {
        foreach (var door in doors)
        {
            door.SetActive(false); // �� ��Ȱ��ȭ (����)
        }
    }

    // ������ �ݶ��̴��� ����� �⺻ ī�޶�� ��ȯ
    public void CheckExitRoom(Collider2D playerCollider)
    {
        if (isRoomCleared && !roomCollider.bounds.Contains(playerCollider.bounds.min))
        {
            SetCameraPriority(defaultCamera, HighPriority); // �⺻ ī�޶� Ȱ��ȭ
            SetCameraPriority(virtualCamera, LowPriority); // �� ī�޶� ��Ȱ��ȭ
        }
    }

    // ī�޶� �켱���� ����
    private void SetCameraPriority(CinemachineVirtualCamera camera, int priority)
    {
        if (camera != null)
        {
            camera.Priority = priority;
        }
    }
}

using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> doors; // �� �Ա��� �ⱸ
    public CinemachineVirtualCamera virtualCamera; // �ش� ���� ���� ī�޶�
    private CinemachineVirtualCamera currentCamera; // ���� Ȱ��ȭ�� ī�޶�
    public CinemachineVirtualCamera defaultCamera; // �÷��̾��� �⺻ ī�޶�
    public PolygonCollider2D roomCollider; // ���� ������ �ݶ��̴�
    public bool isRoomCleared = false; // �� Ŭ���� ���� ����

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
    }

    // �濡 ������ �� ȣ��Ǵ� �Լ�
    public void EnterRoom()
    {
        if (isRoomCleared) return; // �̹� Ŭ������ ���̸� �ƹ� ���� �� ��

        ActivateCamera(); // �濡 ������ ī�޶� ����
        LockDoors(); // �� ���
    }

    // ���� ���� �� ȣ��Ǵ� �Լ�
    public void ExitRoom()
    {
        if (isRoomCleared) return; // �̹� Ŭ������ ���̸� ī�޶� ��ȯ ����

        DeactivateCamera(); // ���� ī�޶�� ����
        UnlockDoors(); // �� ����
    }

    // ī�޶� Ȱ��ȭ
    void ActivateCamera()
    {
        if (currentCamera != null)
        {
            currentCamera.gameObject.SetActive(false); // ���� ī�޶�� ��Ȱ��ȭ
        }

        if (virtualCamera != null)
        {
            virtualCamera.gameObject.SetActive(true); // ���ο� ī�޶� Ȱ��ȭ
            currentCamera = virtualCamera; // ���� ī�޶� ����
        }
    }

    // �⺻ ī�޶�� ����
    void DeactivateCamera()
    {
        if (currentCamera != null)
        {
            currentCamera.gameObject.SetActive(false); // ���� ī�޶� ��Ȱ��ȭ
        }

        if (defaultCamera != null)
        {
            defaultCamera.gameObject.SetActive(true); // �⺻ ī�޶� Ȱ��ȭ
            currentCamera = defaultCamera; // �⺻ ī�޶�� ����
        }
    }

    
    public void LockDoors()
    {
        foreach (var door in doors)
        {
            door.SetActive(true); // �� Ȱ��ȭ (���)
        }
    }
    // �� Ŭ���� �� �� ����
    public void UnlockDoors()
    {
        foreach (var door in doors)
        {
            door.SetActive(false); // �� ��Ȱ��ȭ (����)
        }
    }

    // ���͸� óġ���� �� ȣ��Ǵ� �Լ�
    public void ClearRoom()
    {
        isRoomCleared = true;
        UnlockDoors(); // ���� óġ �� �� ����
    }

    // ������ �ݶ��̴��� ����� ī�޶� ��ȯ
    public void CheckExitRoom(Collider2D playerCollider)
    {
        if (isRoomCleared && !roomCollider.bounds.Contains(playerCollider.bounds.min))
        {
            DeactivateCamera(); // ���� ī�޶�� ��ȯ
        }
    }
}

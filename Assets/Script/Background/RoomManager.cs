using UnityEngine;
using Cinemachine;

public class RoomManager : MonoBehaviour
{
    public CinemachineVirtualCamera targetCamera; //�̵��� ���� ī�޶�
    LayerMask layerMask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // �÷��̾ �濡 ������ ī�޶� �켱������ ����
            targetCamera.Priority = 20;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // �÷��̾ ���� ������ ī�޶� �켱������ ����
            targetCamera.Priority = 10;
        }
    }

    private Collider2D doorCollider;

    void Awake()
    {
        doorCollider = GetComponent<Collider2D>();
    }

    public void LockDoor()
    {
        doorCollider.enabled = true; // �� ��� (�� Ȱ��ȭ)
    }

    public void UnlockDoor()
    {
        doorCollider.enabled = false; // �� ���� (�� ��Ȱ��ȭ)
    }
}

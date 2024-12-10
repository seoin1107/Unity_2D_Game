using UnityEngine;

public class RoomEnter : MonoBehaviour
{
    public RoomManager roomManager;

    void Start()
    {
        if (roomManager == null) roomManager = GetComponentInParent<RoomManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            roomManager.EnterRoom(); // �濡 �� ��
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            roomManager.CheckExitRoom(collision); // ���� ���� �� ó��
        }
    }
}

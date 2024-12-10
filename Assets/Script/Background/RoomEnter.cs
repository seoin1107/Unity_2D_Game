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
            roomManager.EnterRoom(); // 방에 들어갈 때
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            roomManager.CheckExitRoom(collision); // 방을 나갈 때 처리
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class RoomEnter : MonoBehaviour
{
    public RoomManager roomManager;
    public GameObject roomEnter;
    private bool Iscome;

    void Start()
    {
        if (roomManager == null) roomManager = GetComponentInParent<RoomManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && Iscome == false)
        {
            roomManager.EnterRoom(); // 방에 들어갈 때
            Iscome = true;
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class RoomExit : MonoBehaviour
{
    public RoomManager roomManager;
    public GameObject roomExit;
    private bool Isclear;

    void Start()
    {
        if (roomManager == null) roomManager = GetComponentInParent<RoomManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && Isclear == false)
        {
            roomManager.ExitRoom(); // ���� ���� �� ó��
            Isclear = true;
        }
    }
}
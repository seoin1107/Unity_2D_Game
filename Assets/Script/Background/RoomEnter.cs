using Unity.VisualScripting;
using UnityEngine;

public class RoomEnter : MonoBehaviour
{
    public RoomManager roomManager;
    public GameObject roomEnter;
    private bool Iscome;

    void Start()
    {
        BGMManager.Instance.StopSound();
        if (roomManager == null) roomManager = GetComponentInParent<RoomManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && Iscome == false)
        {
            roomManager.EnterRoom(); // �濡 �� ��
            Iscome = true;
            BGMManager.Instance.PlaySound(BGMManager.Instance.BoosRoomBgm);
        }
    }
}

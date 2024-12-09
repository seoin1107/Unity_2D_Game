using UnityEngine;
using Cinemachine;

public class RoomManager : MonoBehaviour
{
    public CinemachineVirtualCamera targetCamera; //이동할 방의 카메라
    LayerMask layerMask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // 플레이어가 방에 들어오면 카메라 우선순위를 높임
            targetCamera.Priority = 20;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // 플레이어가 방을 나가면 카메라 우선순위를 낮춤
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
        doorCollider.enabled = true; // 문 잠금 (벽 활성화)
    }

    public void UnlockDoor()
    {
        doorCollider.enabled = false; // 문 해제 (벽 비활성화)
    }
}

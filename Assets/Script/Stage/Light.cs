using UnityEngine;

public class LightingController : MonoBehaviour
{
    public Light playerLight;  // 플레이어 주변에 있는 라이트
    public Transform player;     // 플레이어 객체
    

    void Update()
    {
        // 플레이어 위치에 맞춰 라이트 위치 설정
        playerLight.transform.position = player.position;
    }
}

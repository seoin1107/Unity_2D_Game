using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Transform player; // 플레이어 Transform
    public float xParallaxFactor = 0.5f; // X축 Parallax 비율
    public float yParallaxFactor = 0.1f; // Y축 감쇠 비율

    private Vector3 previousPlayerPosition;

    void Start()
    {
        // 플레이어의 초기 위치 저장
        previousPlayerPosition = player.position;
    }

    void Update()
    {
        Vector3 playerDelta = player.position - previousPlayerPosition;

        // Parallax 효과 계산
        float targetX = transform.position.x + playerDelta.x * xParallaxFactor;
        float targetY = Mathf.Lerp(transform.position.y, transform.position.y + playerDelta.y, yParallaxFactor);

        // 위치 적용
        transform.position = new Vector3(targetX, targetY, transform.position.z);

        // 플레이어 위치 갱신
        previousPlayerPosition = player.position;
    }
}

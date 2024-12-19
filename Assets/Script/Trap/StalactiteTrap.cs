using UnityEngine;

public class StalactiteTrap : MonoBehaviour
{
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1.0f; // 중력 적용

        // 종유석 자식 오브젝트가 비활성화되었을 경우, 활성화
        Transform stalactite = transform.Find("Stalactite"); // 자식 오브젝트 이름이 "Stalactite"인 경우
        if (stalactite != null && !stalactite.gameObject.activeSelf)
        {
            stalactite.gameObject.SetActive(true); // 자식 종유석 활성화
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // 플레이어 대미지 처리
            Player2D player = other.GetComponent<Player2D>();
            if (player != null)
            {
                player.OnTrapDamage(10.0f);
            }
            DestroyTrap();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // 바닥에 닿으면 파괴
            DestroyTrap();
        }
    }

    private void DestroyTrap()
    {
        // 종유석 파괴 후 다시 비활성화
        gameObject.SetActive(false);
    }
}

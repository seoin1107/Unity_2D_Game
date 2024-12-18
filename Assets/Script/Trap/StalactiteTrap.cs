using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ground와 충돌 시 파괴
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            DestroyTrap();
        }

        // Player와 충돌 시 대미지 주고 파괴
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // 플레이어에게 대미지 주기
            Player2D player = collision.gameObject.GetComponent<Player2D>();
            if (player != null)
            {
                player.OnTrapDamage(10.0f);  // 대미지 예시
            }

            DestroyTrap();
        }
    }

    private void DestroyTrap()
    {
        // 오브젝트 풀에 반환하려면 SetActive(false)로 비활성화
        gameObject.SetActive(false);
        
    }
}

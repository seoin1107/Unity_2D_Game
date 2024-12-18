using UnityEngine;

public class HiddingTrap : MonoBehaviour
{
    private Animator animator;
    private bool isTrapActive = false; // 가시가 이미 활성화되었는지 확인

    public float damage = 10.0f; // 가시에 닿으면 입는 대미지
    public float resetTime = 1.5f; // 가시가 내려간 후 재활성화 시간

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTrapActive && collision.gameObject.layer == LayerMask.NameToLayer("Player")) // 플레이어와 충돌 시
        {
            isTrapActive = true;
            animator.SetTrigger("OnRise"); // 가시 솟아오르는 애니메이션 실행
            //animator.SetBool("IsActive", true);

            // 플레이어에 대미지 주기
            Player2D player = collision.GetComponent<Player2D>();
            if (player != null)
            {
                player.OnTrapDamage(damage);
            }

            // 가시가 다시 내려가도록 타이머 실행
            Invoke("ResetTrap", resetTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void ResetTrap()
    {
        animator.SetBool("IsActive", false); // 애니메이션 상태를 Idle로 변경
        isTrapActive = false; // 가시 상태 초기화
    }
}

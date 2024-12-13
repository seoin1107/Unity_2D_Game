using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public GameObject player;
    public float damage = 10f;  // 가시가 줄 대미지
    public float damageInterval = 1f;  // 대미지를 주는 간격 (1초)    
    public float timeSinceLastDamage = 0f;  // 마지막 대미지에서 경과한 시간
    private bool isPlayerOnTrap = false;  // 플레이어가 가시에 있는지 여부

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // IDamage 인터페이스를 구현한 객체를 찾기
            var damageable = collision.GetComponent<IDamage>();
            if (damageable != null)
            {
                damageable.OnDamage(damage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // 플레이어가 가시에 있으면 isPlayerOnTrap을 true로 설정
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                isPlayerOnTrap = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 플레이어가 함정을 떠나면 대미지 타이머 초기화
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            timeSinceLastDamage = 0f;
        }
    }

    private void Update()
    {
        // 플레이어가 가시에 있으면 시간 누적
        if (isPlayerOnTrap)
        {
            timeSinceLastDamage += Time.deltaTime;  // 시간이 누적됨

            if (timeSinceLastDamage >= damageInterval)
            {
                // 대미지 주기
                var damageable = GetComponent<IDamage>();
                if (damageable != null)
                {
                    damageable.OnDamage(damage);  // 대미지 주기
                }

                // 타이머 초기화
                timeSinceLastDamage = 0f;
            }
        }
    }
}

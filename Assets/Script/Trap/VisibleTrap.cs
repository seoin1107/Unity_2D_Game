using UnityEngine;

public class VisibleTrap : MonoBehaviour
{
    public float damage = 10.0f;  // 가시가 줄 대미지
    public float damageInterval = 1.0f;  // 대미지를 주는 간격 (1초)    
    private float timeSinceLastDamage = 0.0f;  // 마지막 대미지에서 경과한 시간
    private GameObject Player = null;  // 현재 가시에 올라와 있는 플레이어 객체

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var damageable = collision.GetComponent<IDamage>();
            if (damageable != null)
            {
                damageable.OnDamage(damage);  // 처음 가시에 밟을 때 대미지 주기
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // 플레이어 객체를 추적
            Player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // 플레이어가 가시에서 벗어나면 상태 초기화
            Player = null;
            timeSinceLastDamage = 0.0f;
        }
    }

    private void Update()
    {
        if (Player != null)
        {
            // 시간이 누적되고, 1초 간격으로 대미지 주기
            timeSinceLastDamage += Time.deltaTime;

            if (timeSinceLastDamage >= damageInterval)
            {
                // 대미지 주기
                var damageable = Player.GetComponent<IDamage>();
                if (damageable != null)
                {
                    damageable.OnDamage(damage);  // 대미지 주기
                }

                // 타이머 초기화
                timeSinceLastDamage = 0.0f;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public LayerMask crashMask; // 충돌 마스크
    private bool isFire = false; //발사되었는지 여부

    public float speed = 5.0f;
    public float damage = 10.0f;
    private Vector2 moveDirection = Vector2.right; // 기본 이동 방향
    public float maxDistance = 10f;  // 파이어볼이 이동할 최대 거리
    private float traveledDistance = 0f;  // 현재까지 이동한 거리

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        // SpriteRenderer 컴포넌트 초기화
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (isFire)
        {
            // 파이어볼 이동 처리
            float dist = speed * Time.deltaTime; // 이동 거리 계산
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, dist, crashMask);
            if (hit.collider != null && hit.collider.gameObject.layer
                == LayerMask.NameToLayer("Player"))
            {
                // 충돌 대상에서 IDamage 인터페이스 확인
                IDamage target = hit.collider.GetComponent<IDamage>();
                if (target != null)
                {
                    EFFECTManager.Instance.PlaySound(EFFECTManager.Instance.EffectAttack);
                    target.OnDamage(damage); // 데미지 전달
                }
                DestroyObject(hit.point); // 충돌 위치에서 오브젝트 처리
                return; // 더 이상 이동하지 않음
            }

            transform.Translate(moveDirection * dist); // 우측 방향으로 이동
            traveledDistance += dist; // 이동한 거리 누적

            // 일정 거리 이상 이동했으면 파이어볼 삭제
            if (traveledDistance >= maxDistance)
            {
                Destroy(gameObject); // 파이어볼 삭제
                return; // 더 이상 이동하지 않음
            }
        }
    }
    public void OnFire(Vector2 direction)
    {
        isFire = true;
        transform.parent = null;
        moveDirection = direction.normalized; // 방향 설정 및 정규화
                                              
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = direction.x < 0; // 왼쪽으로 발사하면 뒤집기
        }
    }

    void DestroyObject(Vector2 pos)
    {
        // 현재 오브젝트 삭제
        Destroy(gameObject);
    }
}

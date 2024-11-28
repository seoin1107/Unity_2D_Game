using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public LayerMask crashMask; // 충돌 마스크
    private bool isFire = false; // 파이어볼이 발사되었는지 여부

    public float speed = 5.0f;
    public float damage = 10.0f;
    private Vector2 moveDirection = Vector2.right; // 기본 이동 방향
    void Update()
    {
        if (isFire)
        {
            // 파이어볼 이동 처리
            float dist = speed * Time.deltaTime; // 이동 거리 계산
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, dist, crashMask);

            if (hit.collider != null) // 충돌한 오브젝트가 있는 경우
            {
                DestroyObject(hit.point); // 충돌 위치에서 오브젝트 처리
                return; // 더 이상 이동하지 않음
            }

            transform.Translate(moveDirection * dist); // 우측 방향으로 이동
        }
    }
    public void OnFire(Vector2 direction)
    {
        isFire = true;
        transform.parent = null;
        moveDirection = direction.normalized; // 방향 설정 및 정규화
    }

    void DestroyObject(Vector2 pos)
    {
        // 현재 오브젝트 삭제
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // 충돌 대상에서 IDamage 인터페이스 확인
            IDamage target = collision.GetComponent<IDamage>();
            if (target != null)
            {
                target.OnDamage(damage); // 데미지 전달
            }

            // FireBall 파괴
            Destroy(gameObject);
        }
    }
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    // 충돌 유지 시 처리
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    // 충돌 종료 시 처리
    //}
}

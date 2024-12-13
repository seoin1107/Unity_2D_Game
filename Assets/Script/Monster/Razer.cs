using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Razer : RazerPoint
{
    public LayerMask crashMask; // 충돌 마스크
    private bool isRazer = false;
    public float speed = 10.0f;
    public float damage = 10.0f;

    private Vector2 moveDirection = Vector2.right;
    public float maxDistance = 20f;
    private float traveledDistance = 0f;

    private bool isDestroying = false; // 파괴 상태 플래그

    void Update()
    {
        if (isRazer && !isDestroying) //파괴중이 아니면 이동
        {
            float dist = speed * Time.deltaTime; // 이동 거리 계산
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, dist, crashMask);
            if (hit.collider != null && hit.collider.gameObject.layer
                == LayerMask.NameToLayer("Player"))
            {
                myAnim.SetBool(animData.IsDestorying, true);
                isDestroying = true; // 파괴 상태로 전환
                // 충돌 대상에서 IDamage 인터페이스 확인
                IDamage target = hit.collider.GetComponent<IDamage>();
                if (target != null)
                {
                    target.OnDamage(damage); // 데미지 전달
                }
                // DestroyObject(hit.point); // 충돌 위치에서 오브젝트 처리
                StartCoroutine(DelayDestroyObject());
                return; // 더 이상 이동하지 않음
            }

            transform.Translate(moveDirection * dist); // 우측 방향으로 이동
            traveledDistance += dist; // 이동한 거리 누적
            if (traveledDistance >= maxDistance)
            {
                Destroy(gameObject);
                return; // 더 이상 이동하지 않음
            }
        }
    }
    public void OnRazer(Vector2 direction)
    {
        isRazer = true;
        transform.parent = null;
        moveDirection = direction.normalized; // 방향 설정 및 정규화
    }
    void DestroyObject()
    {
        Destroy(gameObject);
    }
    IEnumerator DelayDestroyObject()
    {
        yield return new WaitForSeconds(0.3f); // 0.5초 대기
        DestroyObject(); // 오브젝트 삭제
    }
}
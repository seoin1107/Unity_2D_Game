using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public LayerMask crashMask; // 충돌 마스크

    void Update()
    {
        // 파이어볼 이동
        float dist = 5.0f * Time.deltaTime;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, dist, crashMask);
        if (hit.collider != null)
        {
            DestroyObject(hit.point); // 충돌 위치에서 폭발 처리
        }
        transform.Translate(Vector2.right * dist);
    }

    void DestroyObject(Vector2 pos)
    {
/*        // 폭발 효과 생성
        GameObject obj = Instantiate(orgEffect);
        obj.transform.position = pos;
*/
        // 현재 오브젝트 삭제
        Destroy(gameObject);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        // 충돌 유지 시 처리
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 충돌 종료 시 처리
    }
}

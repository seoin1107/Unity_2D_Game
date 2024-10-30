using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIMovementSub : Movement
{
    private Coroutine aiMove;
    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMove(Vector2 pos, UnityAction act)
    {
        // 목표 위치까지의 방향을 계산하고 거리 점검
        Vector2 dir = pos - (Vector2)transform.position;
        if (dir.magnitude < 0.1f) // 목표 지점이 가까운 경우
        {
            act?.Invoke(); // 완료 액션 호출
            return;
        }

        if (aiMove != null) StopCoroutine(aiMove); // 기존 이동 중지
        aiMove = StartCoroutine(MovingTowards(pos, act)); // 새로운 이동 시작
    }

    private IEnumerator MovingTowards(Vector2 target, UnityAction act)
    {
        while (Vector2.Distance(transform.position, target) > 0.1f) // 목표 지점에 도달할 때까지 반복
        {
            Vector2 dir = (target - (Vector2)transform.position).normalized; // 방향 정규화
            float delta = Time.deltaTime * moveSpeed; // 이동 거리 계산

            // Rigidbody2D로 이동
            rb2D.MovePosition(rb2D.position + dir * delta);

            // 애니메이션 상태 업데이트
            if (myAnim != null) // myAnim이 null이 아닐 경우만
            {
                myAnim.SetBool(animData.IsMove, true);
            }

            yield return null; // 다음 프레임까지 대기
        }

        if (myAnim != null)
        {
            myAnim.SetBool(animData.IsMove, false); // 이동 완료 후 애니메이션 종료
        }
        act?.Invoke(); // 완료 액션 호출
        aiMove = null; // 현재 이동 코루틴 참조 해제
    }

    protected new void OnFollow(Transform target)
    {
        if (aiMove != null) StopCoroutine(aiMove); // 기존 이동 중지
        aiMove = StartCoroutine(Following(target)); // 새로운 추적 시작
    }

    private IEnumerator Following(Transform target)
    {
        while (target != null)
        {
            Vector2 dir = target.position - transform.position; // 목표 위치와 현재 위치 간의 방향 벡터
            if (dir.magnitude > 0.1f) // 목표 지점이 가까운 경우
            {
                dir.Normalize(); // 방향 정규화
                float delta = Time.deltaTime * moveSpeed; // 이동 거리 계산
                rb2D.MovePosition(rb2D.position + dir * delta); // Rigidbody2D를 통해 이동

                if (myAnim != null)
                {
                    myAnim.SetBool(animData.IsMove, true); // 이동 애니메이션 설정
                }
            }
            else
            {
                if (myAnim != null)
                {
                    myAnim.SetBool(animData.IsMove, false); // 이동 애니메이션 종료
                }
            }

            yield return null; // 다음 프레임까지 대기
        }
        if (myAnim != null)
        {
            myAnim.SetBool(animData.IsMove, false); // 추적 중지 시 애니메이션 종료
        }
        aiMove = null; // 현재 추적 코루틴 참조 해제
    }
}

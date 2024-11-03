using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using UnityEditor;

public class Movement : BattleSystem
{
    public float moveSpeed = 2.0f;  // 이동 속도

    Coroutine move = null;
    //int PlayerLayer, GroundLayer, FloorLayer;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerLayer = LayerMask.NameToLayer("Player");
        //GroundLayer = LayerMask.NameToLayer("Ground");
        //FloorLayer = LayerMask.NameToLayer("Floor");
    }

    // Update is called once per frame
    void Update()
    {
        ////점프시 플레이어&플로어 충돌무시
        //if (rid.velocity.y > 0.00f)
        //{
        //    Physics2D.IgnoreLayerCollision(PlayerLayer, FloorLayer, true);
        //}
        ////평소에는 충돌
        //else
        //{
        //    Physics2D.IgnoreLayerCollision(PlayerLayer, FloorLayer, false);
        //}
    }

    protected void OnStop()
    {
        if (move != null) StopCoroutine(move);
        move = null;
    }

    public void OnFollow(Transform target)
    {
        OnStop();
        move = StartCoroutine(Following(target)); // 추적 시작
    }

    private IEnumerator Following(Transform target)
    {
        while (target != null)
        {
            playTime += Time.deltaTime; // 시간 누적
            Vector2 dir = target.position - transform.position; // 목표 위치 방향 계산
            float dist = dir.magnitude; // 목표까지의 거리


            if (dist > battleStat.AttackRange && !myAnim.GetBool(animData.IsAttack)) // 공격 범위 이내가 아닐 때
            {
                myAnim.SetBool(animData.IsMove, true); // 이동 애니메이션 설정
                dir.Normalize(); // 방향 정규화

                float delta = Time.deltaTime * moveSpeed; // 이동 거리 계산
                if (delta > dist) delta = dist; // 남은 거리보다 클 경우 조정
                transform.Translate(dir * delta, Space.World); // 이동
            }
            else
            {
                if (myAnim != null) myAnim.SetBool(animData.IsMove, false); // 이동 애니메이션 종료
                if (playTime >= battleStat.AttackDelay)
                {
                    playTime = 0.0f; // 재설정
                    if (myAnim != null) myAnim.SetTrigger(animData.OnAttack); // 공격 트리거 설정
                }
            }

            yield return null; // 다음 프레임까지 대기
        }

        if (myAnim != null) myAnim.SetBool(animData.IsMove, false); // 추적 중지 시 애니메이션 종료

    }

    [SerializeField] Rigidbody2D rid;
    public bool IsJumping = false;
    public float jumpForce = 1;
    public bool IsMoving = false;
    public float MoveSpeed = 3;
    public float spaceCoolTime = 5.0f; // 회피 쿨타임
    public float curSpaceCool = 5.0f; // 회피 쿨타임 계산
    

    public void OnJump()
    {
        if (Input.GetKeyDown(KeyCode.W) )
        {
            IsJumping = true;
            rid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            myAnim.SetTrigger("OnJump");
        }
    }

    public void OnMove()
    {
        // A/D 키 입력을 통한 좌우 이동
        float h = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(h, 0);
        if (h != 0)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            myAnim.SetBool("IsMoving", true);
            if (Input.GetKeyDown(KeyCode.Space))//스페이스와 a/d입력이 있는 경우에만 회피 작동
            {
                OnDodge(moveDirection);
            }
        }
        else
        {
            myAnim.SetBool("IsMoving", false);
            transform.Translate(new Vector2(h, 0) * Time.deltaTime);
        }
    }

    public new void OnAttack()
    {
        if(Input.GetMouseButtonDown(0) && !myAnim.GetBool(animData.IsAttack))
        {
            myAnim.SetTrigger("OnAttack");
        }
    }


    IEnumerator Dodging(Vector2 rl) //스페이스바 입력시 회피 코루틴
    {
       
        float duration = 0.5f; // 이동 시간
        float elapsed = 0f;  //이동 시간 계산
        if (curSpaceCool>=5.0f) //현재 스페이스 쿨타임 계산
        {
            myAnim.SetBool(animData.IsDodge, true);
            myAnim.SetTrigger(animData.OnDodge);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Monster"));
            rid.gravityScale = 0.0f;//중력 삭제
            rid.velocity = Vector2.zero;
            curSpaceCool = 0.0f; //스페이스 쿨타임 시작

            while (elapsed < duration)
            {
                transform.Translate(rl * 10 * Time.deltaTime);
                elapsed += Time.deltaTime;
                yield return null;
            }
            myAnim.SetBool(animData.IsDodge, false);
            rid.gravityScale = 1.0f; //중력 복구
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Monster"), false);

        }
    }
    public void OnDodge(Vector2 rl) //회피 코루틴 동작
    {
        StartCoroutine(Dodging(rl));
    }
}

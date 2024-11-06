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
    int PlayerLayer, GroundLayer, FloorLayer;

    // Start is called before the first frame update
    void Start()
    {
        rid = GetComponent<Rigidbody2D>();
        PlayerLayer = LayerMask.NameToLayer("Player");
        GroundLayer = LayerMask.NameToLayer("Ground");
        FloorLayer = LayerMask.NameToLayer("Floor");
    }

    // Update is called once per frame
    void Update()
    {
        //평소에는 충돌
        if (IsJumping == true)
        {
            //점프시 플레이어&플로어 충돌무시
            Physics2D.IgnoreLayerCollision(PlayerLayer, FloorLayer, true);
        }
        else
        { 
            Physics2D.IgnoreLayerCollision(PlayerLayer, FloorLayer, false);
        }
    }

    protected void OnStop()
    {
        if (move != null) StopCoroutine(move);
        move = null;
    }


    [SerializeField] Rigidbody2D rid;
    public bool IsJumping = false;
    public float jumpForce = 1;
    public byte JumpCount;
    public bool IsMoving = false;
    public float MoveSpeed = 3;
    public float spaceCoolTime = 5.0f; // 회피 쿨타임
    public float curSpaceCool = 5.0f; // 회피 쿨타임 계산
    public LayerMask enemMask;

    //처음부터 더블점프가 포함된 코드
    //이후 조건 충족하면 JumpConunt를 -1로 초기화 하여 2단점프 가능하게
    public void OnJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && JumpCount < 2)
        {
            IsJumping = true;
            rid.velocity = Vector2.zero;
            rid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            myAnim.SetTrigger("OnJump");
            JumpCount++;
        }
    }

    //착지시 점프카운트 초기화
    //여기는 if에 &&으로 조건 추가해서 메인 JumpCount를 -1로 초기화하고 else로 0으로 초기화
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            JumpCount = 0;
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
        // 마우스 좌클릭 시 공격 애니메이션 트리거 설정
        if (Input.GetMouseButtonDown(0) && !myAnim.GetBool(animData.IsAttack))
        {
            myAnim.SetTrigger("OnAttack");

            // 공격 범위 내의 적 탐지 및 데미지 적용
            Vector2 attackPosition = (Vector2)transform.position + (Vector2)transform.right; // 공격 범위 중심
            float attackRadius = 1.0f; // 공격 범위 반지름
            Collider2D[] list = Physics2D.OverlapCircleAll(attackPosition, attackRadius, enemMask);

            // 탐지된 적들에게 데미지 적용
            if (list.Length > 0)
            {
                foreach (Collider2D col in list)
                {
                    IBattle ibat = col.GetComponent<IBattle>();
                    if (ibat != null && ibat.IsLive)
                    {
                        ibat.OnDamage(30.0f);
                    }
                }
            }
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

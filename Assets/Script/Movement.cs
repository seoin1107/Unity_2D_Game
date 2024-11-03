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
    public float moveSpeed = 2.0f;  // �̵� �ӵ�

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
        ////������ �÷��̾�&�÷ξ� �浹����
        //if (rid.velocity.y > 0.00f)
        //{
        //    Physics2D.IgnoreLayerCollision(PlayerLayer, FloorLayer, true);
        //}
        ////��ҿ��� �浹
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
        move = StartCoroutine(Following(target)); // ���� ����
    }

    private IEnumerator Following(Transform target)
    {
        while (target != null)
        {
            playTime += Time.deltaTime; // �ð� ����
            Vector2 dir = target.position - transform.position; // ��ǥ ��ġ ���� ���
            float dist = dir.magnitude; // ��ǥ������ �Ÿ�


            if (dist > battleStat.AttackRange && !myAnim.GetBool(animData.IsAttack)) // ���� ���� �̳��� �ƴ� ��
            {
                myAnim.SetBool(animData.IsMove, true); // �̵� �ִϸ��̼� ����
                dir.Normalize(); // ���� ����ȭ

                float delta = Time.deltaTime * moveSpeed; // �̵� �Ÿ� ���
                if (delta > dist) delta = dist; // ���� �Ÿ����� Ŭ ��� ����
                transform.Translate(dir * delta, Space.World); // �̵�
            }
            else
            {
                if (myAnim != null) myAnim.SetBool(animData.IsMove, false); // �̵� �ִϸ��̼� ����
                if (playTime >= battleStat.AttackDelay)
                {
                    playTime = 0.0f; // �缳��
                    if (myAnim != null) myAnim.SetTrigger(animData.OnAttack); // ���� Ʈ���� ����
                }
            }

            yield return null; // ���� �����ӱ��� ���
        }

        if (myAnim != null) myAnim.SetBool(animData.IsMove, false); // ���� ���� �� �ִϸ��̼� ����

    }

    [SerializeField] Rigidbody2D rid;
    public bool IsJumping = false;
    public float jumpForce = 1;
    public bool IsMoving = false;
    public float MoveSpeed = 3;
    public float spaceCoolTime = 5.0f; // ȸ�� ��Ÿ��
    public float curSpaceCool = 5.0f; // ȸ�� ��Ÿ�� ���
    

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
        // A/D Ű �Է��� ���� �¿� �̵�
        float h = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(h, 0);
        if (h != 0)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            myAnim.SetBool("IsMoving", true);
            if (Input.GetKeyDown(KeyCode.Space))//�����̽��� a/d�Է��� �ִ� ��쿡�� ȸ�� �۵�
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


    IEnumerator Dodging(Vector2 rl) //�����̽��� �Է½� ȸ�� �ڷ�ƾ
    {
       
        float duration = 0.5f; // �̵� �ð�
        float elapsed = 0f;  //�̵� �ð� ���
        if (curSpaceCool>=5.0f) //���� �����̽� ��Ÿ�� ���
        {
            myAnim.SetBool(animData.IsDodge, true);
            myAnim.SetTrigger(animData.OnDodge);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Monster"));
            rid.gravityScale = 0.0f;//�߷� ����
            rid.velocity = Vector2.zero;
            curSpaceCool = 0.0f; //�����̽� ��Ÿ�� ����

            while (elapsed < duration)
            {
                transform.Translate(rl * 10 * Time.deltaTime);
                elapsed += Time.deltaTime;
                yield return null;
            }
            myAnim.SetBool(animData.IsDodge, false);
            rid.gravityScale = 1.0f; //�߷� ����
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Monster"), false);

        }
    }
    public void OnDodge(Vector2 rl) //ȸ�� �ڷ�ƾ ����
    {
        StartCoroutine(Dodging(rl));
    }
}

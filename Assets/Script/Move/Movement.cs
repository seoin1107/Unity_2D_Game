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
        //��ҿ��� �浹
        if (IsJumping == true)
        {
            //������ �÷��̾�&�÷ξ� �浹����
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
    public float spaceCoolTime = 5.0f; // ȸ�� ��Ÿ��
    public float curSpaceCool = 5.0f; // ȸ�� ��Ÿ�� ���
    public LayerMask enemMask;

    //ó������ ���������� ���Ե� �ڵ�
    //���� ���� �����ϸ� JumpConunt�� -1�� �ʱ�ȭ �Ͽ� 2������ �����ϰ�
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

    //������ ����ī��Ʈ �ʱ�ȭ
    //����� if�� &&���� ���� �߰��ؼ� ���� JumpCount�� -1�� �ʱ�ȭ�ϰ� else�� 0���� �ʱ�ȭ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            JumpCount = 0;
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
        // ���콺 ��Ŭ�� �� ���� �ִϸ��̼� Ʈ���� ����
        if (Input.GetMouseButtonDown(0) && !myAnim.GetBool(animData.IsAttack))
        {
            myAnim.SetTrigger("OnAttack");

            // ���� ���� ���� �� Ž�� �� ������ ����
            Vector2 attackPosition = (Vector2)transform.position + (Vector2)transform.right; // ���� ���� �߽�
            float attackRadius = 1.0f; // ���� ���� ������
            Collider2D[] list = Physics2D.OverlapCircleAll(attackPosition, attackRadius, enemMask);

            // Ž���� ���鿡�� ������ ����
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

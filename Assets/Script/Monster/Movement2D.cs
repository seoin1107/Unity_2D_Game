using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Movement2D : SpriteProperty
{
    Collider2D _collider = null;
    public Collider2D myColider
    {
        get
        {
            if (_collider == null)
            {
                _collider = GetComponentInChildren<Collider2D>();
            }
            return _collider;
        }
    }
    Rigidbody2D _rigid = null;
    public Rigidbody2D myRigid
    {
        get
        {
            if (_rigid == null)
            {
                _rigid = GetComponentInChildren<Rigidbody2D>();
            }
            return _rigid;
        }
    }

    public enum SPRITEDIR
    {
        right, left
    }

    public SPRITEDIR lookDir = SPRITEDIR.right;

    [SerializeField] Rigidbody2D rid;
    /*    public float moveSpeed = 2.0f;*/
    public float curSpaceCool = 0.0f; // ȸ�� ��Ÿ�� ���
    public float spaceCoolDown = 5.0f;

    protected Vector2 moveDir;
    protected float deltaDist;

    private bool isFloor = false; // floor���������� ȣ��
    // Start is called before the first frame update
    void Start()
    {

    }

    protected virtual void OnCheckGround(Transform tr)
    {

    }

    bool GetFlip()
    {
        return lookDir == SPRITEDIR.right;
    }

    // Update is called once per frame
    protected void OnUpdate()
    {
        myRenderer.flipX = moveDir.x < 0.0f ? GetFlip() : moveDir.x > 0.0f ? !GetFlip() : myRenderer.flipX;        //0���� ������ x���� ture(�ٶ󺸱���ȯ)
        myAnim.SetBool(animData.IsMove, moveDir.x != 0.0f ? true : false);
        deltaDist = Time.deltaTime * characterStat.moveSpeed;
        transform.Translate(moveDir * deltaDist);

        // ������ ��Ÿ���� ������ ����
        if (curSpaceCool < spaceCoolDown)
        {
            curSpaceCool += Time.deltaTime;
        }
    }

    protected void OnJump()
    {
        StopAllCoroutines();
        StartCoroutine(Jumping());
    }

    IEnumerator Jumping()
    {
        myRigid.AddForce(Vector2.up * 400.0f);
        yield return new WaitForFixedUpdate();

        myColider.isTrigger = true;
        while (myRigid.velocity.y >= 0.0f) //���οö󰡴���
        {
            yield return null;
        }
        myColider.isTrigger = false;

    }

    protected void OnDownJump()
    {
        if (isFloor)
        {
            StopAllCoroutines();
            StartCoroutine(DownJumping());
        }
    }
    IEnumerator DownJumping()
    {
        myRigid.AddForce(Vector2.up * 50.0f);
        yield return new WaitForFixedUpdate();

        myColider.isTrigger = true;
        yield return new WaitForSeconds(0.7f);
        myColider.isTrigger = false;
        while (myRigid.velocity.y < 0.0f) //��������
        {
            yield return null;
        }
    }



    protected void OnDodge()
    {
        StopAllCoroutines();
        StartCoroutine(Dodging());
    }

    IEnumerator Dodging() //�����̽��� �Է½� ȸ�� �ڷ�ƾ
    {
        if (curSpaceCool >= spaceCoolDown)
        {
            myColider.isTrigger = false;
            float duration = 0.5f; // �̵� �ð�
            float elapsed = 0f;  //�̵� �ð� ���
            Vector2 rl = myRenderer.flipX ? Vector2.left : Vector2.right;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Monster"));
            myAnim.SetTrigger(animData.OnDodge);
            curSpaceCool = 0.0f;
            while (elapsed < duration)
            {
                transform.Translate(rl * 10 * Time.deltaTime);
                elapsed += Time.deltaTime;
                yield return null;
            }
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Monster"), false);
        }
    }
    protected void OnParry()
    {
        StopAllCoroutines();
        StartCoroutine(Parring());
    }
    IEnumerator Parring()
    {
        myColider.isTrigger = false;
        if (myAnim.GetBool(animData.IsParry)) yield break; // �̹� Ȱ��ȭ�� ��� ����

        myAnim.SetTrigger(animData.OnParry);
        myAnim.SetBool(animData.IsParry, true);
        yield return new WaitForSeconds(0.75f); // 1�� ���
        myAnim.SetBool(animData.IsParry, false); // IsParry ��Ȱ��ȭ
    }
    protected void OnPlayerAttack()
    {
        StopAllCoroutines();
        StartCoroutine(Attacking());
    }
    IEnumerator Attacking()
    {
        myAnim.SetTrigger(animData.OnAttack);
        myAnim.SetBool(animData.IsAttack, true);
        yield return new WaitForSeconds(0.5f); // 1�� ���
        myAnim.SetBool(animData.IsAttack, false);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            myAnim.SetBool("IsAir", false);
            OnCheckGround(collision.transform);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            isFloor = true;     //floor��������
            myAnim.SetBool("IsAir", false);
            /*            OnCheckGround(collision.transform);*/
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            myAnim.SetBool("IsAir", true);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            isFloor = false;
            myAnim.SetBool("IsAir", true);
        }
    }


    /*    public void OnTriggerEnter2D(Collider2D other)
        {
            // ���� ������ ������ ������ �и� Ÿ�̹��� ���缭 ����
            if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
            {
                // �и� ����: ������ ���� ����
                myAnim.SetBool("IsParry", true);
            }
        }*/

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            myAnim.SetBool("IsAir", true);
        }
    }

}

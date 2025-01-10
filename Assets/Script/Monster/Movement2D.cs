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
        if (characterStat.curDodgeCool < characterStat.dodgeCool)
        {
            characterStat.curDodgeCool += Time.deltaTime;
        }
        if (characterStat.curParryingCool < characterStat.parryingCool)
        {
            characterStat.curParryingCool += Time.deltaTime;
        }
        if(characterStat.curHpReCool < 1.0f)
        {
            characterStat.curHpReCool += Time.deltaTime;
            if(characterStat.curHpReCool >= 1.0f)
            {////////////////////////////////////////////////////////////////////////////////////////////////ü�� ��� ü��
                characterStat.curHpReCool = 0.0f;
                if(characterStat.curHP- characterStat.maxHP * characterStat.hpRegen <= characterStat.maxHP){
                    characterStat.curHP += characterStat.maxHP * characterStat.hpRegen;
                }
                else
                {
                    characterStat.curHP = characterStat.maxHP;
                }
            }
        }
    }

    protected void OnJump()
    {
        StopAllCoroutines();
        StartCoroutine(Jumping()); 
       
    }

    protected void OnDoubleJump()
    {
        StopAllCoroutines();
        StartCoroutine(DoubleJumping());
    }

    IEnumerator DoubleJumping()
    {

        if (Input.GetKeyDown(KeyCode.W) && characterStat.jumpCount < characterStat.CanJump)
        {
            myRigid.AddForce(Vector2.up * 600.0f);
            characterStat.jumpCount++;
            Debug.Log(characterStat.CanJump + " " + characterStat.jumpCount);
        }
        myColider.isTrigger = true;
        while (myRigid.velocity.y >= 0.0f) //���οö󰡴���
        {
            yield return null;
        }

        myColider.isTrigger = false;

    }

    IEnumerator Jumping()
    {
        myRigid.AddForce(Vector2.up * 800.0f);
        characterStat.jumpCount++;
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
        yield return new WaitForSeconds(0.3f);
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
        if (characterStat.curDodgeCool >= characterStat.dodgeCool)
        {
            myColider.isTrigger = false;
            float duration = 0.5f; // �̵� �ð�
            float elapsed = 0f;  //�̵� �ð� ���
            Vector2 rl = myRenderer.flipX ? Vector2.left : Vector2.right;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Monster"));
            myAnim.SetTrigger(animData.OnDodge);
            characterStat.curDodgeCool = 0.0f;
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
        characterStat.curParryingCool = 0.0f;
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
            characterStat.jumpCount = 0;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            isFloor = true;     //floor��������
            myAnim.SetBool("IsAir", false);
            characterStat.jumpCount = 0;
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

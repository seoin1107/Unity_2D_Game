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

    [SerializeField] Rigidbody2D rid;
    public float moveSpeed = 2.0f;
    public float curSpaceCool = 5.0f; // 회피 쿨타임 계산

    protected Vector2 moveDir;
    protected float deltaDist;

    private bool isFloor = false; // floor에있을때만 호출
    // Start is called before the first frame update
    void Start()
    {

    }

    protected virtual void OnCheckGround(Transform tr)
    {

    }

    // Update is called once per frame
    protected void OnUpdate()
    {
        myRenderer.flipX = moveDir.x < 0.0f ? true : moveDir.x > 0.0f ? false : myRenderer.flipX;        //0보다 작을땐 x값을 ture(바라보기전환)
        myAnim.SetBool(animData.IsMove, moveDir.x != 0.0f ? true : false);
        deltaDist = Time.deltaTime * moveSpeed;
        transform.Translate(moveDir * deltaDist);

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
        while (myRigid.velocity.y >= 0.0f) //위로올라가는중
        {
            yield return null;
        }
        myColider.isTrigger = false;

    }

    protected void OnDownJump()
    {
        if(isFloor)
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
        while (myRigid.velocity.y < 0.0f) //내려가기
        {
            yield return null;
        }
    }



    protected void OnDodge()
    {
        StopAllCoroutines();
        StartCoroutine(Dodging());
    }

    IEnumerator Dodging() //스페이스바 입력시 회피 코루틴
    {
        myColider.isTrigger = false;
        float duration = 0.5f; // 이동 시간
        float elapsed = 0f;  //이동 시간 계산
        Vector2 rl = myRenderer.flipX ? Vector2.left : Vector2.right;

        myAnim.SetTrigger(animData.OnDodge);
            while (elapsed < duration)
            {
                transform.Translate(rl * 5 * Time.deltaTime);
                elapsed += Time.deltaTime;
                yield return null;
            }
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
            isFloor = true;     //floor에있을때
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



    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            myAnim.SetBool("IsAir", true);
        }
    }

}

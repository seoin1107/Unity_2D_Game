using System.Collections;
using System.Collections.Generic;
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
    public float moveSpeed = 2.0f;
    public float jumpSpeed;
    int PlayerLayer, GroundLayer, FloorLayer;
    public bool m_grounded = false;
    public bool m_rolling = false;
    public Sensor_HeroKnight m_groundSensor;
    public byte JumpCount = 2;

    GameObject OB;

    protected Vector2 moveDir;
    protected float deltaDist;
    // Start is called before the first frame update
    void Start()
    {
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();

        PlayerLayer = LayerMask.NameToLayer("Player");
        GroundLayer = LayerMask.NameToLayer("Ground");
        FloorLayer = LayerMask.NameToLayer("Floor");

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

        float delta = Time.deltaTime;
        /*if (myRigid.velocity.y == 0.000000f) JumpCount = 2; //더블점프시 필요
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
        }*/
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            myAnim.SetBool("IsAir", false);
            OnCheckGround(collision.transform);
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
    }

    protected void OnDownJump()
    {
        StopAllCoroutines();
        StartCoroutine(DownJumping());
    }
    IEnumerator DownJumping()
    {
        myRigid.AddForce(Vector2.down * -1.0f);
        yield return new WaitForFixedUpdate();

        myColider.isTrigger = false;
        while (myRigid.velocity.y < 0.0f) //내려가기
        {
            yield return null;
        }
        myColider.isTrigger = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            myAnim.SetBool("IsAir", false);
            OnCheckGround(collision.transform);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            myAnim.SetBool("IsAir", true);
        }
    }

}

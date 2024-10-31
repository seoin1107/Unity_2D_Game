using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement1 : MonoBehaviour
{
    public Animator myani;

    [System.Serializable]
    public struct MoveStat
    {
        public byte MoveSpeed;
        public short JumpSpeed;
    }

    public MoveStat moveStat = new MoveStat();
    Rigidbody2D rigid;
    int PlayerLayer, GroundLayer, FloorLayer;
    public bool m_grounded = false;
    public bool m_rolling = false;
    public Sensor_HeroKnight m_groundSensor;
    bool IsDoubleJump;
    public byte JumpCount = 2;

    public GameObject OB;
    public float disableTime = 0.7f;

    ////하단점프
    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
    //    {
    //        OB = collision.gameObject;
    //        StartCoroutine(SJump());
    //    }
    //}
    //IEnumerator SJump()
    //{
    //    while (true)
    //    {
    //        if (Input.GetKeyDown(KeyCode.S))
    //        {
    //            OB.SetActive(false);
    //            yield return new WaitForSeconds(disableTime);
    //            OB.SetActive(true);
    //        }
    //        yield return null;
    //    }
    //}
    ////여기까지

    // Start is called before the first frame update
    void Start()
    {
        myani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();

        PlayerLayer = LayerMask.NameToLayer("Player");
        GroundLayer = LayerMask.NameToLayer("Ground");
        FloorLayer = LayerMask.NameToLayer("Floor");
    }

    // Update is called once per frame
    void Update()
    {

        float delta = Time.deltaTime;
        if (rigid.velocity.y == 0.000000f) JumpCount = 2; //더블점프시 필요

        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            myani.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            myani.SetBool("Grounded", m_grounded);
        }

        //좌우무빙이라 새로 배운 Input.GetAxis("Horizontal"); 사용해도 괜찮을듯
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector2.left * delta * moveStat.MoveSpeed);
                myani.SetInteger("AnimState", 1);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector2.right * delta * moveStat.MoveSpeed);
                myani.SetInteger("AnimState", 1);
            }
        }

        myani.SetFloat("AirSpeedY", rigid.velocity.y); //Falling 애니메이션

        //점프
        if (rigid.velocity.y == 0.00000f && Input.GetKeyDown(KeyCode.W))
        {
            --JumpCount;
            rigid.AddForce(Vector2.up * moveStat.JumpSpeed, ForceMode2D.Force);
            //IsDoubleJump = true;            
            myani.SetTrigger("Jump");
            m_grounded = false;
            myani.SetBool("Grounded", m_grounded);
        }

        //점프시 플레이어&플로어 충돌무시
        if (rigid.velocity.y > 0.00f)
        {
            Physics2D.IgnoreLayerCollision(PlayerLayer, FloorLayer, true);
        }

        //하단점프시 충돌무시
        else if (rigid.velocity.y <= 0.00f && Input.GetKey(KeyCode.S))
        {
            Physics2D.IgnoreLayerCollision(PlayerLayer, FloorLayer, true);
        }

        //평소에는 충돌
        else
        {
            Physics2D.IgnoreLayerCollision(PlayerLayer, FloorLayer, false);
        }

        //더블점프 활성화시
        if (IsDoubleJump == true && JumpCount > 0 && rigid.velocity.y != 0.0000f && Input.GetKeyDown(KeyCode.W))
        {
            --JumpCount;
            rigid.AddForce(Vector2.up * moveStat.JumpSpeed, ForceMode2D.Force);
        }

    }
}

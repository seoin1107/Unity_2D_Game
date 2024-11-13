using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Player2D : BattleSystem2D
{
    public LayerMask myEnemy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");                                     // �¿��̵�

        if (Input.GetKeyDown(KeyCode.W) && !myAnim.GetBool("IsAir"))          // ������
        {
            OnJump();
        }

        if (Input.GetKeyDown(KeyCode.S) && !myAnim.GetBool("IsAir"))          // �Ʒ�����
        {
            OnDownJump();
        }
                    
        if (Input.GetMouseButtonDown(0))                                                // ����Ű
        {
            myAnim.SetTrigger(animData.OnAttack);
        }

        if(Input.GetKeyDown(KeyCode.Space))                                          // ȸ��(������&�뽬)
        {
            OnDodge();
        }

        if(Input.GetMouseButtonDown(1))                                              // �и�Ű
        {
            myAnim.SetTrigger(animData.OnParry);
        }

        base.OnUpdate();
    }

    public void OnAttack() // ���ݹ��� ����
    {
        Vector2 dir = new Vector2(myRenderer.flipX ? -1.0f : 1.0f, 0.0f);
        Collider2D[] list = Physics2D.OverlapCircleAll((Vector2)transform.position + dir, 1.0f, myEnemy);
        foreach (Collider2D col in list)
        {
            col.GetComponent<IDamage>()?.OnDamage(battleStat.AP);
        }
    }
}

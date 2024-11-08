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
        moveDir.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W) && !myAnim.GetBool("IsAir"))
        {
            OnJump();
        }

        if(Input.GetKeyDown(KeyCode.S) && !myAnim.GetBool("IsAir"))
        {
            OnDownJump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            myAnim.SetTrigger(animData.OnAttack);
        }
        base.OnUpdate();
    }

    public void OnAttack() // 공격범위 설정
    {
        Vector2 dir = new Vector2(myRenderer.flipX ? -1.0f : 1.0f, 0.0f);
        Collider2D[] list = Physics2D.OverlapCircleAll((Vector2)transform.position + dir, 1.0f, myEnemy);
        foreach (Collider2D col in list)
        {
            col.GetComponent<IDamage>()?.OnDamage(battleStat.AP);
        }
    }
}

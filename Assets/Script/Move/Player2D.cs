using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Player2D : BattleSystem2D
{
/*    //다이얼로그UI 사용할 수 있게
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }*/

    public LayerMask myEnemy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
/*        if(DialogueUI.IsOpen == false) //대화중이 아닐때만 움직임 가능
        {

        }*/
        moveDir.x = Input.GetAxisRaw("Horizontal");                                     // 좌우이동

        if (Input.GetKeyDown(KeyCode.W) && !myAnim.GetBool("IsAir"))          // 윗점프
        {
            OnJump();
        }

        if (Input.GetKeyDown(KeyCode.S) && !myAnim.GetBool("IsAir"))          // 아랫점프
        {
            OnDownJump();
        }
                    
        if(Input.GetMouseButtonDown(1) && !myAnim.GetBool("IsAir"))           // 패링                                   // 패링키
        {
            OnParry();
        }

        if (Input.GetMouseButtonDown(0) && !myAnim.GetBool("IsAir"))          // 공격                                        // 공격키
        {
            myAnim.SetTrigger(animData.OnAttack);
        }

        if(Input.GetKeyDown(KeyCode.Space))                                   // 회피(구르기&대쉬)
        {
            if (curSpaceCool >= spaceCoolDown)
            {
                OnDodge();
            }
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

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Player2D : BattleSystem2D
{
    //다이얼로그UI 사용할 수 있게
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }

    public LayerMask myEnemy;

    public CharacterStatus playerStatus;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus.characterStat.baseHP = 20;
        playerStatus.characterStat.baseAtk = 10;



        playerStatus.characterStat.moveSpeed = 5.0f;
        playerStatus.characterStat.atkSpeed = 0;
        playerStatus.characterStat.attackRange = 1.0f;


        playerStatus.characterStat.level = 1;

        playerStatus.characterStat.hpRegen = 0;

        playerStatus.characterStat.totalPoint = 1;
        playerStatus.characterStat.atkPoint = 0;
        playerStatus.characterStat.hpPoint = 0;
        playerStatus.characterStat.utilPoint = 0;

        playerStatus.characterStat.hitRecover = 0.5f;
        playerStatus.characterStat.skillCool = 1.0f;
        playerStatus.characterStat.skillDamage = 1.0f;

        playerStatus.characterStat.drain = 0;

        playerStatus.characterStat.dodgeTime = 0.2f;
        playerStatus.characterStat.dodgeCool = 5.0f;
        playerStatus.characterStat.parryingTime = 0.2f;
        playerStatus.characterStat.parryingCool = 2.0f;

        playerStatus.characterStat.needExp = 10;
        playerStatus.characterStat.curExp = 0;
        UpdateStatus();

    }

    // Update is called once per frame
    void Update()
    {
        
        /*        if(DialogueUI.IsOpen == false) //대화중이 아닐때만 움직임 가능
                {

                }*/

        if (!myAnim.GetBool("IsParry") && !myAnim.GetBool("IsAttack"))
        {
            moveDir.x = Input.GetAxisRaw("Horizontal");                                     // 좌우이동
            if (Input.GetKeyDown(KeyCode.W) && !myAnim.GetBool("IsAir"))          // 윗점프
            {
                OnJump();
            }

            if (Input.GetKeyDown(KeyCode.S) && !myAnim.GetBool("IsAir"))          // 아랫점프
            {
                OnDownJump();
            }
        }
        else
        {
            moveDir.x = 0;
        }
        if (!myAnim.GetBool("IsParry"))
        {
            if (Input.GetMouseButtonDown(0) && !myAnim.GetBool("IsAir"))          // 공격                                        // 공격키
            {
                OnPlayerAttack();
            }
        }

        if (Input.GetMouseButtonDown(1) && !myAnim.GetBool("IsAir"))           // 패링                                   // 패링키
        {
            OnParry();
        }


        if (Input.GetKeyDown(KeyCode.Space))                                   // 회피(구르기&대쉬)
        {
            if (curSpaceCool >= spaceCoolDown)
            {
                OnDodge();
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Interactable?.Interact(this);
        }

    
        base.OnUpdate();
    }

    public void OnAttack() // 공격범위 설정
    {
        Vector2 dir = new Vector2(myRenderer.flipX ? -1.0f : 1.0f, 0.0f);
        Collider2D[] list = Physics2D.OverlapCircleAll((Vector2)transform.position + dir, 1.0f, myEnemy);
        foreach (Collider2D col in list)
        {
            col.GetComponent<IDamage>()?.OnDamage(characterStat.totalAtk);
        }
    }

}

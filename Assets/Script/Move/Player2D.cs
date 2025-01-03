using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Player2D : BattleSystem2D
{
    //다이얼로그UI 사용할 수 있게
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private Potal potal;
    public DialogueUI DialogueUI => dialogueUI;
    private Potal Potal => potal;
    public IInteractable Interactable { get; set; }

    public LayerMask myEnemy;

    public CharacterStatus player;

    private PlayerDead playerdead;

    // Start is called before the first frame update
    void Start()
    {
        player = this;
        UpdateStatus();
        playerdead = GetComponent<PlayerDead>();
    }

    // Update is called once per frame
    void Update()
    {

        if (DialogueUI.IsOpen == false) //대화중, 포탈중이 아닐때만 움직임 가능
        {
            if (!myAnim.GetBool("IsParry") && !myAnim.GetBool("IsAttack"))
            {
                moveDir.x = Input.GetAxisRaw("Horizontal");                                     // 좌우이동
                if (Input.GetKeyDown(KeyCode.W) && !myAnim.GetBool("IsAir"))          // 윗점프
                {
                    OnJump();                 
                }
                if (Input.GetKeyDown(KeyCode.W) && myAnim.GetBool("IsAir"))          // 윗점프
                {
                    OnDoubleJump();
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

            if (!myAnim.GetBool("IsParry") && !myAnim.GetBool("IsAttack"))
            {
                if (Input.GetMouseButtonDown(0) && !myAnim.GetBool("IsAir"))          // 공격                                        // 공격키
                {
                    OnPlayerAttack();
                }
            }

            if (Input.GetMouseButtonDown(1) && !myAnim.GetBool("IsAir") && !myAnim.GetBool("IsAttack"))           // 패링                                   // 패링키
            {
                if (characterStat.curParryingCool >= characterStat.parryingCool)
                {
                    OnParry();
                }
            }


            if (Input.GetKeyDown(KeyCode.Space))                                   // 회피(구르기&대쉬)
            {
                if (characterStat.curDodgeCool >= characterStat.dodgeCool)
                {
                    OnDodge();
                }
            }


            if (Input.GetKeyDown(KeyCode.G))
            {
            
                FileManager.SaveToJson<Stat>(Application.dataPath + "/Data/Save/tempSave.dat", player.characterStat);
                Interactable?.Interact(this);
            }


            base.OnUpdate();


            
        }
    }
    
    

    public void OnAttack() // 공격범위 설정
    {
        float addHpAtk = 0;
        if (player.characterStat.hpPoint >=30)
        {
            addHpAtk = characterStat.maxHP * 0.25f;
        }
        Vector2 dir = new Vector2(myRenderer.flipX ? -1.0f : 1.0f, 0.0f);
        Collider2D[] list = Physics2D.OverlapCircleAll((Vector2)transform.position + dir, 1.0f, myEnemy);
        foreach (Collider2D col in list)
        {
            col.GetComponent<IDamage>()?.OnDamage(characterStat.totalAtk + addHpAtk);
        }
    }

    public void OnDieTrap(float percent)
    {
        float damage = characterStat.maxHP * percent;  // 최대 체력을 기준으로 대미지 계산
        characterStat.curHP -= damage;

        if (characterStat.curHP < 0)
            characterStat.curHP = 0;  // 체력은 0 이하로 내려가지 않음
        myAnim.SetTrigger("OnDead");
        OnDead();
        DisablePlayerControls();
        myRigid.velocity = Vector2.zero;
        
    }

    public void OnTrapDamage(float dmg)
    {
        characterStat.curHP -= dmg;

        if (characterStat.curHP > 0)
        {
            myAnim.SetTrigger("OnDamage");
        }
        else
        {
            characterStat.curHP = 0;
            myAnim.SetTrigger("OnDead");
            OnDead();
            DisablePlayerControls();
            
        }
    }

    // 플레이어 컨트롤 비활성화 메서드
    private void DisablePlayerControls()
    {
        // Player2D 컨트롤 스크립트 비활성화
        Player2D playerScript = gameObject.GetComponent<Player2D>();
        Picking pickingScript = gameObject.GetComponent<Picking>();
        if (playerScript != null)
        {
            playerScript.enabled = false; // Player2D 스크립트 비활성화
        }
        if (pickingScript != null)
        {
            pickingScript.enabled = false;
        }
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        myRigid.gravityScale = 0.0f;
    }
}

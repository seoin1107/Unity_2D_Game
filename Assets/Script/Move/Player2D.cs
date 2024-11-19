using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Player2D : BattleSystem2D
{
/*    //���̾�α�UI ����� �� �ְ�
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
/*        if(DialogueUI.IsOpen == false) //��ȭ���� �ƴҶ��� ������ ����
        {

        }*/
        moveDir.x = Input.GetAxisRaw("Horizontal");                                     // �¿��̵�

        if (Input.GetKeyDown(KeyCode.W) && !myAnim.GetBool("IsAir"))          // ������
        {
            OnJump();
        }

        if (Input.GetKeyDown(KeyCode.S) && !myAnim.GetBool("IsAir"))          // �Ʒ�����
        {
            OnDownJump();
        }
                    
        if(Input.GetMouseButtonDown(1) && !myAnim.GetBool("IsAir"))           // �и�                                   // �и�Ű
        {
            OnParry();
        }

        if (Input.GetMouseButtonDown(0) && !myAnim.GetBool("IsAir"))          // ����                                        // ����Ű
        {
            myAnim.SetTrigger(animData.OnAttack);
        }

        if(Input.GetKeyDown(KeyCode.Space))                                   // ȸ��(������&�뽬)
        {
            if (curSpaceCool >= spaceCoolDown)
            {
                OnDodge();
            }
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

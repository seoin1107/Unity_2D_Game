using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Player2D : BattleSystem2D
{
    //���̾�α�UI ����� �� �ְ�
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private Potal potal;
    public DialogueUI DialogueUI => dialogueUI;
    private Potal Potal => potal;
    public IInteractable Interactable { get; set; }

    public LayerMask myEnemy;

    public CharacterStatus playerStatus;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = this;
        UpdateStatus();
    }

    // Update is called once per frame
    void Update()
    {

        //if (DialogueUI.IsOpen == false) //��ȭ��, ��Ż���� �ƴҶ��� ������ ����
        //{
            if (!myAnim.GetBool("IsParry") && !myAnim.GetBool("IsAttack"))
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
            }
            else
            {
                moveDir.x = 0;
            }

            if (!myAnim.GetBool("IsParry") && !myAnim.GetBool("IsAttack"))
            {
                if (Input.GetMouseButtonDown(0) && !myAnim.GetBool("IsAir"))          // ����                                        // ����Ű
                {
                    OnPlayerAttack();
                }
            }

            if (Input.GetMouseButtonDown(1) && !myAnim.GetBool("IsAir"))           // �и�                                   // �и�Ű
            {
                OnParry();
            }


            if (Input.GetKeyDown(KeyCode.Space))                                   // ȸ��(������&�뽬)
            {
                if (curSpaceCool >= spaceCoolDown)
                {
                    OnDodge();
                }
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
            
                FileManager.SaveToJson<Stat>(Application.dataPath + "/Data/Save/tempSave.dat", playerStatus.characterStat);
                Interactable?.Interact(this);
            }

            base.OnUpdate();


            
        }
    

    public void OnAttack() // ���ݹ��� ����
    {

        Vector2 dir = new Vector2(myRenderer.flipX ? -1.0f : 1.0f, 0.0f);
        Collider2D[] list = Physics2D.OverlapCircleAll((Vector2)transform.position + dir, 1.0f, myEnemy);
        foreach (Collider2D col in list)
        {
            col.GetComponent<IDamage>()?.OnDamage(characterStat.totalAtk);
        }
    }

}

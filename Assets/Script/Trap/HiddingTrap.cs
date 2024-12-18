using UnityEngine;

public class HiddingTrap : MonoBehaviour
{
    private Animator animator;
    private bool isTrapActive = false; // ���ð� �̹� Ȱ��ȭ�Ǿ����� Ȯ��

    public float damage = 10.0f; // ���ÿ� ������ �Դ� �����
    public float resetTime = 1.5f; // ���ð� ������ �� ��Ȱ��ȭ �ð�

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Animator ������Ʈ ��������
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTrapActive && collision.gameObject.layer == LayerMask.NameToLayer("Player")) // �÷��̾�� �浹 ��
        {
            isTrapActive = true;
            animator.SetTrigger("OnRise"); // ���� �ھƿ����� �ִϸ��̼� ����
            //animator.SetBool("IsActive", true);

            // �÷��̾ ����� �ֱ�
            Player2D player = collision.GetComponent<Player2D>();
            if (player != null)
            {
                player.OnTrapDamage(damage);
            }

            // ���ð� �ٽ� ���������� Ÿ�̸� ����
            Invoke("ResetTrap", resetTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void ResetTrap()
    {
        animator.SetBool("IsActive", false); // �ִϸ��̼� ���¸� Idle�� ����
        isTrapActive = false; // ���� ���� �ʱ�ȭ
    }
}

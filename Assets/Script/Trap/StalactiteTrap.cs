using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ground�� �浹 �� �ı�
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            DestroyTrap();
        }

        // Player�� �浹 �� ����� �ְ� �ı�
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // �÷��̾�� ����� �ֱ�
            Player2D player = collision.gameObject.GetComponent<Player2D>();
            if (player != null)
            {
                player.OnTrapDamage(10.0f);  // ����� ����
            }

            DestroyTrap();
        }
    }

    private void DestroyTrap()
    {
        // ������Ʈ Ǯ�� ��ȯ�Ϸ��� SetActive(false)�� ��Ȱ��ȭ
        gameObject.SetActive(false);
        
    }
}

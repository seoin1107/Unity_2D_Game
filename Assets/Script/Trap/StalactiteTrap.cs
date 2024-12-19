using UnityEngine;

public class StalactiteTrap : MonoBehaviour
{
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1.0f; // �߷� ����

        // ������ �ڽ� ������Ʈ�� ��Ȱ��ȭ�Ǿ��� ���, Ȱ��ȭ
        Transform stalactite = transform.Find("Stalactite"); // �ڽ� ������Ʈ �̸��� "Stalactite"�� ���
        if (stalactite != null && !stalactite.gameObject.activeSelf)
        {
            stalactite.gameObject.SetActive(true); // �ڽ� ������ Ȱ��ȭ
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // �÷��̾� ����� ó��
            Player2D player = other.GetComponent<Player2D>();
            if (player != null)
            {
                player.OnTrapDamage(10.0f);
            }
            DestroyTrap();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // �ٴڿ� ������ �ı�
            DestroyTrap();
        }
    }

    private void DestroyTrap()
    {
        // ������ �ı� �� �ٽ� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}

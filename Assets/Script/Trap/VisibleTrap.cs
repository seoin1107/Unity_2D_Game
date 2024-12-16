using UnityEngine;

public class VisibleTrap : MonoBehaviour
{
    public float damage = 10.0f;  // ���ð� �� �����
    public float damageInterval = 1.0f;  // ������� �ִ� ���� (1��)    
    private float timeSinceLastDamage = 0.0f;  // ������ ��������� ����� �ð�
    private GameObject Player = null;  // ���� ���ÿ� �ö�� �ִ� �÷��̾� ��ü

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var damageable = collision.GetComponent<IDamage>();
            if (damageable != null)
            {
                damageable.OnDamage(damage);  // ó�� ���ÿ� ���� �� ����� �ֱ�
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // �÷��̾� ��ü�� ����
            Player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // �÷��̾ ���ÿ��� ����� ���� �ʱ�ȭ
            Player = null;
            timeSinceLastDamage = 0.0f;
        }
    }

    private void Update()
    {
        if (Player != null)
        {
            // �ð��� �����ǰ�, 1�� �������� ����� �ֱ�
            timeSinceLastDamage += Time.deltaTime;

            if (timeSinceLastDamage >= damageInterval)
            {
                // ����� �ֱ�
                var damageable = Player.GetComponent<IDamage>();
                if (damageable != null)
                {
                    damageable.OnDamage(damage);  // ����� �ֱ�
                }

                // Ÿ�̸� �ʱ�ȭ
                timeSinceLastDamage = 0.0f;
            }
        }
    }
}

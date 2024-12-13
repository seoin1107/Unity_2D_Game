using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public GameObject player;
    public float damage = 10f;  // ���ð� �� �����
    public float damageInterval = 1f;  // ������� �ִ� ���� (1��)    
    public float timeSinceLastDamage = 0f;  // ������ ��������� ����� �ð�
    private bool isPlayerOnTrap = false;  // �÷��̾ ���ÿ� �ִ��� ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // IDamage �������̽��� ������ ��ü�� ã��
            var damageable = collision.GetComponent<IDamage>();
            if (damageable != null)
            {
                damageable.OnDamage(damage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // �÷��̾ ���ÿ� ������ isPlayerOnTrap�� true�� ����
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                isPlayerOnTrap = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // �÷��̾ ������ ������ ����� Ÿ�̸� �ʱ�ȭ
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            timeSinceLastDamage = 0f;
        }
    }

    private void Update()
    {
        // �÷��̾ ���ÿ� ������ �ð� ����
        if (isPlayerOnTrap)
        {
            timeSinceLastDamage += Time.deltaTime;  // �ð��� ������

            if (timeSinceLastDamage >= damageInterval)
            {
                // ����� �ֱ�
                var damageable = GetComponent<IDamage>();
                if (damageable != null)
                {
                    damageable.OnDamage(damage);  // ����� �ֱ�
                }

                // Ÿ�̸� �ʱ�ȭ
                timeSinceLastDamage = 0f;
            }
        }
    }
}

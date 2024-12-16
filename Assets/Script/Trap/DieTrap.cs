using UnityEngine;

public class DieTrap : MonoBehaviour
{
    public float damagePercent = 1.0f;  // 100% ����� (���)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var damageable = collision.GetComponent<Player2D>();
            if (damageable != null)
            {
                damageable.OnDieDamage(damagePercent);  // ���� �����
            }
        }
    }
}

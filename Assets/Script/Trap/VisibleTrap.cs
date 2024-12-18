using System.Collections;
using UnityEngine;

public class VisibleTrap : MonoBehaviour
{
    public float trapDamage = 10.0f; // ���� �����
    public ushort IsDieTrap = 0; // ��� ����� ����
    private bool isPlayerOnTrap = false; // �÷��̾ ���� ���� �ִ��� ����
    private Coroutine damageCoroutine; // �ڷ�ƾ ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player2D>();
        if (player != null && !isPlayerOnTrap)
        {
            isPlayerOnTrap = true;

            // ó�� ����� �� ����� ó�� (��� �Ǵ� ���� ����� ����)
            if (IsDieTrap == 1)
            {
                player.OnDieTrap(IsDieTrap); // ��� �����
            }
            else
            {
                player.OnTrapDamage(trapDamage); // ó�� ����� ����
                damageCoroutine = StartCoroutine(ApplyContinuousDamage(player)); // ���� ����� ����
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player2D>();
        if (player != null && isPlayerOnTrap)
        {
            isPlayerOnTrap = false;

            // ���� ����� �ߴ�
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator ApplyContinuousDamage(Player2D player)
    {
        while (isPlayerOnTrap)
        {
            yield return new WaitForSeconds(1.0f); // 1�� �������� ����� ����
            player.OnTrapDamage(trapDamage); // ���� �����
        }
    }
}

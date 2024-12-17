using System.Collections;
using UnityEngine;

public class VisibleTrap : MonoBehaviour
{
    public float trapDamage = 10.0f; // 지속 대미지
    public float instantDeadPercent = 100.0f; // 즉사 대미지 비율
    private bool isPlayerOnTrap = false; // 플레이어가 함정 위에 있는지 여부
    private Coroutine damageCoroutine; // 코루틴 참조

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player2D>();
        if (player != null && !isPlayerOnTrap)
        {
            isPlayerOnTrap = true;

            // 처음 밟았을 때 대미지 처리 (즉사 또는 지속 대미지 시작)
            if (instantDeadPercent >= 100.0f)
            {
                player.OnDieTrap(instantDeadPercent); // 즉사 대미지
            }
            else
            {
                player.OnTrapDamage(trapDamage); // 처음 대미지 적용
                damageCoroutine = StartCoroutine(ApplyContinuousDamage(player)); // 지속 대미지 시작
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player2D>();
        if (player != null && isPlayerOnTrap)
        {
            isPlayerOnTrap = false;

            // 지속 대미지 중단
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
            yield return new WaitForSeconds(1.0f); // 1초 간격으로 대미지 적용
            player.OnTrapDamage(trapDamage); // 지속 대미지
        }
    }
}

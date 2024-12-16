using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    public CharacterStatus characterStatus; // CharacterStatus를 참조

    public void ApplyDamage(float dmg)
    {
        // Stat을 가져오기
        Stat stat = characterStatus.GetStat();

        // 체력에 대미지를 반영
        stat.curHP -= dmg;

        // 체력이 0 이하로 떨어지면 사망 처리
        if (stat.curHP <= 0)
        {
            stat.curHP = 0;
            OnDeath();
        }

        // 상태 업데이트
        characterStatus.characterStat = stat;  // 변경된 Stat을 다시 적용
    }

    private void OnDeath()
    {
        // 사망 애니메이션 처리
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("OnDead");

        // 사망 처리 로직
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        // 추가적인 사망 처리 로직
    }
}

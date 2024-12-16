using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    public CharacterStatus characterStatus; // CharacterStatus�� ����

    public void ApplyDamage(float dmg)
    {
        // Stat�� ��������
        Stat stat = characterStatus.GetStat();

        // ü�¿� ������� �ݿ�
        stat.curHP -= dmg;

        // ü���� 0 ���Ϸ� �������� ��� ó��
        if (stat.curHP <= 0)
        {
            stat.curHP = 0;
            OnDeath();
        }

        // ���� ������Ʈ
        characterStatus.characterStat = stat;  // ����� Stat�� �ٽ� ����
    }

    private void OnDeath()
    {
        // ��� �ִϸ��̼� ó��
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("OnDead");

        // ��� ó�� ����
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        // �߰����� ��� ó�� ����
    }
}

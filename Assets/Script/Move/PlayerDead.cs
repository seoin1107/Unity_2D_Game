// PlayerDead.cs
using UnityEngine;
using UnityEngine.Events;

public class PlayerDead : MonoBehaviour, IDeathAlarm
{
    private Animator myAnim;
    public UnityAction deathAlarm { get; set; }

    void Start()
    {
        myAnim = GetComponent<Animator>();  // �ִϸ����� ������Ʈ ��������
        BattleSystem2D battleSystem = FindObjectOfType<BattleSystem2D>();  // BattleSystem2D ã��
        battleSystem.deathAlarm += OnDeath;  // ������ �������� ��� �˸� ����
    }

    // IDeathAlarm �������̽� ����
    public void OnDeath()
    {
        // ���� ��� ǥ�� �� �ִϸ��̼� ����
        Debug.Log("Player is dead, triggering death screen...");
        ShowDeathScreen();
    }

    private void ShowDeathScreen()
    {
        // ���� ��� ǥ�� �� �ִϸ��̼� ���� �� ��� ó��
        Debug.Log("Showing death screen...");
    }

    void OnDestroy()
    {
        // ��� �˸� ���� ����
        BattleSystem2D battleSystem = FindObjectOfType<BattleSystem2D>();
        if (battleSystem != null)
        {
            battleSystem.deathAlarm -= OnDeath;
        }
    }
}

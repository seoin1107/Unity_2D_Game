// PlayerDead.cs
using UnityEngine;
using UnityEngine.Events;

public class PlayerDead : MonoBehaviour, IDeathAlarm
{
    private Animator myAnim;
    public UnityAction deathAlarm { get; set; }

    void Start()
    {
        myAnim = GetComponent<Animator>();  // 애니메이터 컴포넌트 가져오기
        BattleSystem2D battleSystem = FindObjectOfType<BattleSystem2D>();  // BattleSystem2D 찾기
        battleSystem.deathAlarm += OnDeath;  // 옵저버 패턴으로 사망 알림 구독
    }

    // IDeathAlarm 인터페이스 구현
    public void OnDeath()
    {
        // 검은 배경 표시 및 애니메이션 실행
        Debug.Log("Player is dead, triggering death screen...");
        ShowDeathScreen();
    }

    private void ShowDeathScreen()
    {
        // 검은 배경 표시 및 애니메이션 실행 등 사망 처리
        Debug.Log("Showing death screen...");
    }

    void OnDestroy()
    {
        // 사망 알림 구독 해제
        BattleSystem2D battleSystem = FindObjectOfType<BattleSystem2D>();
        if (battleSystem != null)
        {
            battleSystem.deathAlarm -= OnDeath;
        }
    }
}

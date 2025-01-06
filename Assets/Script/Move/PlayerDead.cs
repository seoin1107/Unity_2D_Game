using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class PlayerDead : MonoBehaviour
{
    public CharacterStatus characterStatus; // CharacterStatus 참조
    public GameObject blackBackground;     // 검은 배경 이미지
    public GameObject tryAgainText;              // "Try Again?" 텍스트 UI
    public GameObject pressAnyKeyText;           // "Press Any Key" 텍스트 UI
    private bool isDead = false;           // 사망 처리 여부 플래그

    void Start()
    {
        if (characterStatus == null)
        {
            // 같은 오브젝트에서 CharacterStatus 컴포넌트를 가져옵니다.
            characterStatus = GetComponent<CharacterStatus>();
        }

        // 텍스트 초기화 (비활성화 상태)
        if (tryAgainText != null)
        {
            tryAgainText.gameObject.SetActive(false);
        }
        if (pressAnyKeyText != null)
        {
            pressAnyKeyText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // 사망 처리를 한 번만 실행
        if (!isDead && characterStatus != null && characterStatus.characterStat.curHP <= 0)
        {
            isDead = true; // 사망 처리 시작
            HandleDeath();
        }
    }

    void HandleDeath()
    {
        // 플레이어 레이어일 경우
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("플레이어 사망 처리");

            // 검은 배경 활성화
            if (blackBackground != null)
            {
                blackBackground.SetActive(true);
            }

            // 다시 시작 메시지 및 입력 대기
            StartCoroutine(WaitForRetry());
        }
    }

    IEnumerator WaitForRetry()
    {
        yield return new WaitForSeconds(1f); // 1초 대기
        // "Try Again?" 활성화
        if (tryAgainText != null)
        {
            tryAgainText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(1f); // "Try Again?" 후 1초 대기

        // "Press Any Key" 활성화
        if (pressAnyKeyText != null)
        {
            pressAnyKeyText.gameObject.SetActive(true);
        }

        Debug.Log("다시 시작하려면 아무 키나 누르세요.");

        // 아무 키나 누를 때까지 대기
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        // 로딩 씬 호출 및 마을 이동
        Loading.nScene = 2;
        SceneManager.LoadScene(1);
    }
}

using UnityEngine;

public class StalactiteManager : MonoBehaviour
{
    private bool isTriggered = false; // 트리거가 한번만 작동하도록 제어할 변수

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTriggered) return; // 이미 트리거가 작동했으면 더 이상 처리하지 않음

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // 자식인 종유석을 활성화
            Transform stalactite = transform.Find("Stalactite");
            if (stalactite != null && !stalactite.gameObject.activeSelf)
            {
                stalactite.gameObject.SetActive(true); // 자식 종유석 활성화
            }

            // 트리거 비활성화 (한 번만 작동하도록)
            isTriggered = true; // 트리거 상태를 true로 설정하여 한 번만 작동하도록 함
        }
    }
}

using UnityEngine;

public class StalactiteManager : MonoBehaviour
{
    bool IsActive = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && IsActive == false)
        {
            // 트리거에 닿으면 자식인 종유석을 활성화
            Transform stalactite = transform.Find("Stalactite"); // 자식 오브젝트 이름이 "Stalactite"
            if (stalactite != null && !stalactite.gameObject.activeSelf)
            {
                stalactite.gameObject.SetActive(true); // 자식 종유석 활성화
                IsActive = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTileTrigger : MonoBehaviour
{
    public GameObject targetObject;
    public float moveSpeed = 1f; // 위로 이동하는 속도
    public float destroyDelay = 5f; // 삭제되기까지의 시간

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(MoveUp(targetObject)); // 코루틴 시작
        }
    }

    private IEnumerator MoveUp(GameObject obj)
    {
        float elapsedTime = 0f; // 경과 시간 추적

        while (elapsedTime < destroyDelay)
        {
            // 오브젝트를 위로 이동
            obj.transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime; // 시간 누적
            yield return null; // 다음 프레임까지 대기
        }
    }
}

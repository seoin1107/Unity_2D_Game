using UnityEngine;

public class StalactiteManager : MonoBehaviour
{
    public GameObject trapPrefab;  // 함정(종유석) 프리팹
    public Transform triggerArea;  // 트리거 영역
    private StalactiteObjectPool objectPool;  // 오브젝트 풀
    public float spawnHeight = 10f;  // 종유석 생성 높이

    private void Start()
    {
        objectPool = FindObjectOfType<StalactiteObjectPool>();  // 오브젝트 풀 찾기
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // 트리거를 밟은 경우, 종유석 생성 및 떨어뜨리기
            SpawnStalactite();
        }
    }

    private void SpawnStalactite()
    {
        // 오브젝트 풀에서 종유석 꺼내기
        GameObject trap = objectPool.GetObjectFromPool();
        if (trap != null)
        {
            // 종유석 초기화
            trap.transform.position = new Vector3(triggerArea.position.x, spawnHeight, triggerArea.position.z);

            // 리지드바디 추가하여 떨어지게 설정
            Rigidbody2D rb = trap.GetComponent<Rigidbody2D>();
            rb.isKinematic = false;  // 중력 작용하게 설정
            rb.velocity = Vector2.zero;  // 이전 속도 초기화
            rb.AddForce(Vector2.down * 5f, ForceMode2D.Impulse);  // 수직으로 떨어지게 설정
        }
    }
}

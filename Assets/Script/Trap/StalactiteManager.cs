using UnityEngine;

public class StalactiteManager : MonoBehaviour
{
    public GameObject trapPrefab;  // ����(������) ������
    public Transform triggerArea;  // Ʈ���� ����
    private StalactiteObjectPool objectPool;  // ������Ʈ Ǯ
    public float spawnHeight = 10f;  // ������ ���� ����

    private void Start()
    {
        objectPool = FindObjectOfType<StalactiteObjectPool>();  // ������Ʈ Ǯ ã��
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Ʈ���Ÿ� ���� ���, ������ ���� �� ����߸���
            SpawnStalactite();
        }
    }

    private void SpawnStalactite()
    {
        // ������Ʈ Ǯ���� ������ ������
        GameObject trap = objectPool.GetObjectFromPool();
        if (trap != null)
        {
            // ������ �ʱ�ȭ
            trap.transform.position = new Vector3(triggerArea.position.x, spawnHeight, triggerArea.position.z);

            // ������ٵ� �߰��Ͽ� �������� ����
            Rigidbody2D rb = trap.GetComponent<Rigidbody2D>();
            rb.isKinematic = false;  // �߷� �ۿ��ϰ� ����
            rb.velocity = Vector2.zero;  // ���� �ӵ� �ʱ�ȭ
            rb.AddForce(Vector2.down * 5f, ForceMode2D.Impulse);  // �������� �������� ����
        }
    }
}

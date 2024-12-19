using UnityEngine;
using System.Collections.Generic;

public class StalactiteObjectPool : MonoBehaviour
{
    public GameObject stalactitePrefab;   // ������ ������
    public int initialPoolSize = 10;      // �ʱ� Ǯ ũ��
    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        // �ʱ� Ǯ ũ�⸸ŭ �̸� �����Ͽ� Ǯ�� ����
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject stalactite = Instantiate(stalactitePrefab);
            stalactite.SetActive(false);  // ��Ȱ��ȭ�� ���·� Ǯ�� ����
            pool.Enqueue(stalactite);
        }
    }

    public GameObject GetStalactite()
    {
        if (pool.Count > 0)
        {
            return pool.Dequeue();  // Ǯ���� �ϳ� ����
        }
        else
        {
            // Ǯ�� ���� ���� ���ٸ� ���ο� ��ü ����
            GameObject stalactite = Instantiate(stalactitePrefab);
            return stalactite;
        }
    }

    public void ReturnObject(GameObject stalactite)
    {
        stalactite.SetActive(false);  // ������Ʈ�� ��Ȱ��ȭ�Ͽ� Ǯ�� ��ȯ
        pool.Enqueue(stalactite);
    }
}

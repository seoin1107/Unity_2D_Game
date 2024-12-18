using System.Collections.Generic;
using UnityEngine;

public class StalactiteObjectPool : MonoBehaviour
{
    public GameObject trapPrefab;  // ������ ������
    private Queue<GameObject> pool = new Queue<GameObject>();
    public int poolSize = 10;  // Ǯ ũ��

    private void Start()
    {
        // ������Ʈ Ǯ �ʱ�ȭ
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(trapPrefab);
            obj.SetActive(false);  // ó������ ��Ȱ��ȭ
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);  // Ȱ��ȭ
            return obj;
        }
        else
        {
            // Ǯ�� ��ü�� ������ ���� ����
            GameObject obj = Instantiate(trapPrefab);
            obj.SetActive(true);
            return obj;
        }
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);  // ��Ȱ��ȭ�Ͽ� Ǯ�� ��ȯ
        pool.Enqueue(obj);
    }
}

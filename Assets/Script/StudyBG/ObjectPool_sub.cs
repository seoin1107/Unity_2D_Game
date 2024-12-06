using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_sub : MonoBehaviour
{
    public GameObject prefab; // ��� ������
    public int poolSize = 3; // ������Ʈ Ǯ�� �ʱ� ũ��

    private Queue<GameObject> poolQueue = new Queue<GameObject>();

    void Start()
    {
        // Ǯ �ʱ�ȭ
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false); // ó������ ��Ȱ��ȭ
            poolQueue.Enqueue(obj); // ť�� �ֱ�
        }
    }

    // ������Ʈ�� Ǯ���� ������
    public GameObject GetObject()
    {
        if (poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
            obj.SetActive(true); // ������ ������Ʈ Ȱ��ȭ
            return obj;
        }
        else
        {
            // Ǯ�� ������Ʈ�� ������ ���ο� ������Ʈ ����
            GameObject obj = Instantiate(prefab);
            obj.SetActive(true);
            return obj;
        }
    }

    // ����� ���� ������Ʈ�� Ǯ�� ��ȯ
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // ��Ȱ��ȭ�Ͽ� Ǯ�� ��ȯ
        poolQueue.Enqueue(obj);
    }
}

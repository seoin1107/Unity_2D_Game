using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab; // ������ ������
    public int poolSize = 5; // �ʱ� ������ ������Ʈ ����

    private Queue<GameObject> poolQueue = new Queue<GameObject>();

    void Start()
    {
        // �ʱ�ȭ: Ǯ�� ������Ʈ ����
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        // Ǯ���� ��Ȱ��ȭ�� ������Ʈ ��������
        if (poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        // Ǯ�� �� �̻� ������Ʈ�� ������ ���� ����
        GameObject newObj = Instantiate(prefab);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        // ������Ʈ�� �ٽ� Ǯ�� ��ȯ
        obj.SetActive(false);
        poolQueue.Enqueue(obj);
    }
}

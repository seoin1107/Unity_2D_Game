using System.Collections.Generic;
using UnityEngine;

public class StalactiteObjectPool : MonoBehaviour
{
    public GameObject trapPrefab;  // 종유석 프리팹
    private Queue<GameObject> pool = new Queue<GameObject>();
    public int poolSize = 10;  // 풀 크기

    private void Start()
    {
        // 오브젝트 풀 초기화
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(trapPrefab);
            obj.SetActive(false);  // 처음에는 비활성화
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);  // 활성화
            return obj;
        }
        else
        {
            // 풀에 객체가 없으면 새로 생성
            GameObject obj = Instantiate(trapPrefab);
            obj.SetActive(true);
            return obj;
        }
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);  // 비활성화하여 풀에 반환
        pool.Enqueue(obj);
    }
}

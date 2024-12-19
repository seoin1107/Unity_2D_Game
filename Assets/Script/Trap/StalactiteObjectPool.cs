using UnityEngine;
using System.Collections.Generic;

public class StalactiteObjectPool : MonoBehaviour
{
    public GameObject stalactitePrefab;   // 종유석 프리팹
    public int initialPoolSize = 10;      // 초기 풀 크기
    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        // 초기 풀 크기만큼 미리 생성하여 풀에 저장
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject stalactite = Instantiate(stalactitePrefab);
            stalactite.SetActive(false);  // 비활성화된 상태로 풀에 넣음
            pool.Enqueue(stalactite);
        }
    }

    public GameObject GetStalactite()
    {
        if (pool.Count > 0)
        {
            return pool.Dequeue();  // 풀에서 하나 꺼냄
        }
        else
        {
            // 풀에 남은 것이 없다면 새로운 객체 생성
            GameObject stalactite = Instantiate(stalactitePrefab);
            return stalactite;
        }
    }

    public void ReturnObject(GameObject stalactite)
    {
        stalactite.SetActive(false);  // 오브젝트를 비활성화하여 풀에 반환
        pool.Enqueue(stalactite);
    }
}

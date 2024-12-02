using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_sub : MonoBehaviour
{
    public GameObject prefab; // 배경 프리팹
    public int poolSize = 3; // 오브젝트 풀의 초기 크기

    private Queue<GameObject> poolQueue = new Queue<GameObject>();

    void Start()
    {
        // 풀 초기화
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false); // 처음에는 비활성화
            poolQueue.Enqueue(obj); // 큐에 넣기
        }
    }

    // 오브젝트를 풀에서 가져옴
    public GameObject GetObject()
    {
        if (poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
            obj.SetActive(true); // 가져온 오브젝트 활성화
            return obj;
        }
        else
        {
            // 풀에 오브젝트가 없으면 새로운 오브젝트 생성
            GameObject obj = Instantiate(prefab);
            obj.SetActive(true);
            return obj;
        }
    }

    // 사용이 끝난 오브젝트를 풀로 반환
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // 비활성화하여 풀에 반환
        poolQueue.Enqueue(obj);
    }
}

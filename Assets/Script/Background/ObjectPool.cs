using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab; // 복제할 프리팹
    public int poolSize = 5; // 초기 생성할 오브젝트 개수

    private Queue<GameObject> poolQueue = new Queue<GameObject>();

    void Start()
    {
        // 초기화: 풀에 오브젝트 생성
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        // 풀에서 비활성화된 오브젝트 가져오기
        if (poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        // 풀에 더 이상 오브젝트가 없으면 새로 생성
        GameObject newObj = Instantiate(prefab);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        // 오브젝트를 다시 풀에 반환
        obj.SetActive(false);
        poolQueue.Enqueue(obj);
    }
}

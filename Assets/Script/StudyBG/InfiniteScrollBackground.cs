using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScrollBackground : MonoBehaviour
{
    public Transform cameraTransform;  // 카메라의 Transform
    public GameObject backgroundPrefab;  // 배경 프리팹
    public float backgroundWidth = 17.92f;  // 배경의 너비 (설정 필요)

    private LinkedList<BackgroundNode> backgroundList;

    void Start()
    {
        backgroundList = new LinkedList<BackgroundNode>();

        // 배경 생성
        for (int i = -1; i <= 1; i++)
        {
            GameObject bg = Instantiate(backgroundPrefab, new Vector3(i * backgroundWidth, 0, 0), Quaternion.identity);
            BackgroundNode node = new BackgroundNode(bg);

            if (backgroundList.Count > 0)
            {
                node.prev = backgroundList.Last.Value;
                backgroundList.Last.Value.next = node;
            }

            backgroundList.AddLast(node);
        }

        // 첫 번째와 마지막 노드 연결 (양방향 연결)
        backgroundList.First.Value.prev = backgroundList.Last.Value;
        backgroundList.Last.Value.next = backgroundList.First.Value;
    }

    void Update()
    {
        // 카메라의 x 위치 추적
        float cameraX = cameraTransform.position.x;

        // X축 반복 처리 (무한 스크롤)
        if (cameraX - backgroundList.First.Value.backgroundObject.transform.position.x > backgroundWidth)
        {
            BackgroundNode leftNode = backgroundList.First.Value;
            backgroundList.RemoveFirst();
            leftNode.backgroundObject.transform.position += new Vector3(backgroundWidth * backgroundList.Count, 0, 0);
            backgroundList.AddLast(leftNode);
        }
        else if (backgroundList.Last.Value.backgroundObject.transform.position.x - cameraX > backgroundWidth)
        {
            BackgroundNode rightNode = backgroundList.Last.Value;
            backgroundList.RemoveLast();
            rightNode.backgroundObject.transform.position -= new Vector3(backgroundWidth * backgroundList.Count, 0, 0);
            backgroundList.AddFirst(rightNode);
        }
    }
}

public class BackgroundNode
{
    public GameObject backgroundObject;
    public BackgroundNode next;
    public BackgroundNode prev;

    public BackgroundNode(GameObject bg)
    {
        backgroundObject = bg;
    }
}

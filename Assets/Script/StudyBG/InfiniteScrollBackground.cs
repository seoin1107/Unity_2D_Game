using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScrollBackground : MonoBehaviour
{
    public Transform cameraTransform;  // ī�޶��� Transform
    public GameObject backgroundPrefab;  // ��� ������
    public float backgroundWidth = 17.92f;  // ����� �ʺ� (���� �ʿ�)

    private LinkedList<BackgroundNode> backgroundList;

    void Start()
    {
        backgroundList = new LinkedList<BackgroundNode>();

        // ��� ����
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

        // ù ��°�� ������ ��� ���� (����� ����)
        backgroundList.First.Value.prev = backgroundList.Last.Value;
        backgroundList.Last.Value.next = backgroundList.First.Value;
    }

    void Update()
    {
        // ī�޶��� x ��ġ ����
        float cameraX = cameraTransform.position.x;

        // X�� �ݺ� ó�� (���� ��ũ��)
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

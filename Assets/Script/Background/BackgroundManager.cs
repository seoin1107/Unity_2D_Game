using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public ObjectPool objectPool; // Object Pool ����
    public Transform player; // �÷��̾� Transform
    public float backgroundWidth = 10f; // ��� ������Ʈ�� ��
    public int activeBackgroundCount = 3; // ȭ�鿡 ǥ���� ��� ����

    private LinkedList<GameObject> activeBackgrounds = new LinkedList<GameObject>(); // Ȱ�� ��� ����Ʈ
    private float leftEdge; // ���� ���� ����� X ��ġ
    private float rightEdge; // ���� ������ ����� X ��ġ

    void Start()
    {
        // �ʱ� ��� ����
        for (int i = 0; i < activeBackgroundCount; i++)
        {
            SpawnBackgroundAt(i * backgroundWidth);
        }

        leftEdge = activeBackgrounds.First.Value.transform.position.x;
        rightEdge = activeBackgrounds.Last.Value.transform.position.x;
    }

    void Update()
    {
        float playerX = player.position.x;

        // ���� ��� �߰�
        if (playerX < leftEdge + backgroundWidth * 1.5f)
        {
            SpawnBackgroundAt(leftEdge - backgroundWidth);
            RemoveBackgroundFromRight();
        }

        // ������ ��� �߰�
        if (playerX > rightEdge - backgroundWidth * 1.5f)
        {
            SpawnBackgroundAt(rightEdge + backgroundWidth);
            RemoveBackgroundFromLeft();
        }
    }

    void SpawnBackgroundAt(float xPosition)
    {
        GameObject background = objectPool.GetObject();
        background.transform.position = new Vector3(xPosition, 0, 0);

        // ��� ����Ʈ�� �߰�
        if (xPosition < leftEdge)
        {
            activeBackgrounds.AddFirst(background);
            leftEdge = xPosition;
        }
        else
        {
            activeBackgrounds.AddLast(background);
            rightEdge = xPosition;
        }
    }

    void RemoveBackgroundFromLeft()
    {
        // ���ʿ��� ȭ���� ��� ��� ����
        GameObject leftBackground = activeBackgrounds.First.Value;

        if (leftBackground.transform.position.x < player.position.x - backgroundWidth * 2f)
        {
            activeBackgrounds.RemoveFirst();
            objectPool.ReturnObject(leftBackground);
            leftEdge = activeBackgrounds.First.Value.transform.position.x;
        }
    }

    void RemoveBackgroundFromRight()
    {
        // �����ʿ��� ȭ���� ��� ��� ����
        GameObject rightBackground = activeBackgrounds.Last.Value;

        if (rightBackground.transform.position.x > player.position.x + backgroundWidth * 2f)
        {
            activeBackgrounds.RemoveLast();
            objectPool.ReturnObject(rightBackground);
            rightEdge = activeBackgrounds.Last.Value.transform.position.x;
        }
    }
}

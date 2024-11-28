using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public ObjectPool objectPool; // Object Pool ����
    public Transform player; // �÷��̾� Transform
    public float backgroundWidth = 20.48f; // ��� ������Ʈ�� ��
    public int activeBackgroundCount = 3; // ȭ�鿡 ǥ���� ��� ����
    public float yParallaxFactor = 0.1f; // Y�� �̵� ���� ���� (0�� �������� Y�� �������� ����)

    private LinkedList<GameObject> activeBackgrounds = new LinkedList<GameObject>(); // Ȱ�� ��� ����Ʈ
    private float leftEdge; // ���� ���� ����� X ��ġ
    private float rightEdge; // ���� ������ ����� X ��ġ
    private float baseYPosition; // ��� Y�� ���� ��ġ

    void Start()
    {
        baseYPosition = player.position.y; // �ʱ� ��� ���� Y ��ġ

        // �ʱ� ��� ����
        for (int i = 0; i < activeBackgroundCount; i++)
        {
            float xPosition = i * backgroundWidth; // �ʱ� X ��ġ
            SpawnBackgroundAt(xPosition, baseYPosition);
        }

        // ��� �� �ʱ�ȭ
        leftEdge = activeBackgrounds.First.Value.transform.position.x;
        rightEdge = activeBackgrounds.Last.Value.transform.position.x;
    }

    void Update()
    {
        float playerX = player.position.x;

        // ����� Y�� ��ġ ���
        baseYPosition = Mathf.Lerp(baseYPosition, player.position.y, yParallaxFactor * Time.deltaTime);

        // ���� ��� �߰�
        if (playerX < leftEdge + backgroundWidth * 1.5f)
        {
            SpawnBackgroundAt(leftEdge - backgroundWidth, baseYPosition);
            RemoveBackgroundFromRight();
        }

        // ������ ��� �߰�
        if (playerX > rightEdge - backgroundWidth * 1.5f)
        {
            SpawnBackgroundAt(rightEdge + backgroundWidth, baseYPosition);
            RemoveBackgroundFromLeft();
        }
    }

    void SpawnBackgroundAt(float xPosition, float yPosition)
    {
        GameObject background = objectPool.GetObject();
        background.transform.position = new Vector3(xPosition, yPosition, 0);

        // Ȱ�� ��� ����Ʈ�� �߰�
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

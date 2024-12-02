using UnityEngine;

public class Backgroundmanager : MonoBehaviour
{
    public ObjectPool_sub objectPool; // ������Ʈ Ǯ
    public float backgroundWidth = 21.504f; // ��� �̹����� ���� ũ��
    public Transform player; // �÷��̾� ��ġ
    public int initialBackgroundCount = 3; // �ʱ� ��� ����
    public float yPosition = 1.2f; // ���ϴ� Y���� ����

    private BackgroundNode head;  // ��ũ�� ����Ʈ�� ù ��° ���
    private BackgroundNode tail;  // ��ũ�� ����Ʈ�� ������ ���

    private void Start()
    {
        // �ʱ� ��� ����
        for (int i = 0; i < initialBackgroundCount; i++)
        {
            SpawnBackground(i * backgroundWidth); // ��� ��ġ
        }
    }

    private void Update()
    {
        // ����� �������� ��ũ���ϱ� ���� �÷��̾��� ��ġ�� ���� ����� �̵�
        if (player.position.x > tail.background.transform.position.x)
        {
            MoveBackgroundToRight();
        }
        else if (player.position.x < head.background.transform.position.x)
        {
            MoveBackgroundToLeft();
        }
    }

    private void SpawnBackground(float xPosition)
    {
        GameObject background = objectPool.GetObject();


        background.transform.position = new Vector3(xPosition, yPosition, 0); // x�� y ��ġ ����

        // �� ��带 ��ũ�� ����Ʈ�� �߰�
        BackgroundNode newNode = new BackgroundNode { background = background };
        if (head == null)
        {
            head = tail = newNode; // ù ��° ���
        }
        else
        {
            tail.next = newNode;
            newNode.prev = tail;
            tail = newNode;
        }
    }

    private void MoveBackgroundToRight()
    {
        // ��ũ�� ����Ʈ���� head ����� �����ϰ� tail�� �̵�
        BackgroundNode oldHead = head;
        head = head.next;
        head.prev = null;

        // ����� ������ ������ ���ġ
        float newXPosition = tail.background.transform.position.x + backgroundWidth;
        float newYPosition = tail.background.transform.position.y;
        oldHead.background.transform.position = new Vector3(newXPosition, newYPosition, 0);

        // tail�� �߰�
        tail.next = oldHead;
        oldHead.prev = tail;
        tail = oldHead;
        tail.next = null;
    }

    private void MoveBackgroundToLeft()
    {
        // ��ũ�� ����Ʈ���� tail ����� �����ϰ� head�� �̵�
        BackgroundNode oldTail = tail;
        tail = tail.prev;
        tail.next = null;

        // ����� ���� ������ ���ġ
        float newXPosition = head.background.transform.position.x - backgroundWidth;
        float newYPosition = tail.background.transform.position.y;
        oldTail.background.transform.position = new Vector3(newXPosition, newYPosition, 0);

        // head�� �߰�
        oldTail.next = head;
        head.prev = oldTail;
        head = oldTail;
    }

    // ����� ���� ��� Ŭ����
    private class BackgroundNode
    {
        public GameObject background; // ��� ������Ʈ
        public BackgroundNode next;   // ���� ���
        public BackgroundNode prev;   // ���� ���
    }
}

using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Transform player; // �÷��̾� Transform
    public float xParallaxFactor = 0.5f; // X�� Parallax ����
    public float yParallaxFactor = 0.1f; // Y�� ���� ����
    public float yFollowFactor = 0.5f; // Y�� �̵��� ���� �������� ���󰡴� ����
    public float yFollowThreshold = 3f; // Y�� �̵� ���� ���� �Ӱ谪
    public float backgroundWidth = 20.48f; // ��� �� ������ ��

    public ObjectPool objectPool; // ������Ʈ Ǯ ��ũ��Ʈ ����
    private Queue<GameObject> activeBackgrounds = new Queue<GameObject>(); // Ȱ��ȭ�� ��� ť

    private Vector3 previousPlayerPosition;
    private float leftBoundaryX; // ���� ���� ����� X ��ġ
    private float rightBoundaryX; // ���� ������ ����� X ��ġ
    private bool isFollowingY = false;

    void Start()
    {
        // �÷��̾� �ʱ� ��ġ ����
        previousPlayerPosition = player.position;

        // �ʱ� ��谪 ����
        leftBoundaryX = Mathf.Floor(player.position.x / backgroundWidth) * backgroundWidth - backgroundWidth;
        rightBoundaryX = leftBoundaryX + backgroundWidth;

        // �ʱ� ��� ����
        SpawnInitialBackgrounds();
    }

    void Update()
    {
        Vector3 playerDelta = player.position - previousPlayerPosition;

        // X�� �̵�: Parallax ȿ��
        float targetX = transform.position.x + playerDelta.x * xParallaxFactor;

        // Y�� �̵�: ���� �Ǵ� ���󰡱�
        float targetY;
        if (isFollowingY)
        {
            // �÷��̾ ���󰡴� ���
            targetY = Mathf.Lerp(transform.position.y, player.position.y, yFollowFactor * Time.deltaTime);
        }
        else
        {
            // ���� �� ���� ȿ��
            targetY = Mathf.Lerp(transform.position.y, transform.position.y + playerDelta.y, yParallaxFactor);
        }

        // ��� �̵�
        transform.position = new Vector3(targetX, targetY, transform.position.z);

        // Y�� �̵� ���� ��ȯ
        if (Mathf.Abs(playerDelta.y) > yFollowThreshold)
        {
            isFollowingY = true;
        }
        else
        {
            isFollowingY = false;
        }

        // �÷��̾� �̵� ���⿡ ���� ���ο� ��� ����
        if (player.position.x > rightBoundaryX)
        {
            SpawnBackgroundToRight();
        }
        else if (player.position.x < leftBoundaryX)
        {
            SpawnBackgroundToLeft();
        }

        // Ȱ�� ��� ����
        ManageActiveBackgrounds();

        // �÷��̾� ��ġ ����
        previousPlayerPosition = player.position;
    }

    private void SpawnInitialBackgrounds()
    {
        // �ʱ� ����� �÷��̾� �������� ����, ������ ��ġ
        for (int i = 0; i < objectPool.poolSize; i++) // poolSize ��ŭ ��� ����
        {
            float spawnX = leftBoundaryX + i * backgroundWidth;
            GameObject bg = objectPool.GetObject();
            if (bg != null)
            {
                bg.transform.position = new Vector3(spawnX, player.position.y, transform.position.z);
                bg.SetActive(true);
                activeBackgrounds.Enqueue(bg); // ť�� �߰�
            }
        }

        // ��谪 ����
        rightBoundaryX = leftBoundaryX + objectPool.poolSize * backgroundWidth;
    }

    private void SpawnBackgroundToRight()
    {
        GameObject bg = objectPool.GetObject();
        if (bg != null)
        {
            // ��� ������Ʈ ��ġ
            bg.transform.position = new Vector3(rightBoundaryX, player.position.y, transform.position.z);
            bg.SetActive(true);
            activeBackgrounds.Enqueue(bg);

            // ��谪 ����
            rightBoundaryX += backgroundWidth;
        }
    }

    private void SpawnBackgroundToLeft()
    {
        GameObject bg = objectPool.GetObject();
        if (bg != null)
        {
            // ��� ������Ʈ ��ġ
            bg.transform.position = new Vector3(leftBoundaryX - backgroundWidth, player.position.y, transform.position.z);
            bg.SetActive(true);
            activeBackgrounds.Enqueue(bg);

            // ��谪 ����
            leftBoundaryX -= backgroundWidth; // �������� ����� �̵�
        }
    }

    private void ManageActiveBackgrounds()
    {
        while (activeBackgrounds.Count > 0)
        {
            GameObject bg = activeBackgrounds.Peek();

            // ����� ȭ�� ������ ��� ���
            if (bg.transform.position.x < player.position.x - backgroundWidth * 2 ||
                bg.transform.position.x > player.position.x + backgroundWidth * 2)
            {
                // ��Ȱ��ȭ �� Ǯ�� ��ȯ
                bg.SetActive(false);
                objectPool.ReturnObject(bg);
                activeBackgrounds.Dequeue(); // ť���� ����
            }
            else
            {
                break; // ť���� ����ؼ� ����� ����� ���������� ���߱�
            }
        }
    }
}

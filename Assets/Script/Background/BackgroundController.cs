using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Transform player; // �÷��̾� Transform
    public ObjectPool objectPool; // ������Ʈ Ǯ (��� ������ ����)
    public float backgroundWidth = 20.48f; // ��� �� ���� �ʺ�

    private Queue<GameObject> activeBackgrounds = new Queue<GameObject>();
    private float leftBoundaryX;
    private float rightBoundaryX;

    void Start()
    {
        // �ʱ� ��� ����
        SpawnInitialBackgrounds();
    }

    void Update()
    {
        // ��� ���� �� ���� ����
        if (player.position.x > rightBoundaryX)
        {
            SpawnBackgroundToRight();
        }
        if (player.position.x < leftBoundaryX)
        {
            SpawnBackgroundToLeft();
        }

        ManageActiveBackgrounds();
    }

    private void SpawnInitialBackgrounds()
    {
        float initialX = Mathf.Floor(player.position.x / backgroundWidth) * backgroundWidth;

        // �ʱ� ���: �÷��̾� �������� ����, �߰�, ������
        for (int i = -1; i <= 1; i++)
        {
            GameObject bg = objectPool.GetObject();
            bg.transform.position = new Vector3(initialX + i * backgroundWidth, transform.position.y, transform.position.z);
            bg.SetActive(true);
            activeBackgrounds.Enqueue(bg);
        }

        leftBoundaryX = initialX - backgroundWidth;
        rightBoundaryX = initialX + backgroundWidth;
    }

    private void SpawnBackgroundToLeft()
    {
        GameObject bg = objectPool.GetObject();
        float spawnX = leftBoundaryX - backgroundWidth;

        bg.transform.position = new Vector3(spawnX, transform.position.y, transform.position.z);
        bg.SetActive(true);
        activeBackgrounds.Enqueue(bg);

        leftBoundaryX -= backgroundWidth;
    }

    private void SpawnBackgroundToRight()
    {
        GameObject bg = objectPool.GetObject();
        float spawnX = rightBoundaryX + backgroundWidth;

        bg.transform.position = new Vector3(spawnX, transform.position.y, transform.position.z);
        bg.SetActive(true);
        activeBackgrounds.Enqueue(bg);

        rightBoundaryX += backgroundWidth;
    }

    private void ManageActiveBackgrounds()
    {
        while (activeBackgrounds.Count > 0)
        {
            GameObject bg = activeBackgrounds.Peek();

            // �÷��̾���� �Ÿ� ������� ��� ����
            if (bg.transform.position.x < player.position.x - backgroundWidth * 2 ||
                bg.transform.position.x > player.position.x + backgroundWidth * 2)
            {
                activeBackgrounds.Dequeue();
                objectPool.ReturnObject(bg);
            }
            else
            {
                break;
            }
        }
    }
}

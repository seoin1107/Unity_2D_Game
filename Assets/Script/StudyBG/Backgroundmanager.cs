using UnityEngine;

public class Backgroundmanager : MonoBehaviour
{
    public Transform player; // �÷��̾�
    public ObjectPool_sub objectPool; // ����� ������ ������Ʈ Ǯ
    public float backgroundWidth = 20.48f; // ����� ���� ũ��

    private float leftBoundaryX;
    private float rightBoundaryX;
    private GameObject[] activeBackgrounds; // ���� Ȱ��ȭ�� ����

    void Start()
    {
        // �ʱ� ��� ����
        CreateInitialBackgrounds();
    }

    void Update()
    {
        // �÷��̾ ����, ������ ��踦 �Ѿ��� �� ��� ����
        if (player.position.x > rightBoundaryX - backgroundWidth)
        {
            SpawnBackgroundToRight();
        }
        if (player.position.x < leftBoundaryX + backgroundWidth)
        {
            SpawnBackgroundToLeft();
        }

        // ����� ��ġ�� �����Ͽ� ȭ�鿡 �°� ��ũ��
        ManageActiveBackgrounds();
    }

    // �ʱ� ��� ����
    private void CreateInitialBackgrounds()
    {
        float initialX = Mathf.Floor(player.position.x / backgroundWidth) * backgroundWidth;
        activeBackgrounds = new GameObject[3]; // ����, �߰�, ������ ����� ����

        for (int i = -1; i <= 1; i++)
        {
            GameObject bg = objectPool.GetObject();
            bg.transform.position = new Vector3(initialX + i * backgroundWidth, transform.position.y, transform.position.z);
            bg.SetActive(true);
            activeBackgrounds[i + 1] = bg; // �ε��� -1, 0, 1�� �°� ����
        }

        leftBoundaryX = initialX - backgroundWidth;
        rightBoundaryX = initialX + backgroundWidth;
    }

    // ����� ���������� ����
    private void SpawnBackgroundToRight()
    {
        // ���� ���� ����� Ǯ�� ��ȯ
        objectPool.ReturnObject(activeBackgrounds[0]);

        // ������ ���� �̵�
        activeBackgrounds[0] = activeBackgrounds[1];
        activeBackgrounds[1] = activeBackgrounds[2];

        // ���ο� ��� ����
        GameObject bg = objectPool.GetObject();
        bg.transform.position = new Vector3(rightBoundaryX + backgroundWidth, transform.position.y, transform.position.z);
        bg.SetActive(true);

        // ���� ������ ����� �迭�� �߰�
        activeBackgrounds[2] = bg;

        // ������ ��� ������Ʈ
        rightBoundaryX += backgroundWidth;
    }

    // ����� �������� ����
    private void SpawnBackgroundToLeft()
    {
        // ���� ������ ����� Ǯ�� ��ȯ
        objectPool.ReturnObject(activeBackgrounds[2]);

        // ������ ���� �̵�
        activeBackgrounds[2] = activeBackgrounds[1];
        activeBackgrounds[1] = activeBackgrounds[0];

        // ���ο� ��� ����
        GameObject bg = objectPool.GetObject();
        bg.transform.position = new Vector3(leftBoundaryX - backgroundWidth, transform.position.y, transform.position.z);
        bg.SetActive(true);

        // ���� ������ ����� �迭�� �߰�
        activeBackgrounds[0] = bg;

        // ���� ��� ������Ʈ
        leftBoundaryX -= backgroundWidth;
    }

    // ȭ�� ������ ���� ����� Ǯ�� ��ȯ
    private void ManageActiveBackgrounds()
    {
        // ������ ȭ�� ������ ������ �� �ڵ����� Ǯ�� ��ȯ�� �� �ֵ��� ����
        for (int i = 0; i < activeBackgrounds.Length; i++)
        {
            GameObject bg = activeBackgrounds[i];
            if (bg != null && (bg.transform.position.x < player.position.x - backgroundWidth * 2 ||
                bg.transform.position.x > player.position.x + backgroundWidth * 2))
            {
                objectPool.ReturnObject(bg); // ȭ�� ������ ���� ����� Ǯ�� ��ȯ
                activeBackgrounds[i] = null; // �迭���� �ش� ����� null�� ����
            }
        }
    }
}

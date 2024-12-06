using UnityEngine;

public class Backgroundmanager : MonoBehaviour
{
    public Transform player;             // �÷��̾� Transform
    public float scrollSpeed;            // ����� ��ũ�� �ӵ�
    public float backgroundWidth;        // ��� �̹����� ���� ũ��
    public GameObject backgroundPrefab;  // ��� �̹��� ������
    public Camera backgroundCamera;      // ��� ī�޶�

    private GameObject[] backgroundObjects; // ��� �̹��� ��ü �迭
    private int currentBackgroundIndex = 0; // ���� ��� �ε���

    void Start()
    {
        // ��� �̹����� �����Ͽ� �ʱ�ȭ
        backgroundObjects = new GameObject[2]; // ��� 2���� �ݺ��ؼ� ���

        for (int i = 0; i < backgroundObjects.Length; i++)
        {
            backgroundObjects[i] = Instantiate(backgroundPrefab);
            backgroundObjects[i].transform.position = new Vector3(i * backgroundWidth, 0, 0); // ����� ���η� ��ġ
        }
    }

    void Update()
    {
        MoveBackground();
    }

    private void MoveBackground()
    {
        // ����� ��ġ�� �̵����� ���� ��ũ�� ó��
        float targetX = player.position.x * scrollSpeed;
        backgroundObjects[currentBackgroundIndex].transform.position = new Vector3(targetX, 0, backgroundObjects[currentBackgroundIndex].transform.position.z);

        // ����� ������ �������� ��ġ�� �����Ͽ� �ݺ�
        if (backgroundObjects[currentBackgroundIndex].transform.position.x <= -backgroundWidth)
        {
            // ����� ȭ���� ����� ��� ��ġ�� �ʱ�ȭ�ϰ� ���� �������
            backgroundObjects[currentBackgroundIndex].transform.position = new Vector3(backgroundObjects[(currentBackgroundIndex + 1) % 2].transform.position.x + backgroundWidth, 0, 0);
            currentBackgroundIndex = (currentBackgroundIndex + 1) % 2; // ��� �ε��� ����
        }

        // ��� ī�޶�� y�ุ ���󰡰�, x���� ���� ��ũ�� ó��
        backgroundCamera.transform.position = new Vector3(player.position.x * scrollSpeed, backgroundCamera.transform.position.y, backgroundCamera.transform.position.z);
    }
}

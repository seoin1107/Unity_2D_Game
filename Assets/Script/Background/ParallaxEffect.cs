using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Transform player; // �÷��̾� Transform
    public float xParallaxFactor = 0.5f; // X�� Parallax ����
    public float yParallaxFactor = 0.1f; // Y�� ���� ����

    private Vector3 previousPlayerPosition;

    void Start()
    {
        // �÷��̾��� �ʱ� ��ġ ����
        previousPlayerPosition = player.position;
    }

    void Update()
    {
        Vector3 playerDelta = player.position - previousPlayerPosition;

        // Parallax ȿ�� ���
        float targetX = transform.position.x + playerDelta.x * xParallaxFactor;
        float targetY = Mathf.Lerp(transform.position.y, transform.position.y + playerDelta.y, yParallaxFactor);

        // ��ġ ����
        transform.position = new Vector3(targetX, targetY, transform.position.z);

        // �÷��̾� ��ġ ����
        previousPlayerPosition = player.position;
    }
}

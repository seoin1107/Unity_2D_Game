using UnityEngine;

public class BackCameraManager : MonoBehaviour
{
    public Transform player;          // �÷��̾� Transform
    public float scrollSpeedX;
    public float scrollSpeedY;         // ��� �ӵ� ���� (0.5 = �÷��̾� �ӵ��� 50%)
    public float initialYOffset;      // y�� ���� ��ġ ������
    public float backgroundWidth;     // ��� �̹����� ���� ����
    public float backgroundHeight;    // ��� �̹����� ���� ����
    public Camera backgroundCamera;   // ��� ī�޶� (�Ϲ� ī�޶�)

    private Camera cam;               // ��� ī�޶� ������Ʈ
    private float startPositionX;     // �ʱ� x�� ��ġ

    private void Start()
    {
        cam = GetComponent<Camera>(); // ��� ī�޶� ������Ʈ ��������
        startPositionX = transform.position.x; // �ʱ� x ��ġ ����
        // ��� ī�޶��� y�� ���� ��ġ ����
        transform.position = new Vector3(transform.position.x, player.position.y + initialYOffset, transform.position.z);
    }

    private void LateUpdate()
    {
        // x��: ���� ��ũ�� ������� ��� �̹��� �̵�
        float targetX = player.position.x * scrollSpeedX;

        // y��: �÷��̾��� y�� ��ġ�� �������� ��� ī�޶� �̵� (�ʿ信 ���� ���� ȿ���� �� ���� ����)
        float targetY = player.position.y * scrollSpeedY + initialYOffset;

        // x�� ���� ���� (��� �̹����� ũ�⿡ ���缭 ���� ��ũ��)
        float clampedX = Mathf.Repeat(targetX - startPositionX, backgroundWidth) + startPositionX;

        // ��� ī�޶� ��ġ ������Ʈ (y���� �÷��̾��� y��ġ�� ���� �����̰�, x���� ���� ��ũ��)
        backgroundCamera.transform.position = new Vector3(clampedX, targetY, backgroundCamera.transform.position.z);
    }
}

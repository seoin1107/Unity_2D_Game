using UnityEngine;

public class BackCameraManager : MonoBehaviour
{
    public Transform playerTransform;  // �÷��̾��� Transform
    public Transform backgroundCamera;  // ��� ī�޶��� Transform

    public float xMovementRatio = 0.5f;  // ��� ī�޶� x�� �̵� ���� (�÷��̾� �̵����� ���� ����)
    public float yMovementRatio = 0.2f;  // ��� ī�޶� y�� �̵� ���� (�÷��̾� �̵����� ���� ����)
    public float xOffset = 0f;
    public float yOffset = 0f;  // ��� ī�޶��� y�� ������ (�÷��̾��� y���� ���󰡵� �ణ ���̸� �� �� ���)

    private Vector3 previousPlayerPosition;  // ���� �÷��̾��� ��ġ (�̵����� ����ϱ� ����)

    void Start()
    {
        previousPlayerPosition = playerTransform.position;
    }

    void Update()
    {
        // x��: �÷��̾��� �̵����� ����� ��� ī�޶��� x�� �̵�
        float playerXMovement = playerTransform.position.x - previousPlayerPosition.x;
        float targetX = backgroundCamera.position.x + playerXMovement * xMovementRatio;
        //float targetX = playerXMovement * xMovementRatio + xOffset;

        // y��: �÷��̾��� y�� �̵����� ���� ������ ��� ī�޶� �̵��ϵ��� ����
        float playerYMovement = playerTransform.position.y;
        float targetY = playerYMovement * yMovementRatio + yOffset;

        // ��� ī�޶��� ��ġ�� ����
        backgroundCamera.position = new Vector3(targetX, targetY, backgroundCamera.position.z);

        // ���� ��ġ ������Ʈ (���� ������Ʈ���� �̵����� ����� �� �ֵ���)
        previousPlayerPosition = playerTransform.position;
    }
}

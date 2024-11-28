using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public string playerLayerName = "Player"; // Player�� ���� Layer �̸�
    public Vector2 parallaxFactor = new Vector2(1.0f, 1.0f); // X, Y�࿡ ���� Parallax ����

    private Transform player;
    private Vector3 lastPlayerPosition;

    void Start()
    {
        // Player Layer�� ���� ������Ʈ�� ã�� ����
        int playerLayer = LayerMask.NameToLayer(playerLayerName);

        if (playerLayer >= 0)
        {
            GameObject playerObject = FindObjectOfType<GameObject>();
            foreach (var obj in FindObjectsOfType<GameObject>())
            {
                if (obj.layer == playerLayer)
                {
                    player = obj.transform;
                    break;
                }
            }
        }

        if (player != null)
        {
            lastPlayerPosition = player.position;
        }
        else
        {
            Debug.LogWarning($"Player Layer '{playerLayerName}'�� ���� ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    void Update()
    {
        if (player == null) return;

        // �÷��̾� �̵��� ���
        Vector3 deltaMovement = player.position - lastPlayerPosition;

        // ����� �÷��̾� �̵����� ���� ���� (X, Y�࿡ ���� Parallax Factor ����)
        transform.position += new Vector3(0, deltaMovement.y * parallaxFactor.y, 0);

        // �÷��̾��� ���� ��ġ ������Ʈ
        lastPlayerPosition = player.position;
    }
}

using UnityEngine;

public class LightingController : MonoBehaviour
{
    public Light playerLight;  // �÷��̾� �ֺ��� �ִ� ����Ʈ
    public Transform player;     // �÷��̾� ��ü
    

    void Update()
    {
        // �÷��̾� ��ġ�� ���� ����Ʈ ��ġ ����
        playerLight.transform.position = player.position;
    }
}

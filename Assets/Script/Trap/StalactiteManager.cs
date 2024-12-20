using UnityEngine;

public class StalactiteManager : MonoBehaviour
{
    private bool isTriggered = false; // Ʈ���Ű� �ѹ��� �۵��ϵ��� ������ ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTriggered) return; // �̹� Ʈ���Ű� �۵������� �� �̻� ó������ ����

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // �ڽ��� �������� Ȱ��ȭ
            Transform stalactite = transform.Find("Stalactite");
            if (stalactite != null && !stalactite.gameObject.activeSelf)
            {
                stalactite.gameObject.SetActive(true); // �ڽ� ������ Ȱ��ȭ
            }

            // Ʈ���� ��Ȱ��ȭ (�� ���� �۵��ϵ���)
            isTriggered = true; // Ʈ���� ���¸� true�� �����Ͽ� �� ���� �۵��ϵ��� ��
        }
    }
}

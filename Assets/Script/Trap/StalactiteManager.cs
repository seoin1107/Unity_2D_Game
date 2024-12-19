using UnityEngine;

public class StalactiteManager : MonoBehaviour
{
    bool IsActive = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && IsActive == false)
        {
            // Ʈ���ſ� ������ �ڽ��� �������� Ȱ��ȭ
            Transform stalactite = transform.Find("Stalactite"); // �ڽ� ������Ʈ �̸��� "Stalactite"
            if (stalactite != null && !stalactite.gameObject.activeSelf)
            {
                stalactite.gameObject.SetActive(true); // �ڽ� ������ Ȱ��ȭ
                IsActive = true;
            }
        }
    }
}

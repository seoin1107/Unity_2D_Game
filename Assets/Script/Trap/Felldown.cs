using UnityEngine;
using Cinemachine;

public class Felldown : MonoBehaviour
{
    public CinemachineVirtualCamera fallCamera; // ���� ���� ���� ī�޶�
    public CinemachineVirtualCamera currentCamera; // �÷��̾��� ���� ī�޶�
    private CinemachineBrain brain; // CinemachineBrain ����
    private bool isFelldown;

    void Start()
    {
        // CinemachineBrain�� �����ɴϴ�.
        brain = Camera.main.GetComponent<CinemachineBrain>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && isFelldown == false)
        {
            SwitchToFallCameraImmediately(fallCamera);  // ���� ī�޶�� ��� ��ȯ
        }
    }

    void SwitchToFallCameraImmediately(CinemachineVirtualCamera fallCamera)
    {
        if (brain != null)
        {
            // ���� ī�޶�� ��Ȱ��ȭ
            if (currentCamera != null)
            {
                currentCamera.gameObject.SetActive(false);
            }

            // ���� ī�޶� �켱���� ���̱�
            fallCamera.Priority = 100; // �켱���� ����

            // ���� ī�޶�� ��� ��ȯ (�켱������ ���� �ڵ����� Ȱ��ȭ��)
            currentCamera = fallCamera;
            isFelldown = true;
        }
    }
}

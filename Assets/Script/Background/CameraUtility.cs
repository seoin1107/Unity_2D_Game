using Cinemachine;
using UnityEngine;

public static class CameraUtility
{
    // ī�޶� Ȱ��ȭ �� �켱���� ����
    public static void ActivateCamera(CinemachineVirtualCamera cameraToActivate, int priority = 20)
    {
        if (cameraToActivate != null)
        {
            cameraToActivate.Priority = priority; // �켱���� ����
            cameraToActivate.gameObject.SetActive(true); // Ȱ��ȭ
        }
    }

    // ī�޶� ��Ȱ��ȭ �� �켱���� �缳��
    public static void DeactivateCamera(CinemachineVirtualCamera cameraToDeactivate, int defaultPriority = 10)
    {
        if (cameraToDeactivate != null)
        {
            cameraToDeactivate.Priority = defaultPriority; // �⺻ �켱������ �缳��
            cameraToDeactivate.gameObject.SetActive(false); // ��Ȱ��ȭ
        }
    }
}

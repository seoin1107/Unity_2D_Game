using Cinemachine;
using UnityEngine;

public static class CameraUtility
{
    // 카메라 활성화 및 우선순위 설정
    public static void ActivateCamera(CinemachineVirtualCamera cameraToActivate, int priority = 20)
    {
        if (cameraToActivate != null)
        {
            cameraToActivate.Priority = priority; // 우선순위 변경
            cameraToActivate.gameObject.SetActive(true); // 활성화
        }
    }

    // 카메라 비활성화 및 우선순위 재설정
    public static void DeactivateCamera(CinemachineVirtualCamera cameraToDeactivate, int defaultPriority = 10)
    {
        if (cameraToDeactivate != null)
        {
            cameraToDeactivate.Priority = defaultPriority; // 기본 우선순위로 재설정
            cameraToDeactivate.gameObject.SetActive(false); // 비활성화
        }
    }
}

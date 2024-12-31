using UnityEngine;
using Cinemachine;

public class Felldown : MonoBehaviour
{
    public CinemachineVirtualCamera fallCamera; // 낙사 구역 전용 카메라
    public CinemachineVirtualCamera currentCamera; // 플레이어의 현재 카메라
    private CinemachineBrain brain; // CinemachineBrain 참조
    private bool isFelldown;

    void Start()
    {
        // CinemachineBrain을 가져옵니다.
        brain = Camera.main.GetComponent<CinemachineBrain>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && isFelldown == false)
        {
            SwitchToFallCameraImmediately(fallCamera);  // 낙사 카메라로 즉시 전환
        }
    }

    void SwitchToFallCameraImmediately(CinemachineVirtualCamera fallCamera)
    {
        if (brain != null)
        {
            // 기존 카메라는 비활성화
            if (currentCamera != null)
            {
                currentCamera.gameObject.SetActive(false);
            }

            // 낙사 카메라 우선순위 높이기
            fallCamera.Priority = 100; // 우선순위 설정

            // 낙사 카메라로 즉시 전환 (우선순위에 따라 자동으로 활성화됨)
            currentCamera = fallCamera;
            isFelldown = true;
        }
    }
}

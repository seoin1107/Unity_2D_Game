using UnityEngine;

public class BackCameraManager : MonoBehaviour
{
    public Transform playerTransform;  // 플레이어의 Transform
    public Transform backgroundCamera;  // 배경 카메라의 Transform

    public float xMovementRatio = 0.5f;  // 배경 카메라 x축 이동 비율 (플레이어 이동량에 대한 비율)
    public float yMovementRatio = 0.2f;  // 배경 카메라 y축 이동 비율 (플레이어 이동량에 대한 비율)
    public float xOffset = 0f;
    public float yOffset = 0f;  // 배경 카메라의 y축 오프셋 (플레이어의 y축을 따라가되 약간 차이를 둘 때 사용)

    private Vector3 previousPlayerPosition;  // 이전 플레이어의 위치 (이동량을 계산하기 위함)

    void Start()
    {
        previousPlayerPosition = playerTransform.position;
    }

    void Update()
    {
        // x축: 플레이어의 이동량에 비례한 배경 카메라의 x축 이동
        float playerXMovement = playerTransform.position.x - previousPlayerPosition.x;
        float targetX = backgroundCamera.position.x + playerXMovement * xMovementRatio;
        //float targetX = playerXMovement * xMovementRatio + xOffset;

        // y축: 플레이어의 y축 이동량에 대한 비율로 배경 카메라가 이동하도록 설정
        float playerYMovement = playerTransform.position.y;
        float targetY = playerYMovement * yMovementRatio + yOffset;

        // 배경 카메라의 위치를 설정
        backgroundCamera.position = new Vector3(targetX, targetY, backgroundCamera.position.z);

        // 이전 위치 업데이트 (다음 업데이트에서 이동량을 계산할 수 있도록)
        previousPlayerPosition = playerTransform.position;
    }
}

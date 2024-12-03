using UnityEngine;

public class BackCameraManager : MonoBehaviour
{
    public Transform player;          // 플레이어 Transform
    public float scrollSpeedX;
    public float scrollSpeedY;         // 배경 속도 조정 (0.5 = 플레이어 속도의 50%)
    public float initialYOffset;      // y축 시작 위치 오프셋
    public float backgroundWidth;     // 배경 이미지의 가로 길이
    public float backgroundHeight;    // 배경 이미지의 세로 길이
    public Camera backgroundCamera;   // 배경 카메라 (일반 카메라)

    private Camera cam;               // 배경 카메라 컴포넌트
    private float startPositionX;     // 초기 x축 위치

    private void Start()
    {
        cam = GetComponent<Camera>(); // 배경 카메라 컴포넌트 가져오기
        startPositionX = transform.position.x; // 초기 x 위치 저장
        // 배경 카메라의 y축 시작 위치 설정
        transform.position = new Vector3(transform.position.x, player.position.y + initialYOffset, transform.position.z);
    }

    private void LateUpdate()
    {
        // x축: 무한 스크롤 방식으로 배경 이미지 이동
        float targetX = player.position.x * scrollSpeedX;

        // y축: 플레이어의 y축 위치를 기준으로 배경 카메라 이동 (필요에 따라 감쇠 효과를 줄 수도 있음)
        float targetY = player.position.y * scrollSpeedY + initialYOffset;

        // x축 범위 제한 (배경 이미지의 크기에 맞춰서 무한 스크롤)
        float clampedX = Mathf.Repeat(targetX - startPositionX, backgroundWidth) + startPositionX;

        // 배경 카메라 위치 업데이트 (y축은 플레이어의 y위치에 따라 움직이고, x축은 무한 스크롤)
        backgroundCamera.transform.position = new Vector3(clampedX, targetY, backgroundCamera.transform.position.z);
    }
}

using UnityEngine;

public class Backgroundmanager : MonoBehaviour
{
    public Transform player;             // 플레이어 Transform
    public float scrollSpeed;            // 배경의 스크롤 속도
    public float backgroundWidth;        // 배경 이미지의 가로 크기
    public GameObject backgroundPrefab;  // 배경 이미지 프리팹
    public Camera backgroundCamera;      // 배경 카메라

    private GameObject[] backgroundObjects; // 배경 이미지 객체 배열
    private int currentBackgroundIndex = 0; // 현재 배경 인덱스

    void Start()
    {
        // 배경 이미지를 복제하여 초기화
        backgroundObjects = new GameObject[2]; // 배경 2개를 반복해서 사용

        for (int i = 0; i < backgroundObjects.Length; i++)
        {
            backgroundObjects[i] = Instantiate(backgroundPrefab);
            backgroundObjects[i].transform.position = new Vector3(i * backgroundWidth, 0, 0); // 배경을 가로로 배치
        }
    }

    void Update()
    {
        MoveBackground();
    }

    private void MoveBackground()
    {
        // 배경의 위치를 이동시켜 무한 스크롤 처리
        float targetX = player.position.x * scrollSpeed;
        backgroundObjects[currentBackgroundIndex].transform.position = new Vector3(targetX, 0, backgroundObjects[currentBackgroundIndex].transform.position.z);

        // 배경이 끝까지 지나가면 위치를 리셋하여 반복
        if (backgroundObjects[currentBackgroundIndex].transform.position.x <= -backgroundWidth)
        {
            // 배경이 화면을 벗어났을 경우 위치를 초기화하고 다음 배경으로
            backgroundObjects[currentBackgroundIndex].transform.position = new Vector3(backgroundObjects[(currentBackgroundIndex + 1) % 2].transform.position.x + backgroundWidth, 0, 0);
            currentBackgroundIndex = (currentBackgroundIndex + 1) % 2; // 배경 인덱스 변경
        }

        // 배경 카메라는 y축만 따라가고, x축은 무한 스크롤 처리
        backgroundCamera.transform.position = new Vector3(player.position.x * scrollSpeed, backgroundCamera.transform.position.y, backgroundCamera.transform.position.z);
    }
}

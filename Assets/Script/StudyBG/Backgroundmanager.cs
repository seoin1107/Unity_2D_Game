using UnityEngine;

public class Backgroundmanager : MonoBehaviour
{
    public Transform player; // 플레이어
    public ObjectPool_sub objectPool; // 배경을 관리할 오브젝트 풀
    public float backgroundWidth = 20.48f; // 배경의 가로 크기

    private float leftBoundaryX;
    private float rightBoundaryX;
    private GameObject[] activeBackgrounds; // 현재 활성화된 배경들

    void Start()
    {
        // 초기 배경 생성
        CreateInitialBackgrounds();
    }

    void Update()
    {
        // 플레이어가 왼쪽, 오른쪽 경계를 넘었을 때 배경 생성
        if (player.position.x > rightBoundaryX - backgroundWidth)
        {
            SpawnBackgroundToRight();
        }
        if (player.position.x < leftBoundaryX + backgroundWidth)
        {
            SpawnBackgroundToLeft();
        }

        // 배경의 위치를 관리하여 화면에 맞게 스크롤
        ManageActiveBackgrounds();
    }

    // 초기 배경 생성
    private void CreateInitialBackgrounds()
    {
        float initialX = Mathf.Floor(player.position.x / backgroundWidth) * backgroundWidth;
        activeBackgrounds = new GameObject[3]; // 왼쪽, 중간, 오른쪽 배경을 관리

        for (int i = -1; i <= 1; i++)
        {
            GameObject bg = objectPool.GetObject();
            bg.transform.position = new Vector3(initialX + i * backgroundWidth, transform.position.y, transform.position.z);
            bg.SetActive(true);
            activeBackgrounds[i + 1] = bg; // 인덱스 -1, 0, 1에 맞게 저장
        }

        leftBoundaryX = initialX - backgroundWidth;
        rightBoundaryX = initialX + backgroundWidth;
    }

    // 배경을 오른쪽으로 생성
    private void SpawnBackgroundToRight()
    {
        // 가장 왼쪽 배경을 풀에 반환
        objectPool.ReturnObject(activeBackgrounds[0]);

        // 나머지 배경들 이동
        activeBackgrounds[0] = activeBackgrounds[1];
        activeBackgrounds[1] = activeBackgrounds[2];

        // 새로운 배경 생성
        GameObject bg = objectPool.GetObject();
        bg.transform.position = new Vector3(rightBoundaryX + backgroundWidth, transform.position.y, transform.position.z);
        bg.SetActive(true);

        // 새로 생성된 배경을 배열에 추가
        activeBackgrounds[2] = bg;

        // 오른쪽 경계 업데이트
        rightBoundaryX += backgroundWidth;
    }

    // 배경을 왼쪽으로 생성
    private void SpawnBackgroundToLeft()
    {
        // 가장 오른쪽 배경을 풀에 반환
        objectPool.ReturnObject(activeBackgrounds[2]);

        // 나머지 배경들 이동
        activeBackgrounds[2] = activeBackgrounds[1];
        activeBackgrounds[1] = activeBackgrounds[0];

        // 새로운 배경 생성
        GameObject bg = objectPool.GetObject();
        bg.transform.position = new Vector3(leftBoundaryX - backgroundWidth, transform.position.y, transform.position.z);
        bg.SetActive(true);

        // 새로 생성된 배경을 배열에 추가
        activeBackgrounds[0] = bg;

        // 왼쪽 경계 업데이트
        leftBoundaryX -= backgroundWidth;
    }

    // 화면 밖으로 나간 배경을 풀로 반환
    private void ManageActiveBackgrounds()
    {
        // 배경들이 화면 밖으로 나갔을 때 자동으로 풀로 반환될 수 있도록 관리
        for (int i = 0; i < activeBackgrounds.Length; i++)
        {
            GameObject bg = activeBackgrounds[i];
            if (bg != null && (bg.transform.position.x < player.position.x - backgroundWidth * 2 ||
                bg.transform.position.x > player.position.x + backgroundWidth * 2))
            {
                objectPool.ReturnObject(bg); // 화면 밖으로 나간 배경을 풀로 반환
                activeBackgrounds[i] = null; // 배열에서 해당 배경을 null로 설정
            }
        }
    }
}

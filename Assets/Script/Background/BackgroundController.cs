using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Transform player; // 플레이어 Transform
    public float xParallaxFactor = 0.5f; // X축 Parallax 비율
    public float yParallaxFactor = 0.1f; // Y축 감쇠 비율
    public float yFollowFactor = 0.5f; // Y축 이동이 많은 구간에서 따라가는 비율
    public float yFollowThreshold = 3f; // Y축 이동 구간 감지 임계값
    public float backgroundWidth = 20.48f; // 배경 한 조각의 폭

    public ObjectPool objectPool; // 오브젝트 풀 스크립트 참조
    private Queue<GameObject> activeBackgrounds = new Queue<GameObject>(); // 활성화된 배경 큐

    private Vector3 previousPlayerPosition;
    private float leftBoundaryX; // 가장 왼쪽 배경의 X 위치
    private float rightBoundaryX; // 가장 오른쪽 배경의 X 위치
    private bool isFollowingY = false;

    void Start()
    {
        // 플레이어 초기 위치 저장
        previousPlayerPosition = player.position;

        // 초기 경계값 설정
        leftBoundaryX = Mathf.Floor(player.position.x / backgroundWidth) * backgroundWidth - backgroundWidth;
        rightBoundaryX = leftBoundaryX + backgroundWidth;

        // 초기 배경 생성
        SpawnInitialBackgrounds();
    }

    void Update()
    {
        Vector3 playerDelta = player.position - previousPlayerPosition;

        // X축 이동: Parallax 효과
        float targetX = transform.position.x + playerDelta.x * xParallaxFactor;

        // Y축 이동: 감쇠 또는 따라가기
        float targetY;
        if (isFollowingY)
        {
            // 플레이어를 따라가는 방식
            targetY = Mathf.Lerp(transform.position.y, player.position.y, yFollowFactor * Time.deltaTime);
        }
        else
        {
            // 점프 시 감쇠 효과
            targetY = Mathf.Lerp(transform.position.y, transform.position.y + playerDelta.y, yParallaxFactor);
        }

        // 배경 이동
        transform.position = new Vector3(targetX, targetY, transform.position.z);

        // Y축 이동 상태 전환
        if (Mathf.Abs(playerDelta.y) > yFollowThreshold)
        {
            isFollowingY = true;
        }
        else
        {
            isFollowingY = false;
        }

        // 플레이어 이동 방향에 따라 새로운 배경 생성
        if (player.position.x > rightBoundaryX)
        {
            SpawnBackgroundToRight();
        }
        else if (player.position.x < leftBoundaryX)
        {
            SpawnBackgroundToLeft();
        }

        // 활성 배경 관리
        ManageActiveBackgrounds();

        // 플레이어 위치 갱신
        previousPlayerPosition = player.position;
    }

    private void SpawnInitialBackgrounds()
    {
        // 초기 배경을 플레이어 기준으로 왼쪽, 오른쪽 배치
        for (int i = 0; i < objectPool.poolSize; i++) // poolSize 만큼 배경 생성
        {
            float spawnX = leftBoundaryX + i * backgroundWidth;
            GameObject bg = objectPool.GetObject();
            if (bg != null)
            {
                bg.transform.position = new Vector3(spawnX, player.position.y, transform.position.z);
                bg.SetActive(true);
                activeBackgrounds.Enqueue(bg); // 큐에 추가
            }
        }

        // 경계값 갱신
        rightBoundaryX = leftBoundaryX + objectPool.poolSize * backgroundWidth;
    }

    private void SpawnBackgroundToRight()
    {
        GameObject bg = objectPool.GetObject();
        if (bg != null)
        {
            // 배경 오브젝트 배치
            bg.transform.position = new Vector3(rightBoundaryX, player.position.y, transform.position.z);
            bg.SetActive(true);
            activeBackgrounds.Enqueue(bg);

            // 경계값 갱신
            rightBoundaryX += backgroundWidth;
        }
    }

    private void SpawnBackgroundToLeft()
    {
        GameObject bg = objectPool.GetObject();
        if (bg != null)
        {
            // 배경 오브젝트 배치
            bg.transform.position = new Vector3(leftBoundaryX - backgroundWidth, player.position.y, transform.position.z);
            bg.SetActive(true);
            activeBackgrounds.Enqueue(bg);

            // 경계값 갱신
            leftBoundaryX -= backgroundWidth; // 왼쪽으로 배경을 이동
        }
    }

    private void ManageActiveBackgrounds()
    {
        while (activeBackgrounds.Count > 0)
        {
            GameObject bg = activeBackgrounds.Peek();

            // 배경이 화면 밖으로 벗어난 경우
            if (bg.transform.position.x < player.position.x - backgroundWidth * 2 ||
                bg.transform.position.x > player.position.x + backgroundWidth * 2)
            {
                // 비활성화 후 풀로 반환
                bg.SetActive(false);
                objectPool.ReturnObject(bg);
                activeBackgrounds.Dequeue(); // 큐에서 제거
            }
            else
            {
                break; // 큐에서 계속해서 사용할 배경이 남아있으면 멈추기
            }
        }
    }
}

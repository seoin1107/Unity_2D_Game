using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public ObjectPool objectPool; // Object Pool 참조
    public Transform player; // 플레이어 Transform
    public float backgroundWidth = 20.48f; // 배경 오브젝트의 폭
    public int activeBackgroundCount = 3; // 화면에 표시할 배경 개수
    public float yParallaxFactor = 0.1f; // Y축 이동 감쇠 비율 (0에 가까울수록 Y축 움직임이 적음)

    private LinkedList<GameObject> activeBackgrounds = new LinkedList<GameObject>(); // 활성 배경 리스트
    private float leftEdge; // 가장 왼쪽 배경의 X 위치
    private float rightEdge; // 가장 오른쪽 배경의 X 위치
    private float baseYPosition; // 배경 Y축 기준 위치

    void Start()
    {
        baseYPosition = player.position.y; // 초기 배경 기준 Y 위치

        // 초기 배경 생성
        for (int i = 0; i < activeBackgroundCount; i++)
        {
            float xPosition = i * backgroundWidth; // 초기 X 위치
            SpawnBackgroundAt(xPosition, baseYPosition);
        }

        // 경계 값 초기화
        leftEdge = activeBackgrounds.First.Value.transform.position.x;
        rightEdge = activeBackgrounds.Last.Value.transform.position.x;
    }

    void Update()
    {
        float playerX = player.position.x;

        // 감쇠된 Y축 위치 계산
        baseYPosition = Mathf.Lerp(baseYPosition, player.position.y, yParallaxFactor * Time.deltaTime);

        // 왼쪽 배경 추가
        if (playerX < leftEdge + backgroundWidth * 1.5f)
        {
            SpawnBackgroundAt(leftEdge - backgroundWidth, baseYPosition);
            RemoveBackgroundFromRight();
        }

        // 오른쪽 배경 추가
        if (playerX > rightEdge - backgroundWidth * 1.5f)
        {
            SpawnBackgroundAt(rightEdge + backgroundWidth, baseYPosition);
            RemoveBackgroundFromLeft();
        }
    }

    void SpawnBackgroundAt(float xPosition, float yPosition)
    {
        GameObject background = objectPool.GetObject();
        background.transform.position = new Vector3(xPosition, yPosition, 0);

        // 활성 배경 리스트에 추가
        if (xPosition < leftEdge)
        {
            activeBackgrounds.AddFirst(background);
            leftEdge = xPosition;
        }
        else
        {
            activeBackgrounds.AddLast(background);
            rightEdge = xPosition;
        }
    }

    void RemoveBackgroundFromLeft()
    {
        // 왼쪽에서 화면을 벗어난 배경 제거
        GameObject leftBackground = activeBackgrounds.First.Value;

        if (leftBackground.transform.position.x < player.position.x - backgroundWidth * 2f)
        {
            activeBackgrounds.RemoveFirst();
            objectPool.ReturnObject(leftBackground);
            leftEdge = activeBackgrounds.First.Value.transform.position.x;
        }
    }

    void RemoveBackgroundFromRight()
    {
        // 오른쪽에서 화면을 벗어난 배경 제거
        GameObject rightBackground = activeBackgrounds.Last.Value;

        if (rightBackground.transform.position.x > player.position.x + backgroundWidth * 2f)
        {
            activeBackgrounds.RemoveLast();
            objectPool.ReturnObject(rightBackground);
            rightEdge = activeBackgrounds.Last.Value.transform.position.x;
        }
    }
}

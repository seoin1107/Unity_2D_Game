using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Transform player; // 플레이어 Transform
    public ObjectPool objectPool; // 오브젝트 풀 (배경 프리팹 관리)
    public float backgroundWidth = 20.48f; // 배경 한 개의 너비

    private Queue<GameObject> activeBackgrounds = new Queue<GameObject>();
    private float leftBoundaryX;
    private float rightBoundaryX;

    void Start()
    {
        // 초기 배경 생성
        SpawnInitialBackgrounds();
    }

    void Update()
    {
        // 배경 생성 및 제거 관리
        if (player.position.x > rightBoundaryX)
        {
            SpawnBackgroundToRight();
        }
        if (player.position.x < leftBoundaryX)
        {
            SpawnBackgroundToLeft();
        }

        ManageActiveBackgrounds();
    }

    private void SpawnInitialBackgrounds()
    {
        float initialX = Mathf.Floor(player.position.x / backgroundWidth) * backgroundWidth;

        // 초기 배경: 플레이어 기준으로 왼쪽, 중간, 오른쪽
        for (int i = -1; i <= 1; i++)
        {
            GameObject bg = objectPool.GetObject();
            bg.transform.position = new Vector3(initialX + i * backgroundWidth, transform.position.y, transform.position.z);
            bg.SetActive(true);
            activeBackgrounds.Enqueue(bg);
        }

        leftBoundaryX = initialX - backgroundWidth;
        rightBoundaryX = initialX + backgroundWidth;
    }

    private void SpawnBackgroundToLeft()
    {
        GameObject bg = objectPool.GetObject();
        float spawnX = leftBoundaryX - backgroundWidth;

        bg.transform.position = new Vector3(spawnX, transform.position.y, transform.position.z);
        bg.SetActive(true);
        activeBackgrounds.Enqueue(bg);

        leftBoundaryX -= backgroundWidth;
    }

    private void SpawnBackgroundToRight()
    {
        GameObject bg = objectPool.GetObject();
        float spawnX = rightBoundaryX + backgroundWidth;

        bg.transform.position = new Vector3(spawnX, transform.position.y, transform.position.z);
        bg.SetActive(true);
        activeBackgrounds.Enqueue(bg);

        rightBoundaryX += backgroundWidth;
    }

    private void ManageActiveBackgrounds()
    {
        while (activeBackgrounds.Count > 0)
        {
            GameObject bg = activeBackgrounds.Peek();

            // 플레이어와의 거리 기반으로 배경 제거
            if (bg.transform.position.x < player.position.x - backgroundWidth * 2 ||
                bg.transform.position.x > player.position.x + backgroundWidth * 2)
            {
                activeBackgrounds.Dequeue();
                objectPool.ReturnObject(bg);
            }
            else
            {
                break;
            }
        }
    }
}

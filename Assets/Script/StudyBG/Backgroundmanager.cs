using UnityEngine;

public class Backgroundmanager : MonoBehaviour
{
    public ObjectPool_sub objectPool; // 오브젝트 풀
    public float backgroundWidth = 21.504f; // 배경 이미지의 가로 크기
    public Transform player; // 플레이어 위치
    public int initialBackgroundCount = 3; // 초기 배경 개수
    public float yPosition = 1.2f; // 원하는 Y값을 설정

    private BackgroundNode head;  // 링크드 리스트의 첫 번째 배경
    private BackgroundNode tail;  // 링크드 리스트의 마지막 배경

    private void Start()
    {
        // 초기 배경 설정
        for (int i = 0; i < initialBackgroundCount; i++)
        {
            SpawnBackground(i * backgroundWidth); // 배경 배치
        }
    }

    private void Update()
    {
        // 배경을 양쪽으로 스크롤하기 위해 플레이어의 위치에 따라 배경을 이동
        if (player.position.x > tail.background.transform.position.x)
        {
            MoveBackgroundToRight();
        }
        else if (player.position.x < head.background.transform.position.x)
        {
            MoveBackgroundToLeft();
        }
    }

    private void SpawnBackground(float xPosition)
    {
        GameObject background = objectPool.GetObject();


        background.transform.position = new Vector3(xPosition, yPosition, 0); // x와 y 위치 설정

        // 새 노드를 링크드 리스트에 추가
        BackgroundNode newNode = new BackgroundNode { background = background };
        if (head == null)
        {
            head = tail = newNode; // 첫 번째 배경
        }
        else
        {
            tail.next = newNode;
            newNode.prev = tail;
            tail = newNode;
        }
    }

    private void MoveBackgroundToRight()
    {
        // 링크드 리스트에서 head 배경을 제거하고 tail로 이동
        BackgroundNode oldHead = head;
        head = head.next;
        head.prev = null;

        // 배경을 오른쪽 끝으로 재배치
        float newXPosition = tail.background.transform.position.x + backgroundWidth;
        float newYPosition = tail.background.transform.position.y;
        oldHead.background.transform.position = new Vector3(newXPosition, newYPosition, 0);

        // tail에 추가
        tail.next = oldHead;
        oldHead.prev = tail;
        tail = oldHead;
        tail.next = null;
    }

    private void MoveBackgroundToLeft()
    {
        // 링크드 리스트에서 tail 배경을 제거하고 head로 이동
        BackgroundNode oldTail = tail;
        tail = tail.prev;
        tail.next = null;

        // 배경을 왼쪽 끝으로 재배치
        float newXPosition = head.background.transform.position.x - backgroundWidth;
        float newYPosition = tail.background.transform.position.y;
        oldTail.background.transform.position = new Vector3(newXPosition, newYPosition, 0);

        // head에 추가
        oldTail.next = head;
        head.prev = oldTail;
        head = oldTail;
    }

    // 배경을 위한 노드 클래스
    private class BackgroundNode
    {
        public GameObject background; // 배경 오브젝트
        public BackgroundNode next;   // 다음 노드
        public BackgroundNode prev;   // 이전 노드
    }
}

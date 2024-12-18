using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sencor : MonoBehaviour
{
    public BattleWall battleWall; // BattleWall 스크립트 참조

    // Start is called before the first frame update
    void Start()
    {
        // 필요한 초기화
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어를 감지하는 로직을 구현 (예: 트리거 방식)
    }

    // 플레이어가 센서 범위에 들어오면 호출되는 메서드
    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 센서에 들어올 때
        if (other.CompareTag("Player"))
        {
            // BattleWall의 isFalling을 true로 설정
            if (battleWall != null)
            {
                battleWall.isFalling = true;
            }
        }
    }
}

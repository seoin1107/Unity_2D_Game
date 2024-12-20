using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sencor : MonoBehaviour
{
    public bool isFalling = false; // isFalling 변수 선언

    // 충돌 체크를 위한 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트의 레이어가 "Player"인지 확인
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isFalling = true; // isFalling 값을 true로 설정

        }
    }
}


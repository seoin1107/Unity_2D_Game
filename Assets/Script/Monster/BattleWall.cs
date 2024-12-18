using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWall : MonoBehaviour
{
    public float fallSpeed;  // 떨어지는 속도
    public bool isFalling = false;  // 벽이 떨어지고 있는지 확인

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 벽이 떨어지고 있는 상태이고 y 위치가 0보다 클 경우 떨어짐
        if (isFalling == true && transform.position.y > 0)
        {
            transform.Translate(0, -fallSpeed * Time.deltaTime, 0);
            if (transform.position.y < 0) // y가 0보다 작아지면 보정
            {
                Vector3 correctedPosition = transform.position;
                correctedPosition.y = 0; // y 값을 0으로 설정
                transform.position = correctedPosition;
            }
        }
    }

    // 충돌이 시작될 때 호출되는 메서드
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어와 충돌했을 때 (Player가 "Player"라는 태그를 가지고 있다고 가정)
        if (collision.gameObject.CompareTag("Player"))
        {
            isFalling = true;  // 벽이 떨어지기 시작하도록 설정
        }
    }
}

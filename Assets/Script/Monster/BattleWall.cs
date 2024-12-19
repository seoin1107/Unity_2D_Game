using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWall : Sencor
{
    public float fallSpeed;  // 떨어지는 속도
    private Sencor sencor;
    void Start()
    {
        sencor = GetComponentInChildren<Sencor>(); // Sencor 컴포넌트를 가져옴
    }
    // Update is called once per frame
    void Update()
    {
        if (sencor.isFalling == true && transform.position.y > 0)
        {
            // 벽을 아래로 이동
            transform.Translate(0, -fallSpeed * Time.deltaTime, 0);

            // y 값이 0보다 작아지면 보정
            if (transform.position.y < 0)
            {
                Vector3 correctedPosition = transform.position;
                correctedPosition.y = 0; // y 값을 0으로 설정
                transform.position = correctedPosition;
            }
        }
        if(sencor.isFalling == false && transform.position.y <5)
        {
            transform.Translate(0, fallSpeed * Time.deltaTime, 0);
            if(transform.position.y > 5)
            {
                Vector3 correctedPosition = transform.position;
                correctedPosition.y = 5; // y 값을 0으로 설정
                transform.position = correctedPosition;
            }
        }
    }
}

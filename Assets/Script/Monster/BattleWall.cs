using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWall : Sencor
{
    public float fallSpeed;  // �������� �ӵ�
    private Sencor sencor;
    void Start()
    {
        sencor = GetComponentInChildren<Sencor>(); // Sencor ������Ʈ�� ������
    }
    // Update is called once per frame
    void Update()
    {
        if (sencor.isFalling == true && transform.position.y > 0)
        {
            // ���� �Ʒ��� �̵�
            transform.Translate(0, -fallSpeed * Time.deltaTime, 0);

            // y ���� 0���� �۾����� ����
            if (transform.position.y < 0)
            {
                Vector3 correctedPosition = transform.position;
                correctedPosition.y = 0; // y ���� 0���� ����
                transform.position = correctedPosition;
            }
        }
        if(sencor.isFalling == false && transform.position.y <5)
        {
            transform.Translate(0, fallSpeed * Time.deltaTime, 0);
            if(transform.position.y > 5)
            {
                Vector3 correctedPosition = transform.position;
                correctedPosition.y = 5; // y ���� 0���� ����
                transform.position = correctedPosition;
            }
        }
    }
}

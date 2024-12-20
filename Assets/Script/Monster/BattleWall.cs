using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWall : Sencor
{
    public float fallSpeed;  // �������� �ӵ�
    private Sencor sencor;

    private bool isRising = false;


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
        
/*        if(isRising)
        {
            transform.Translate(0, fallSpeed * Time.deltaTime, 0);

            if (transform.position.y >= 5.0f)
            {
                Vector3 correctedPosition = transform.position;
                correctedPosition.y = 5.0f; // ��ǥ ���̿��� ����
                transform.position = correctedPosition;
                isRising = false; // �� �̻� �ö��� �ʵ��� ����
            }
        }*/
    }
/*    public void StartRising()
    {
        isRising = true;
    }*/
}

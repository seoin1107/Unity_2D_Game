using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWall : MonoBehaviour
{
    public float fallSpeed;  // �������� �ӵ�
    public bool isFalling = false;  // ���� �������� �ִ��� Ȯ��

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ���� �������� �ִ� �����̰� y ��ġ�� 0���� Ŭ ��� ������
        if (isFalling == true && transform.position.y > 0)
        {
            transform.Translate(0, -fallSpeed * Time.deltaTime, 0);
            if (transform.position.y < 0) // y�� 0���� �۾����� ����
            {
                Vector3 correctedPosition = transform.position;
                correctedPosition.y = 0; // y ���� 0���� ����
                transform.position = correctedPosition;
            }
        }
    }

    // �浹�� ���۵� �� ȣ��Ǵ� �޼���
    void OnCollisionEnter2D(Collision2D collision)
    {
        // �÷��̾�� �浹���� �� (Player�� "Player"��� �±׸� ������ �ִٰ� ����)
        if (collision.gameObject.CompareTag("Player"))
        {
            isFalling = true;  // ���� �������� �����ϵ��� ����
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public LayerMask crashMask; // �浹 ����ũ
    private bool isFire = false; // ���̾�� �߻�Ǿ����� ����

    public float speed = 5.0f;
    public float damage = 10.0f;
    private Vector2 moveDirection = Vector2.right; // �⺻ �̵� ����
    void Update()
    {
        if (isFire)
        {
            // ���̾ �̵� ó��
            float dist = speed * Time.deltaTime; // �̵� �Ÿ� ���
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, dist, crashMask);

            if (hit.collider != null) // �浹�� ������Ʈ�� �ִ� ���
            {
                DestroyObject(hit.point); // �浹 ��ġ���� ������Ʈ ó��
                return; // �� �̻� �̵����� ����
            }

            transform.Translate(moveDirection * dist); // ���� �������� �̵�
        }
    }
    public void OnFire(Vector2 direction)
    {
        isFire = true;
        transform.parent = null;
        moveDirection = direction.normalized; // ���� ���� �� ����ȭ
    }

    void DestroyObject(Vector2 pos)
    {
        // ���� ������Ʈ ����
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // �浹 ��󿡼� IDamage �������̽� Ȯ��
            IDamage target = collision.GetComponent<IDamage>();
            if (target != null)
            {
                target.OnDamage(damage); // ������ ����
            }

            // FireBall �ı�
            Destroy(gameObject);
        }
    }
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    // �浹 ���� �� ó��
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    // �浹 ���� �� ó��
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public LayerMask crashMask; // �浹 ����ũ

    void Update()
    {
        // ���̾ �̵�
        float dist = 5.0f * Time.deltaTime;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, dist, crashMask);
        if (hit.collider != null)
        {
            DestroyObject(hit.point); // �浹 ��ġ���� ���� ó��
        }
        transform.Translate(Vector2.right * dist);
    }

    void DestroyObject(Vector2 pos)
    {
/*        // ���� ȿ�� ����
        GameObject obj = Instantiate(orgEffect);
        obj.transform.position = pos;
*/
        // ���� ������Ʈ ����
        Destroy(gameObject);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        // �浹 ���� �� ó��
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �浹 ���� �� ó��
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Razer : RazerPoint
{
    public LayerMask crashMask; // �浹 ����ũ
    private bool isRazer = false;
    public float speed = 10.0f;
    public float damage = 10.0f;

    private Vector2 moveDirection = Vector2.right;
    public float maxDistance = 20f;
    private float traveledDistance = 0f;

    private bool isDestroying = false; // �ı� ���� �÷���

    void Update()
    {
        if (isRazer && !isDestroying) //�ı����� �ƴϸ� �̵�
        {
            float dist = speed * Time.deltaTime; // �̵� �Ÿ� ���
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, dist, crashMask);
            if (hit.collider != null && hit.collider.gameObject.layer
                == LayerMask.NameToLayer("Player"))
            {
                myAnim.SetBool(animData.IsDestorying, true);
                isDestroying = true; // �ı� ���·� ��ȯ
                // �浹 ��󿡼� IDamage �������̽� Ȯ��
                IDamage target = hit.collider.GetComponent<IDamage>();
                if (target != null)
                {
                    target.OnDamage(damage); // ������ ����
                }
                // DestroyObject(hit.point); // �浹 ��ġ���� ������Ʈ ó��
                StartCoroutine(DelayDestroyObject());
                return; // �� �̻� �̵����� ����
            }

            transform.Translate(moveDirection * dist); // ���� �������� �̵�
            traveledDistance += dist; // �̵��� �Ÿ� ����
            if (traveledDistance >= maxDistance)
            {
                Destroy(gameObject);
                return; // �� �̻� �̵����� ����
            }
        }
    }
    public void OnRazer(Vector2 direction)
    {
        isRazer = true;
        transform.parent = null;
        moveDirection = direction.normalized; // ���� ���� �� ����ȭ
    }
    void DestroyObject()
    {
        Destroy(gameObject);
    }
    IEnumerator DelayDestroyObject()
    {
        yield return new WaitForSeconds(0.3f); // 0.5�� ���
        DestroyObject(); // ������Ʈ ����
    }
}
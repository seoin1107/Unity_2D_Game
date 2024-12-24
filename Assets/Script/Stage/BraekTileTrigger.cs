using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTileTrigger : MonoBehaviour
{
    public GameObject targetObject;
    public float moveSpeed = 1f; // ���� �̵��ϴ� �ӵ�
    public float destroyDelay = 5f; // �����Ǳ������ �ð�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(MoveUp(targetObject)); // �ڷ�ƾ ����
        }
    }

    private IEnumerator MoveUp(GameObject obj)
    {
        float elapsedTime = 0f; // ��� �ð� ����

        while (elapsedTime < destroyDelay)
        {
            // ������Ʈ�� ���� �̵�
            obj.transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime; // �ð� ����
            yield return null; // ���� �����ӱ��� ���
        }
    }
}

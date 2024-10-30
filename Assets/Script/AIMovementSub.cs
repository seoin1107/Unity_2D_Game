using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIMovementSub : Movement
{
    private Coroutine aiMove;
    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMove(Vector2 pos, UnityAction act)
    {
        // ��ǥ ��ġ������ ������ ����ϰ� �Ÿ� ����
        Vector2 dir = pos - (Vector2)transform.position;
        if (dir.magnitude < 0.1f) // ��ǥ ������ ����� ���
        {
            act?.Invoke(); // �Ϸ� �׼� ȣ��
            return;
        }

        if (aiMove != null) StopCoroutine(aiMove); // ���� �̵� ����
        aiMove = StartCoroutine(MovingTowards(pos, act)); // ���ο� �̵� ����
    }

    private IEnumerator MovingTowards(Vector2 target, UnityAction act)
    {
        while (Vector2.Distance(transform.position, target) > 0.1f) // ��ǥ ������ ������ ������ �ݺ�
        {
            Vector2 dir = (target - (Vector2)transform.position).normalized; // ���� ����ȭ
            float delta = Time.deltaTime * moveSpeed; // �̵� �Ÿ� ���

            // Rigidbody2D�� �̵�
            rb2D.MovePosition(rb2D.position + dir * delta);

            // �ִϸ��̼� ���� ������Ʈ
            if (myAnim != null) // myAnim�� null�� �ƴ� ��츸
            {
                myAnim.SetBool(animData.IsMove, true);
            }

            yield return null; // ���� �����ӱ��� ���
        }

        if (myAnim != null)
        {
            myAnim.SetBool(animData.IsMove, false); // �̵� �Ϸ� �� �ִϸ��̼� ����
        }
        act?.Invoke(); // �Ϸ� �׼� ȣ��
        aiMove = null; // ���� �̵� �ڷ�ƾ ���� ����
    }

    protected new void OnFollow(Transform target)
    {
        if (aiMove != null) StopCoroutine(aiMove); // ���� �̵� ����
        aiMove = StartCoroutine(Following(target)); // ���ο� ���� ����
    }

    private IEnumerator Following(Transform target)
    {
        while (target != null)
        {
            Vector2 dir = target.position - transform.position; // ��ǥ ��ġ�� ���� ��ġ ���� ���� ����
            if (dir.magnitude > 0.1f) // ��ǥ ������ ����� ���
            {
                dir.Normalize(); // ���� ����ȭ
                float delta = Time.deltaTime * moveSpeed; // �̵� �Ÿ� ���
                rb2D.MovePosition(rb2D.position + dir * delta); // Rigidbody2D�� ���� �̵�

                if (myAnim != null)
                {
                    myAnim.SetBool(animData.IsMove, true); // �̵� �ִϸ��̼� ����
                }
            }
            else
            {
                if (myAnim != null)
                {
                    myAnim.SetBool(animData.IsMove, false); // �̵� �ִϸ��̼� ����
                }
            }

            yield return null; // ���� �����ӱ��� ���
        }
        if (myAnim != null)
        {
            myAnim.SetBool(animData.IsMove, false); // ���� ���� �� �ִϸ��̼� ����
        }
        aiMove = null; // ���� ���� �ڷ�ƾ ���� ����
    }
}

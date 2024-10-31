using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIMovement : Movement
{
    private Coroutine aiMove;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // �ʿ信 ���� ������Ʈ ���� �߰�
    }

    public void OnMove(Vector2 pos)
    {
        OnMove(pos, null);
    }

    public void OnMove(Vector2 pos, UnityAction act)
    {
        // ���� ��ġ���� ��ǥ ��ġ������ ��θ� ����ϰ�, ��θ� �����մϴ�.
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

            // �̵�
            transform.Translate(dir * delta, Space.World);

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
                transform.Translate(dir * delta, Space.World); // �̵�

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

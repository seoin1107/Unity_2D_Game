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
    public void OnMove(Vector2 pos, UnityAction act)
    {
        if (aiMove != null) StopCoroutine(aiMove); // ���� �̵� ����
        aiMove = StartCoroutine(MovingTowards(pos, act)); // ���ο� �̵� ����
    }

    private IEnumerator MovingTowards(Vector2 target, UnityAction act)
    {
        while (Vector2.Distance(transform.position, target) > 0.1f) // ��ǥ ������ ������ ������ �ݺ�
        {

            Vector2 dir = (target - (Vector2)transform.position).normalized; // ���� ����ȭ
            dir.y = 0.0f;
            float delta = Time.deltaTime * moveSpeed; // �̵� �Ÿ� ���

            // �̵�
            transform.Translate(dir * delta);

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
    protected void OnFollow(Transform target)
    {
        if (aiMove != null) StopCoroutine(aiMove); // ���� �̵� ����
        aiMove = StartCoroutine(Following(target)); // ���ο� ���� ����
    }

    private IEnumerator Following(Transform target)
    {
        while (target != null)
        {
            Vector2 dir = target.position - transform.position; // ��ǥ ��ġ�� ���� ��ġ ���� ���� ����
            dir.y = 0.0f;

            if (dir.magnitude > 1.0f) // ��ǥ ������ ����� ���
            {
                dir.Normalize(); // ���� ����ȭ
                float delta = Time.deltaTime * moveSpeed; // �̵� �Ÿ� ���
                transform.Translate(dir * delta); // �̵�

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class middleBoss2D : BattleSystem2D
{
    Transform myTarget;
    Vector2 originalPosition; // ������ ���� ��ġ�� ������ ����
    public CharacterStatus monsterStatus;
    public GameObject deletGround;

    public GameObject orgEnergyBall;
    public Transform myEnergyBall;

    public enum State
    {
        Create, Normal, Battle, Dead
    }
    public State myState = State.Create;
    //float maxDist = 0.0f;
    Transform curGround = null;
    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;

        switch (myState)
        {
            case State.Normal:
                if (curGround != null)
                {
                    OnCheckGround(curGround);
                }
                break;
            case State.Battle:
                break;
            case State.Dead:
                StopAllCoroutines();
                myRigid.gravityScale = 0.0f;
                myRigid.velocity = Vector2.zero;
                moveDir = Vector2.zero;
                gameObject.layer = default;
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case State.Normal:
                myAnim.SetBool("IsAir", false);
                if (!myAnim.GetBool("IsAir"))
                {
                    if (Mathf.Abs(transform.position.x - originalPosition.x) > 0.1f)
                    {
                        // X�� ���⸸ ��� (y ���� ����)
                        Vector2 direction = new Vector2(originalPosition.x - transform.position.x, 0).normalized;
                        moveDir = direction;
                    }
                    else
                    {
                        moveDir = Vector2.zero;
                    }

                }
                break;
            case State.Battle:
                playTime += Time.deltaTime;
                // Ÿ���� �����ʿ� ������ �������� �ٶ󺸰�, ���ʿ� ������ ������ �ٶ󺸵��� ����
                if (myTarget != null)
                {
                    if (myTarget.position.x > transform.position.x)
                    {
                        // Ÿ���� �����ʿ� ������ ���Ͱ� �������� �ٶ�
                        transform.localScale = new Vector3(1.0f, transform.localScale.y, transform.localScale.z);
                    }
                    else if (myTarget.position.x < transform.position.x)
                    {
                        // Ÿ���� ���ʿ� ������ ���Ͱ� ������ �ٶ�
                        transform.localScale = new Vector3(-1.0f, transform.localScale.y, transform.localScale.z);
                    }
                }
/*                moveDir.x = myTarget.position.x > transform.position.x ? 1.0f :
                    myTarget.position.x < transform.position.x ? -1.0f : 0.0f;*/

                if (Vector2.Distance(myTarget.position, transform.position) <= monsterStatus.characterStat.attackRange)
                {
                    moveDir.x = 0.0f;
                    if (playTime >= monsterStatus.characterStat.atkSpeed)
                    {
                        playTime = 0.0f;
                        myAnim.SetTrigger(animData.OnAttack);
                    }
                }
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position; // ���� ��ġ�� ����
        ChangeState(State.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
        base.OnUpdate();
    }

    protected override void OnCheckGround(Transform tr)
    {
        if (tr == null)
        {
            Debug.LogWarning("Ground transform is null.");
            return;
        }
    }

    public void OnFindTarget(Transform tr)
    {
        if (myState == State.Dead) return;
        if (tr.GetComponent<ILive>().IsLive)
        {
            myTarget = tr;
            myTarget.GetComponent<IDeathAlarm>().deathAlarm += () => ChangeState(State.Normal);
            ChangeState(State.Battle);
        }
    }

    public void OnLostTarget()
    {
        if (myState == State.Dead) return;
        myTarget = null;
        ChangeState(State.Normal);
    }
    public void OnAttack()
    {
        if (myTarget == null) return; //Ÿ�����־�� ����
        //���̾����
        GameObject obj = Instantiate(orgEnergyBall, myEnergyBall.position, Quaternion.identity);
        EnergyBall energyBall = obj.GetComponent<EnergyBall>();
        if (energyBall != null)
        {
            // ������ ���⿡ ���� ���̾�� �̵� ���� ����
            Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            energyBall.OnFire(direction); // �߻�
            energyBall.damage = monsterStatus.characterStat.totalAtk; // ������ ����
        }
    }

    protected override void OnDead()
    {
        base.OnDead();
        ChangeState(State.Dead);
        OnDisApear();
    }

    public void OnDisApear()
    {
        StartCoroutine(DisApearing());
    }
    IEnumerator DisApearing()
    {
        yield return new WaitForSeconds(3.0f);
        float duration = 3.0f; // 3�� ���� �̵�
        float elapsedTime = 0f; // ��� �ð� �ʱ�ȭ
        float moveSpeed = 1.0f; // �ʴ� �̵� �ӵ�
                                // 3�� ���� y������ �̵�
        while (elapsedTime < duration)
        {
            if (deletGround != null)
            {
                Vector3 position = deletGround.transform.position;
                position.y += moveSpeed * Time.deltaTime; // y������ �̵�
                deletGround.transform.position = position;
            }

            elapsedTime += Time.deltaTime; // ��� �ð� ����
            yield return null; // ���� �����ӱ��� ���
        }

        Color color = myRenderer.color;
        while (color.a > 0.0f)
        {
            color.a -= Time.deltaTime;
            myRenderer.color = color;
            yield return null;
        }
        Destroy(gameObject);

    }
}

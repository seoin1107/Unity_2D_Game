using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonster2D : BattleSystem2D
{
    Transform myTarget;
    public CharacterStatus monsterStatus;

    public GameObject orgFireBall;
    public Transform myFirePoint;
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
                    moveDir = Vector2.zero;
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

                // Ÿ�ٰ��� �Ÿ� ���
                if (Vector2.Distance(myTarget.position, transform.position) <= monsterStatus.characterStat.attackRange)
                {
                    moveDir.x = 0.0f; // ���� ���� �������� �̵����� ����

                    // ���� �غ� �� ����
                    if (playTime >= monsterStatus.characterStat.atkSpeed)
                    {
                        playTime = 0.0f;
                        myAnim.SetTrigger(animData.OnAttack); // ���� �ִϸ��̼� Ʈ����
                    }
                }
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
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
        GameObject obj = Instantiate(orgFireBall, myFirePoint.position, Quaternion.identity);
        FireBall fireBall = obj.GetComponent<FireBall>();
        if (fireBall != null)
        {
            // ������ ���⿡ ���� ���̾�� �̵� ���� ����
            Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            fireBall.OnFire(direction); // �߻�
            fireBall.damage = monsterStatus.characterStat.totalAtk; // ������ ����
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
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}
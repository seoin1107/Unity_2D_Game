using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyMonster2D : BattleSystem2D
{
    Transform myTarget;
    Vector2 originalPosition; // 몬스터의 원래 위치를 저장할 변수
    public CharacterStatus monsterStatus;
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
                myRigid.gravityScale = 0.0f;
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
                    if (Vector2.Distance(transform.position, originalPosition) > 0.1f)
                    {
                        // X축과 Y축을 모두 포함한 방향 계산
                        Vector2 direction = (originalPosition - (Vector2)transform.position).normalized;
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
                Vector2 targetDirection = (myTarget.position - transform.position).normalized;
                moveDir = targetDirection;

                if (Vector2.Distance(myTarget.position, transform.position) <= monsterStatus.characterStat.attackRange)
                {
                    moveDir = Vector2.zero; // 멈춤

                    if (playTime >= monsterStatus.characterStat.atkSpeed)
                    {
                        playTime = 0.0f;
                        myAnim.SetTrigger(animData.OnAttack); // 공격 애니메이션 실행
                    }
                }
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position; // 시작 위치를 저장
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
        /*        curGround = tr;
                float halfDist = tr.localScale.x * 0.5f; // 발판의절반거리
                float dist = tr.position.x - transform.position.x;
                if (moveDir.x < 0.0f)
                {
                    maxDist = halfDist - dist;
                }
                else
                {
                    maxDist = halfDist + dist;
                }*/
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
        myTarget.GetComponent<IDamage>()?.OnDamage(monsterStatus.characterStat.totalAtk);
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

        Color color = myRenderer.color;
        while (color.a > 0.0f)
        {
            color.a -= Time.deltaTime;
            myRenderer.color = color;
            transform.Translate(Vector2.up * Time.deltaTime * 0.3f);
            yield return null;
        }
        Destroy(gameObject);
    }
}
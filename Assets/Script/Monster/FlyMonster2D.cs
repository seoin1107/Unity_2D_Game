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
                    // X축 방향만 계산 (y 값은 무시)
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
/*            if (myTarget == null)
            {
                Debug.LogWarning("myTarget이 null입니다.");
                ChangeState(State.Normal);
                return;
            }*/

            playTime += Time.deltaTime;
            moveDir.x = myTarget.position.x > transform.position.x ? 1.0f :
                myTarget.position.x < transform.position.x ? -1.0f : 0.0f;

            if (Vector2.Distance(myTarget.position, transform.position) <=  monsterStatus.characterStat.attackRange)
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
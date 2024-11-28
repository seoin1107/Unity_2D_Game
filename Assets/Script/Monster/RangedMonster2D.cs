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

                // 타겟이 오른쪽에 있으면 오른쪽을 바라보고, 왼쪽에 있으면 왼쪽을 바라보도록 설정
                if (myTarget != null)
                {
                    if (myTarget.position.x > transform.position.x)
                    {
                        // 타겟이 오른쪽에 있으면 몬스터가 오른쪽을 바라봄
                        transform.localScale = new Vector3(1.0f, transform.localScale.y, transform.localScale.z);
                    }
                    else if (myTarget.position.x < transform.position.x)
                    {
                        // 타겟이 왼쪽에 있으면 몬스터가 왼쪽을 바라봄
                        transform.localScale = new Vector3(-1.0f, transform.localScale.y, transform.localScale.z);
                    }
                }

                // 타겟과의 거리 계산
                if (Vector2.Distance(myTarget.position, transform.position) <= monsterStatus.characterStat.attackRange)
                {
                    moveDir.x = 0.0f; // 공격 범위 내에서는 이동하지 않음

                    // 공격 준비 및 실행
                    if (playTime >= monsterStatus.characterStat.atkSpeed)
                    {
                        playTime = 0.0f;
                        myAnim.SetTrigger(animData.OnAttack); // 공격 애니메이션 트리거
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
        if (myTarget == null) return; //타겟이있어야 공격
        //파이어볼생성
        GameObject obj = Instantiate(orgFireBall, myFirePoint.position, Quaternion.identity);
        FireBall fireBall = obj.GetComponent<FireBall>();
        if (fireBall != null)
        {
            // 몬스터의 방향에 따라 파이어볼의 이동 방향 설정
            Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            fireBall.OnFire(direction); // 발사
            fireBall.damage = monsterStatus.characterStat.totalAtk; // 데미지 설정
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
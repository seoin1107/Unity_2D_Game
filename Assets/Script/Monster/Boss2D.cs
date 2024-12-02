using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2D : BattleSystem2D
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

                        //랜덤기믹
                        int randomAction = Random.Range(0, 4);
                        if(randomAction == 0) // 일반공격패턴
                        {
                            myAnim.SetTrigger(animData.OnAttack); 
                        }
                        if(randomAction == 1) // 점프 위치변경 패턴
                        {
                            if(!myAnim.GetBool("IsAir"))
                            {
                                StopAllCoroutines();
                                StartCoroutine(Jumping());
                            }
                        }
                        if(randomAction == 2) // 구울 소환
                        {
                                                       
                        }
                    }
                }
                break;
        }
    }
    IEnumerator Jumping()
    {
        myRigid.AddForce(Vector2.up * 400.0f);
        yield return new WaitForFixedUpdate();

        myColider.isTrigger = true;
        while (myRigid.velocity.y >= 0.0f) //위로올라가는중
        {
            yield return null;
        }
        myColider.isTrigger = false;

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

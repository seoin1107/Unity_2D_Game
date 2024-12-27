using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class middleBoss2D : BattleSystem2D
{
    Transform myTarget;
    Vector2 originalPosition; // 몬스터의 원래 위치를 저장할 변수
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
        if (myTarget == null) return; //타겟이있어야 공격
        //파이어볼생성
        GameObject obj = Instantiate(orgEnergyBall, myEnergyBall.position, Quaternion.identity);
        EnergyBall energyBall = obj.GetComponent<EnergyBall>();
        if (energyBall != null)
        {
            // 몬스터의 방향에 따라 파이어볼의 이동 방향 설정
            Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            energyBall.OnFire(direction); // 발사
            energyBall.damage = monsterStatus.characterStat.totalAtk; // 데미지 설정
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
        float duration = 3.0f; // 3초 동안 이동
        float elapsedTime = 0f; // 경과 시간 초기화
        float moveSpeed = 1.0f; // 초당 이동 속도
                                // 3초 동안 y축으로 이동
        while (elapsedTime < duration)
        {
            if (deletGround != null)
            {
                Vector3 position = deletGround.transform.position;
                position.y += moveSpeed * Time.deltaTime; // y축으로 이동
                deletGround.transform.position = position;
            }

            elapsedTime += Time.deltaTime; // 경과 시간 누적
            yield return null; // 다음 프레임까지 대기
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

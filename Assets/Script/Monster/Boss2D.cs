using System.Collections;
using UnityEngine;

public class Boss2D : BattleSystem2D
{
    Transform myTarget;
    Vector2 originalPosition; // 몬스터의 원래 위치를 저장할 변수
    public CharacterStatus monsterStatus;

    public GameObject GhoulPrefab;      // 구울 프리팹
    public Transform summonPoint;       // 구울 소환위치
    public GameObject DeathBringer;     // 수호자 프리팹
    public Transform DBPoint;          // 수호자 소환위치      
    public GameObject LightingPrefab;   // 번개 프리팹
    public Transform effectPoint;       // 번개 생성위치
    public Transform tpPos1;             // 순간이동위치
    public Transform tpPos2;
    public Transform HpPos;             //체력 50퍼 되면 순간이동위치

    public GameObject RazerPoint1;
    public GameObject RazerPoint2;
    public GameObject RazerPoint3;
    public GameObject RazerPoint4;

    private bool IsHealing = false;
    private bool hasHealedOnce = false; // 힐링이 한 번 진행되었는지 여부
    public float healingAmount = 5f; // 초당 회복량
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
                        int randomAction = Random.Range(0,5);
                        if (isTeleporting != true)
                        {
                            if (randomAction == 0) // 일반공격패턴
                            {
                                myAnim.SetTrigger(animData.OnAttack);
                            }
                            if (randomAction == 1) // 일반공격패턴 + 일반공격을 좀더 자주하도록
                            {
                                myAnim.SetTrigger(animData.OnAttack);
                            }
                            if (randomAction == 2) // 구울소환
                            {
                                SummonGhoul();
                            }
                            if (randomAction == 3)
                            {
                                //장판 아래서 위로 쏘는 불기둥?
                                EffectLighting();
                            }
                        }
                        if (randomAction == 4)
                        {
                            StartCoroutine(TP());
                        }
                        // 추가로 체력이 30퍼 가되면 플레이어가 닿을수없는 위치로 이동해서 천천히 체력회복
                        if(monsterStatus.characterStat.curHP < 50 && !hasHealedOnce)
                        {
                            StartCoroutine(healing());
                        }
                        // 중간에 망령스프라이트 생성하여 처치시, 보스가 밟고있는 지반 스프라이트 제거후 약간의 프리딜타임 후, 일반배틀상태로 변경
                    }
                }
                break;
        }
    }
    void SummonGhoul()
    {
        if(GhoulPrefab != null && summonPoint != null)
        {
            //구울소환
            Instantiate(GhoulPrefab, summonPoint.position, Quaternion.identity);
        }
    }

    void SummonDeathBringer()
    {
        if (DeathBringer != null && DBPoint != null)
        {
            //수호자소환
            Instantiate(DeathBringer, DBPoint.position, Quaternion.identity);
        }
    }
    void EffectLighting()
    {
        if(LightingPrefab != null && effectPoint != null)
        {
            //번개소환
            Instantiate(LightingPrefab, effectPoint.position, Quaternion.identity);
        }
    }
    private bool isTeleporting = false; // 순간 이동 중인지 확인
    IEnumerator TP()
    {
        // 다른 동작이 중단되도록 설정
        isTeleporting = true;
        // 1. 페이드 아웃 (투명해짐)
        yield return new WaitForSeconds(1.0f);
        Color color = myRenderer.color;
        while (color.a > 0.0f)
        {
            color.a -= Time.deltaTime;
            myRenderer.color = color;
            yield return null;
        }
        // 2. 타겟 위치로 이동
        if (myTarget != null)
        {
            Transform chosenPosition = Random.Range(0, 2) == 0 ? tpPos1 : tpPos2;
            transform.position = chosenPosition.position; // 선택된 위치로 이동
            yield return new WaitForSeconds(1.5f);
        }
        // 3. 페이드 인 (색 복구)
        while (color.a < 1.0f)
        {
            color.a += Time.deltaTime; // 알파값 증가
            myRenderer.color = color;  // 색상 적용
            yield return null;         // 다음 프레임 대기
        }
        // 다른 동작이 다시 가능하도록 설정
        isTeleporting = false;
    }

    IEnumerator healing()
    {
        // 이미 힐링이 진행되었거나 한 번 힐링한 적이 있으면 종료
        if (IsHealing || hasHealedOnce)
        {
            yield break;
        }

        IsHealing = true; // 힐링 상태로 변경
        hasHealedOnce = true; // 한 번 힐링한 것으로 설정
        //이동위치 설정 추가할것.
        if (myTarget != null)
        {
            // 다른 동작이 중단되도록 설정
            isTeleporting = true;
            // 1. 페이드 아웃 (투명해짐)
            yield return new WaitForSeconds(1.0f);
            Color color = myRenderer.color;
            while (color.a > 0.0f)
            {
                color.a -= Time.deltaTime;
                myRenderer.color = color;
                yield return null;
            }
            // 2. 타겟 위치로 이동
            if (myTarget != null)
            {
                transform.position = HpPos.position; // 선택된 위치로 이동
                yield return new WaitForSeconds(1.5f);
                SummonDeathBringer();
            }
            // 3. 페이드 인 (색 복구)
            while (color.a < 1.0f)
            {
                color.a += Time.deltaTime; // 알파값 증가
                myRenderer.color = color;  // 색상 적용
                yield return null;         // 다음 프레임 대기
            }
            // 다른 동작이 다시 가능하도록 설정
            isTeleporting = false;
        }
        GameObject deletGround = GameObject.Find("deletGround");
        while (monsterStatus.characterStat.curHP < monsterStatus.characterStat.maxHP)
        {
            if(deletGround == null)
            {
                break;
            }
            monsterStatus.characterStat.curHP += healingAmount;
            monsterStatus.characterStat.curHP =
                Mathf.Min(monsterStatus.characterStat.curHP, monsterStatus.characterStat.maxHP);
            yield return new WaitForSeconds(1.5f);
        }
        IsHealing = false;
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
        StartCoroutine(OpenWall());
    }
    IEnumerator OpenWall()
    {
        // BattleWall 오브젝트 삭제
        GameObject battleWall = GameObject.Find("BattleWall");
        GameObject battleWall2 = GameObject.Find("BattleWall2");

        if (battleWall != null && battleWall2 != null)
        {
            Destroy(battleWall);
            Destroy(battleWall2);
        }
        yield return new WaitForSeconds(3.0f);

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
        if (RazerPoint1 != null && RazerPoint2 != null && RazerPoint3 != null && RazerPoint4 != null)
        {
            Destroy(RazerPoint1);
            Destroy(RazerPoint2);
            Destroy(RazerPoint3);
            Destroy(RazerPoint4);
        }
    }
}

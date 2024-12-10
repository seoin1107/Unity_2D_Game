using System.Collections;
using UnityEngine;

public class Boss2D : BattleSystem2D
{
    Transform myTarget;
    Vector2 originalPosition; // ������ ���� ��ġ�� ������ ����
    public CharacterStatus monsterStatus;

    public GameObject GhoulPrefab;      // ���� ������
    public Transform summonPoint;       // ���� ��ȯ��ġ
    public GameObject DeathBringer;     // ��ȣ�� ������
    public Transform DBPoint;          // ��ȣ�� ��ȯ��ġ      
    public GameObject LightingPrefab;   // ���� ������
    public Transform effectPoint;       // ���� ������ġ
    public Transform tpPos1;             // �����̵���ġ
    public Transform tpPos2;
    public Transform HpPos;             //ü�� 50�� �Ǹ� �����̵���ġ
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

                        //�������
                        int randomAction = Random.Range(0,5);
                        if (isTeleporting != true)
                        {
                            if (randomAction == 0) // �Ϲݰ�������
                            {
                                myAnim.SetTrigger(animData.OnAttack);
                            }
                            if (randomAction == 1) // �Ϲݰ������� + �Ϲݰ����� ���� �����ϵ���
                            {
                                myAnim.SetTrigger(animData.OnAttack);
                            }
                            if (randomAction == 2) // �����ȯ
                            {
                                SummonGhoul();
                            }
                            if (randomAction == 3)
                            {
                                //���� �Ʒ��� ���� ��� �ұ��?
                                EffectLighting();
                            }
                        }
                        if (randomAction == 4)
                        {
                            StartCoroutine(TP());
                        }
                        // �߰��� ü���� 30�� ���Ǹ� �÷��̾ ���������� ��ġ�� �̵��ؼ� õõ�� ü��ȸ��
                        if(monsterStatus.characterStat.curHP < 50)
                        {
                            StartCoroutine(healing());
                        }
                        // �߰��� ���ɽ�������Ʈ �����Ͽ� óġ��, ������ ����ִ� ���� ��������Ʈ ������ �ణ�� ������Ÿ�� ��, �Ϲݹ�Ʋ���·� ����
                    }
                }
                break;
        }
    }
    void SummonGhoul()
    {
        if(GhoulPrefab != null && summonPoint != null)
        {
            //�����ȯ
            Instantiate(GhoulPrefab, summonPoint.position, Quaternion.identity);
        }
    }

    void SummonDeathBringer()
    {
        if (DeathBringer != null && DBPoint != null)
        {
            //��ȣ�ڼ�ȯ
            Instantiate(DeathBringer, DBPoint.position, Quaternion.identity);
        }
    }
    void EffectLighting()
    {
        if(LightingPrefab != null && effectPoint != null)
        {
            //������ȯ
            Instantiate(LightingPrefab, effectPoint.position, Quaternion.identity);
        }
    }
    private bool isTeleporting = false; // ���� �̵� ������ Ȯ��
    IEnumerator TP()
    {
        // �ٸ� ������ �ߴܵǵ��� ����
        isTeleporting = true;
        // 1. ���̵� �ƿ� (��������)
        yield return new WaitForSeconds(1.0f);
        Color color = myRenderer.color;
        while (color.a > 0.0f)
        {
            color.a -= Time.deltaTime;
            myRenderer.color = color;
            yield return null;
        }
        // 2. Ÿ�� ��ġ�� �̵�
        if (myTarget != null)
        {
            Transform chosenPosition = Random.Range(0, 2) == 0 ? tpPos1 : tpPos2;
            transform.position = chosenPosition.position; // ���õ� ��ġ�� �̵�
            yield return new WaitForSeconds(1.5f);
        }
        // 3. ���̵� �� (�� ����)
        while (color.a < 1.0f)
        {
            color.a += Time.deltaTime; // ���İ� ����
            myRenderer.color = color;  // ���� ����
            yield return null;         // ���� ������ ���
        }
        // �ٸ� ������ �ٽ� �����ϵ��� ����
        isTeleporting = false;
    }

    private bool IsHealing = false;
    public float healingAmount = 5f; // �ʴ� ȸ����
    IEnumerator healing()
    {
        //�̵���ġ ���� �߰��Ұ�.
        if(myTarget != null)
        {
            // �ٸ� ������ �ߴܵǵ��� ����
            isTeleporting = true;
            // 1. ���̵� �ƿ� (��������)
            yield return new WaitForSeconds(1.0f);
            Color color = myRenderer.color;
            while (color.a > 0.0f)
            {
                color.a -= Time.deltaTime;
                myRenderer.color = color;
                yield return null;
            }
            // 2. Ÿ�� ��ġ�� �̵�
            if (myTarget != null)
            {
                transform.position = HpPos.position; // ���õ� ��ġ�� �̵�
                yield return new WaitForSeconds(1.5f);
                SummonDeathBringer();
            }
            // 3. ���̵� �� (�� ����)
            while (color.a < 1.0f)
            {
                color.a += Time.deltaTime; // ���İ� ����
                myRenderer.color = color;  // ���� ����
                yield return null;         // ���� ������ ���
            }
            // �ٸ� ������ �ٽ� �����ϵ��� ����
            isTeleporting = false;
        }

        IsHealing = true;
        while (monsterStatus.characterStat.curHP < monsterStatus.characterStat.maxHP)
        {
            monsterStatus.characterStat.curHP += healingAmount;
            monsterStatus.characterStat.curHP =
                Mathf.Min(monsterStatus.characterStat.curHP, monsterStatus.characterStat.maxHP);
            yield return new WaitForSeconds(1.0f);
        }
        IsHealing = false;
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

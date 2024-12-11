using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_real2D : BattleSystem2D
{
    Transform myTarget;
    public CharacterStatus monsterStatus;
    public GameObject LightingPrefab;   // ���� ������
    public Transform effectPoint1;       // ���� ������ġ
    public Transform effectPoint2;       // ���� ������ġ
    public Transform effectPoint3;       // ���� ������ġ
    public Transform effectPoint4;       // ���� ������ġ
    public Transform effectPoint5;       // ���� ������ġ

    public GameObject DB_HardPrefab;
    public Transform DB_HardPoint;

    public enum State
    {
        Create, Normal, Battle, Dead
    }
    public State myState = State.Create;
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
                StartCoroutine(Energy());
                // StartCoroutine(TimeOver());
                break;
            case State.Battle:
                break;
        }
    }
    IEnumerator TimeOver()
    {
        yield return new WaitForSeconds(7.0f);
        myAnim.SetTrigger(animData.OnDead);
        Color color = myRenderer.color;
        while (color.a > 0.0f)
        {
            color.a -= Time.deltaTime;
            myRenderer.color = color;
            yield return null;
        }
        Destroy(gameObject);
    }

    void SummonDB_Hard()
    {
        if (DB_HardPrefab != null && DB_HardPoint != null)
        {
            //�ϵ��ȣ�� ��ȯ
            Instantiate(DB_HardPrefab, DB_HardPoint.position, Quaternion.identity);
        }
    }
    IEnumerator Energy()
    {
        yield return new WaitForSeconds(2.0f);
        myAnim.SetBool("IsEnergy", false);
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
        EffectLighting1();
        EffectLighting2();
        EffectLighting3();
        EffectLighting4();
        EffectLighting5();
        StartCoroutine(DisApearing());
        SummonDB_Hard();
    }
    IEnumerator DisApearing()
    {
        yield return new WaitForSeconds(0.6f);

        Color color = myRenderer.color;
        while (color.a > 0.0f)
        {
            color.a -= Time.deltaTime;
            myRenderer.color = color;
            yield return null;
        }
        Destroy(gameObject);
    }
    void EffectLighting1()
    {
        if (LightingPrefab != null && effectPoint1 != null)
        {
            //������ȯ
            Instantiate(LightingPrefab, effectPoint1.position, Quaternion.identity);
        }
    }
    void EffectLighting2()
    {
        if (LightingPrefab != null && effectPoint2 != null)
        {
            //������ȯ
            Instantiate(LightingPrefab, effectPoint2.position, Quaternion.identity);
        }
    }
    void EffectLighting3()
    {
        if (LightingPrefab != null && effectPoint3 != null)
        {
            //������ȯ
            Instantiate(LightingPrefab, effectPoint3.position, Quaternion.identity);
        }
    }
    void EffectLighting4()
    {
        if (LightingPrefab != null && effectPoint4 != null)
        {
            //������ȯ
            Instantiate(LightingPrefab, effectPoint4.position, Quaternion.identity);
        }
    }
    void EffectLighting5()
    {
        if (LightingPrefab != null && effectPoint5 != null)
        {
            //������ȯ
            Instantiate(LightingPrefab, effectPoint5.position, Quaternion.identity);
        }
    }
}
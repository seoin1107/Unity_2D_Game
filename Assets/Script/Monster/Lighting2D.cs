using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting2D : BattleSystem2D
{
    Transform myTarget;
    public CharacterStatus monsterStatus;
    public enum State
    {
        Create, Normal, Battle, Dead
    }
    public State myState = State.Create;

    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;

        switch (myState)
        {
            case State.Normal:
                break;
            case State.Battle:
                StartCoroutine(DisApearingAfterDelay(0.7f));
                break;
            case State.Dead:
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case State.Normal:
                break;
            case State.Battle:
                playTime += Time.deltaTime;
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
        EFFECTManager.Instance.PlaySound(EFFECTManager.Instance.EffectAttack);
        ChangeState(State.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
        base.OnUpdate();
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
    public void OnAttack()
    {
        myTarget.GetComponent<IDamage>()?.OnDamage(monsterStatus.characterStat.totalAtk);
    }

    public void OnDisApear()
    {
        StartCoroutine(DisApearing());
    }
    IEnumerator DisApearing()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }
    IEnumerator DisApearingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (myState == State.Battle) // 삭제 전 상태 확인
        {
            Destroy(gameObject);
        }
    }
}
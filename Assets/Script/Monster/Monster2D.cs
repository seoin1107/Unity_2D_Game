using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster2D : BattleSystem2D
{
    Transform myTarget;
    public enum State
    {
        Create, Normal, Battle, Dead
    }
    public State myState = State.Create;
    float maxDist = 0.0f;
    Transform curGround = null;
    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;

        switch (myState)
        {
            case State.Normal:
                if (Random.Range(0, 2) == 0)
                {
                    moveDir.x = 1.0f;
                }
                else
                {
                    moveDir.x = -1.0f;
                }
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
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case State.Normal:
                myAnim.SetBool("IsAir", false);
                maxDist -= deltaDist;
                if (!myAnim.GetBool("IsAir") && maxDist <= 0.0f)
                {
                    moveDir *= -1.0f;
                    myRenderer.flipX = !myRenderer.flipX;
                    OnCheckGround(curGround);
                }
                break;
            case State.Battle:
                playTime += Time.deltaTime;
                moveDir.x = myTarget.position.x > transform.position.x ? 1.0f :
                    myTarget.position.x < transform.position.x ? -1.0f : 0.0f;

                if (Vector2.Distance(myTarget.position, transform.position) <= battleStat.AttackRange)
                {
                    moveDir.x = 0.0f;
                    if (playTime >= battleStat.AttackDelay)
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
        curGround = tr;
        float halfDist = tr.localScale.x * 0.5f; // 발판의절반거리
        float dist = tr.position.x - transform.position.x;
        if (moveDir.x < 0.0f)
        {
            maxDist = halfDist - dist;
        }
        else
        {
            maxDist = halfDist + dist;
        }
    }

    public void OnFindTarget(Transform tr)
    {
        if (myState == State.Dead) return;
        myTarget = tr;
        ChangeState(State.Battle);
    }

    public void OnLostTarget()
    {
        if (myState == State.Dead) return;
        myTarget = null;
        ChangeState(State.Normal);
    }
    public void OnAttack()
    {
        myTarget.GetComponent<IDamage>()?.OnDamage(battleStat.AP);
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

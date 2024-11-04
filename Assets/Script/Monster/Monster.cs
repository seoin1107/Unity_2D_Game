using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Monster : AIMovement
{


    public enum State
    {
        Create, Normal, Roaming, Battle, Death
    }
    public State myState = State.Create;
    Vector2 createPos;

    Coroutine myCoro = null;

    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Normal:
                {
                    OnStop();
                    Vector2 dir = Vector2.right;
                    float dist = Random.Range(1.0f, 5.0f);
                    dir = dir * dist;
                    Vector2 randomPos = createPos + dir;
                    OnMove(randomPos, () => myCoro = StartCoroutine(DelayAction(Random.Range(1.0f, 3.0f), () => ChangeState(State.Normal))));
                    ChangeState(State.Roaming);
                }
                break;
            case State.Battle:
                OnStop();
                OnFollow(myTarget.transform);
                break;
            case State.Death:
                StopAllCoroutines();
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
                break;
        }
    }

    protected override void OnDead()
    {
        base.OnDead();
        ChangeState(State.Death);
    }

    private new void OnStop()
    {
        if (myCoro != null) StopCoroutine(myCoro);
        base.OnStop();
        myCoro = null;
    }

    IEnumerator DelayAction(float t, UnityEngine.Events.UnityAction act)
    {
        yield return new WaitForSeconds(t); // ������Ű��
        act?.Invoke();
    }

    public void OnBattle(GameObject target)
    {
        if (myState == State.Death) return;
        myTarget = target;
        ChangeState(State.Battle);

    }

    public void OnNormal()
    {
        if (myState == State.Death) return;
        myTarget = null;
        ChangeState(State.Normal);
    }

    // Start is called before the first frame update
    void Start()
    {
        OnReset();
        createPos = transform.position;
        ChangeState(State.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    public void OnDisApear()
    {
        StartCoroutine(DisApearing());
    }

    IEnumerator DisApearing()
    {
        yield return new WaitForSeconds(3.0f);
        float dist = 1.0f;
        while (dist > 0.0f)
        {
            float delta = 0.5f * Time.deltaTime;
            transform.Translate(Vector2.down * delta, Space.World);
            dist -= delta;
            yield return null;
        }
        Destroy(gameObject);
    }
}
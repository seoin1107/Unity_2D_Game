using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2D : Movement2D
{
    public enum State
    {
        Create, Normal, Battle
    }
    public State myState = State.Create;
    float maxDist = 0.0f;
    Transform curGround;
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
                break;
            case State.Battle:
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case State.Normal:
                maxDist -= deltaDist;
                if (!myAnim.GetBool("IsAir") && maxDist <= 0.0f)
                {
                    moveDir *= -1.0f;
                    myRenderer.flipX = !myRenderer.flipX;
                    OnCheckGround(curGround);
                }
                break;
            case State.Battle:
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
        curGround = tr;
        float halfDist = tr.localScale.x * 0.5f; // 발판의절반거리
        float dist = tr.position.x - transform.position.x;
        if (myRenderer.flipX)
        {
            maxDist = halfDist - dist;
        }
        else
        {
            maxDist = halfDist + dist;
        }
    }
}
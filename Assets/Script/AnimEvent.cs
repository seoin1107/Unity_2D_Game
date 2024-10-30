using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent attackAction;
    public UnityEvent deadAction;
    public UnityEvent skillAction;
    public UnityEvent comboAction;

    public void OnAttack()
    {
        attackAction?.Invoke();
    }
    public void OnDead()
    {
        deadAction?.Invoke();
    }
    public void OnSkill()
    {
        skillAction?.Invoke();
    }
    public void OncomboAttack1()
    {
        attackAction?.Invoke();
    }
    public void OncomboAttack2()
    {
        attackAction?.Invoke();
    }
}
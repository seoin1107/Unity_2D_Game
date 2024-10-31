using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AnimParameterData
{
    public int IsMove;
    public int OnAttack;
    public int IsAttack;
    public int OnDamage;
    public int OnDead;
    public int OnJump;
    public int OnSkill;
    public int IsSkill;
    public int OnDodge; //회피 동작 트리거
    public int IsDodge; // 회피 동작 구분 
    public int OncomboAttack1;
    public int OncomboAttack2;
    public int IscomboAttack1;
    public int IscomboAttack2;
    public void Initialize()
    {
        IsMove = Animator.StringToHash("IsMoving");
        OnAttack = Animator.StringToHash("OnAttack");
        IsAttack = Animator.StringToHash("IsAttack");
        OnDamage = Animator.StringToHash("OnDamage");
        OnDead = Animator.StringToHash("OnDead");
        OnJump = Animator.StringToHash("OnJump");
        OnSkill = Animator.StringToHash("OnSkill");
        IsSkill = Animator.StringToHash("IsSkill");
        OnDodge = Animator.StringToHash("OnDodge");
        IsDodge = Animator.StringToHash("IsDodge");
        OncomboAttack1 = Animator.StringToHash("OncomboAttack1");
        OncomboAttack2 = Animator.StringToHash("OncomboAttack2");
        IscomboAttack1 = Animator.StringToHash("IscomboAttack1");
        IscomboAttack2 = Animator.StringToHash("IscomboAttack2");
    }
}

public class AnimatorProperty : MonoBehaviour
{
    Animator _anim = null;
    protected AnimParameterData animData = new AnimParameterData();
    protected Animator myAnim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponentInChildren<Animator>();
                if (_anim != null)
                {
                    animData.Initialize(); // 초기화
                }
            }
            return _anim;
        }
    }

}


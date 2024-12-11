using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

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
    public int OnDodge; //ȸ�� ���� Ʈ����
    public int IsDodge; // ȸ�� ���� ���� 
    public int IsDead;
    public int OnParry; //�и� Ʈ����
    public int IsParry; //�и� ���۱���
    public int OnParring;
    public int IsEnergy;


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
        IsDead = Animator.StringToHash("IsDead");
        OnParry = Animator.StringToHash("OnParry");
        IsParry = Animator.StringToHash("IsParry");
        OnParring = Animator.StringToHash("OnParring");
        IsEnergy = Animator.StringToHash("IsEnergy");
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
                    animData.Initialize(); // �ʱ�ȭ
                }
            }
            return _anim;
        }
    }

}


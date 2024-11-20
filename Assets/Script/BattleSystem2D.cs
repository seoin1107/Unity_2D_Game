using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;





interface IDeathAlarm
{
    UnityAction deathAlarm { get; set; }
}
interface IDamage // �߻��Լ����� ������ = �θ�θ� ����
{
    void OnDamage(float dmg);
}
interface ILive
{
    bool IsLive { get; }
}


public class BattleSystem2D : Movement2D, IDamage, IDeathAlarm, ILive
{
    public CharacterStatus characterStatus;
    protected float playTime;

    public UnityAction deathAlarm { get; set; }
    
    public bool IsLive
    {
        get => characterStatus.curHP > 0 ? true : false;        
    }
    protected virtual void OnDead()
    {
        deathAlarm?.Invoke();
    }


    public void OnDamage(float dmg)
    {
        // IsParry�� true�� ��� ������ ��ȿȭ
        if (myAnim.GetBool(animData.IsParry))
        {
            Debug.Log("Parried! Damage avoided.");
            myAnim.SetTrigger(animData.OnParring);
            return;
        }
        characterStatus.curHP -= dmg;
        if (characterStatus.curHP > 0.0f)
        {
            myAnim.SetTrigger(animData.OnDamage);
        }
        else
        {
            myAnim.SetTrigger(animData.OnDead);
            OnDead();
            Player2D playerScript = gameObject.GetComponent<Player2D>();
            Picking pickingScript = gameObject.GetComponent<Picking>();
            if (playerScript != null)
            {
                playerScript.enabled = false; // Player2D ��ũ��Ʈ ��Ȱ��ȭ
            }
            if(pickingScript != null)
            {
                pickingScript.enabled = false;
            }
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            myRigid.gravityScale = 0.0f;
        }
    }
}


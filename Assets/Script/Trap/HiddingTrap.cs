using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddingTrap : MonoBehaviour
{
    public CharacterStatus characterstatus;

    private void OnCollisionEnter2D(Collision2D collision)//ĳ���� ���� �����ؼ� ����ü�� �����ϵ���
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //characterstatus.
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddingTrap : MonoBehaviour
{
    public CharacterStatus characterstatus;

    private void OnCollisionEnter2D(Collision2D collision)//캐릭터 스탯 참조해서 현재체력 감소하도록
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //characterstatus.
        }

    }

}

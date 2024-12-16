using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazerPointLeft : BattleSystem2D
{
    public CharacterStatus monsterStatus;

    public GameObject orgRazer;
    public Transform myRazerPoint;
    // Start is called before the first frame update
    void Start()
    {
        OnAttack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnAttack()
    {
        GameObject obj = Instantiate(orgRazer, myRazerPoint.position, Quaternion.identity);
        RazerLeft razer = obj.GetComponent<RazerLeft>();
        if (razer != null)
        {
            Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            razer.OnRazer(direction); // 발사
            razer.damage = monsterStatus.characterStat.totalAtk; // 데미지 설정
        }
    }
}

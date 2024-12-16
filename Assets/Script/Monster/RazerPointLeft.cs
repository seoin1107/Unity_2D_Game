using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazerPointLeft : BattleSystem2D
{
    public CharacterStatus monsterStatus;

    public GameObject orgRazer;
    public Transform myRazerPoint;

    public int minRazerCount; // 최소 소환 개수
    public int maxRazerCount; // 최대 소환 개수
    public float spawnDelay; // 레이저 소환 간의 딜레이
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomSpawnRazers()); // 랜덤 소환 코루틴 시작
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator RandomSpawnRazers()
    {
        int razerCount = Random.Range(minRazerCount, maxRazerCount + 1); // 랜덤 소환 개수 결정

        for (int i = 0; i < razerCount; i++)
        {
            OnAttack(); // 레이저 소환
            yield return new WaitForSeconds(spawnDelay); // 딜레이 후 다음 소환
        }
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

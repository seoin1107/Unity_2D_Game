using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazerPointLeft : BattleSystem2D
{
    public CharacterStatus monsterStatus;

    public GameObject orgRazer;
    public Transform myRazerPoint;

    public int minRazerCount; // �ּ� ��ȯ ����
    public int maxRazerCount; // �ִ� ��ȯ ����
    public float spawnDelay; // ������ ��ȯ ���� ������
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomSpawnRazers()); // ���� ��ȯ �ڷ�ƾ ����
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator RandomSpawnRazers()
    {
        int razerCount = Random.Range(minRazerCount, maxRazerCount + 1); // ���� ��ȯ ���� ����

        for (int i = 0; i < razerCount; i++)
        {
            OnAttack(); // ������ ��ȯ
            yield return new WaitForSeconds(spawnDelay); // ������ �� ���� ��ȯ
        }
    }
    public void OnAttack()
    {
        GameObject obj = Instantiate(orgRazer, myRazerPoint.position, Quaternion.identity);
        RazerLeft razer = obj.GetComponent<RazerLeft>();
        if (razer != null)
        {
            Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            razer.OnRazer(direction); // �߻�
            razer.damage = monsterStatus.characterStat.totalAtk; // ������ ����
        }
    }
}

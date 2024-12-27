using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject MonsterPrefab;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            SpawnMonster();
        }
    }
    void SpawnMonster()
    {
        if (MonsterPrefab != null)
        {
            Vector3 spawnPosition1 = spawnPoint1 != null ? spawnPoint1.position : transform.position;
            Vector3 spawnPosition2 = spawnPoint2 != null ? spawnPoint2.position : transform.position;
            Vector3 spawnPosition3 = spawnPoint3 != null ? spawnPoint3.position : transform.position;

            Instantiate(MonsterPrefab, spawnPosition1, Quaternion.identity);
            Instantiate(MonsterPrefab, spawnPosition2, Quaternion.identity);
            Instantiate(MonsterPrefab, spawnPosition3, Quaternion.identity);
        }
    }

}

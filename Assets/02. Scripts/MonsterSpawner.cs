using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public GameObject monster;
    [SerializeField] private Transform monsterParent;

    public float spawnTime = 5;
    public int maxCnt = 5;
    private float spawnRange = 10;

    [SerializeField] private List<GameObject> monsters = new List<GameObject>();
    public List<GameObject> Monsters
    {
        get
        {
            return monsters;
        }
    }

    private void Start()
    {
        StartCoroutine(StartMonsterSpawn());
    }

    private IEnumerator StartMonsterSpawn()
    {
        while(true)
        {
            while(monsters.Count >= maxCnt)
            {
                yield return null;
            }

            yield return new WaitForSeconds(spawnTime);

            Vector3 pos = Random.insideUnitSphere * spawnRange + player.transform.position; // 플레이어 사거리 spawnRange 이내에서 스폰
            pos.y = 0;
            GameObject instance = Instantiate(monster, pos, Quaternion.identity, monsterParent);
            
            monsters.Add(instance);

            Debug.Log("몬스터 생성");
        }
    }

    public int GetMonstersCount()
    {
        return monsters.Count;
    }
}

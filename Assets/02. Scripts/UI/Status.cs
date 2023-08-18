using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Status : MonoBehaviour
{
    private MonsterSpawner spawner;
    private Player player;

    public TMP_Text name;
    public TMP_Text stat;
    public Button downgrade;
    public Button upgrade;

    private void Awake()
    {
        spawner = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void Init(string name, float value)
    {
        this.name.text = name;
        stat.text = GetStatus().ToString();
        SetButton(value);
    }

    private void SetButton(float value)
    {
        float down = -value;
        float up = value;
        downgrade.onClick.AddListener(() => SetText(down));
        upgrade.onClick.AddListener(() => SetText(up));
    }

    private void SetText(float value)
    {
        float cur = float.Parse(stat.text) + value;
        if(cur <= 0)
        {
            Debug.Log("잘못된 값입니다.");
            return;
        }
        stat.text = cur.ToString();
        SetStatus();
    }

    private void SetStatus()
    {
        switch(name.text)
        {
            case "몬스터 생성 주기":
                spawner.spawnTime = float.Parse(stat.text);
                break;
            case "몬스터 최대 수":
                spawner.maxCnt = int.Parse(stat.text);
                break;
            case "몬스터의 체력":
                spawner.monster.GetComponent<Monster>().maxHp = float.Parse(stat.text);
                break;
            case "몬스터의 공격력":
                spawner.monster.GetComponent<Monster>().atk = float.Parse(stat.text);
                break;
            case "몬스터의 초당 공격 횟수":
                spawner.monster.GetComponent<Monster>().attackCount = float.Parse(stat.text);
                break;
            case "몬스터의 공격 사거리":
                spawner.monster.GetComponent<Monster>().range = float.Parse(stat.text);
                break;
            case "플레이어의 체력":
                player.maxHp = float.Parse(stat.text);
                break;
            case "플레이어의 공격력":
                player.atk = float.Parse(stat.text);
                break;
            case "플레이어의 초당 공격 횟수":
                player.attackCount = float.Parse(stat.text);
                break;
            case "플레이어의 공격 사거리":
                player.range = float.Parse(stat.text);
                break;
            case "플레이어의 스킬2 범위":
                player.skill2range = float.Parse(stat.text);
                break;
        }
    }

    private float GetStatus()
    {
        float tmp = 0;
        switch(name.text)
        {
            case "몬스터 생성 주기":
                tmp = spawner.spawnTime;
                break;
            case "몬스터 최대 수":
                tmp = spawner.maxCnt;
                break;
            case "몬스터의 체력":
                tmp = spawner.monster.GetComponent<Monster>().maxHp;
                break;
            case "몬스터의 공격력":
                tmp = spawner.monster.GetComponent<Monster>().atk;
                break;
            case "몬스터의 초당 공격 횟수":
                tmp = spawner.monster.GetComponent<Monster>().attackCount;
                break;
            case "몬스터의 공격 사거리":
                tmp = spawner.monster.GetComponent<Monster>().range;
                break;
            case "플레이어의 체력":
                tmp = player.maxHp;
                break;
            case "플레이어의 공격력":
                tmp = player.atk;
                break;
            case "플레이어의 초당 공격 횟수":
                tmp = player.attackCount;
                break;
            case "플레이어의 공격 사거리":
                tmp = player.range;
                break;
            case "플레이어의 스킬2 범위":
                tmp = player.skill2range;
                break;
        }
        return tmp;
    }
}
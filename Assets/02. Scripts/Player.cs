using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum State
{
    Idle = 0,
    Move = 1,
    Attack = 2,
    Dead = 3
}

public class Player : MonoBehaviour
{
    public delegate void UpdateHpUI(float curHp, float maxHp);
    public UpdateHpUI updateHpUI;
    public delegate void UpdateExpUI(float curExp, float levelUpExp);
    public UpdateExpUI updateExpUI;
    public delegate void UpdateLevelUI(float curLevel);
    public UpdateLevelUI updateLevelUI;

    public EffectController effect;
    public MonsterSpawner spawner;
    public Animator animator;

    public float maxHp = 100;
    public float atk = 10;
    public float attackCount = 1;
    public float range = 1;
    public float skill2range = 2;

    [SerializeField] private float curHp;
    [SerializeField] private int curLevel = 1;
    [SerializeField] private float levelUpExp = 100;
    [SerializeField] private float curExp;
    
    [SerializeField] private State curState;

    [SerializeField] private GameObject targetMonster;

    private bool isAttack = false;
    private int curSkill = 0;

    public float Hp
    {
        get
        {
            return curHp;
        }
        set
        {
            curHp = value;
            if(Hp < 0)
            {
                curHp = 0;
            }
            else if(Hp > maxHp)
            {
                curHp = maxHp;
            }
            updateHpUI(curHp, maxHp);
        }
    }

    public float Exp
    {
        get
        {
            return curExp;
        }
        set
        {
            curExp = value;
            if(curExp >= levelUpExp)
            {
                Level += 1;
                curExp = levelUpExp - curExp;
            }
            updateExpUI(curExp, levelUpExp);
            PlayerPrefs.SetFloat("Exp", curExp);
        }
    }

    public int Level
    {
        get
        {
            return curLevel;
        }
        set
        {
            curLevel = value;
            updateLevelUI(curLevel);
            PlayerPrefs.SetInt("Level", curLevel);
        }
    }

    private void Start()
    {
        Hp = maxHp;
        Exp = PlayerPrefs.GetFloat("Exp", 0);
        Level = PlayerPrefs.GetInt("Level", 1);

        curState = State.Idle;

        isAttack = false;
        curSkill = 0;
    }

    private void Update()
    {
        if(spawner.GetMonstersCount() == 0) // 몬스터가 없으면
        {
            ChangeState(State.Idle);
        }
        else
        {
            GameObject monster = GetNearest();
            if((targetMonster == null || targetMonster.GetComponent<Monster>().curState == State.Dead) && // 타겟이 없거나, 죽었고
                Vector3.Distance(transform.position, monster.transform.position) <= range) // 제일 가까운 적이 사거리 내이면 target 갱신
            {
                targetMonster = monster;
            }
            else if(targetMonster != null && targetMonster.GetComponent<Monster>().curState != State.Dead &&
                Vector3.Distance(transform.position, targetMonster.transform.position) <= range)
            {
                ChangeState(State.Attack);
            }
            else
            {
                ChangeState(State.Move);
            }
        }

        switch(curState)
        {
            case State.Idle:
                break;
            case State.Move:
                Move();
                break;
            case State.Attack:
                if(!isAttack)
                {
                    StartCoroutine(Attack());
                }
                break;
            case State.Dead:
                break;
        }
    }

    private void ChangeState(State state)
    {
        if(state == curState)
        {
            return;
        }

        animator.SetInteger("State", (int)state);

        curState = state;
    }

    private void Move()
    {
        GameObject target = GetNearest();

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime);
        transform.LookAt(target.transform);
    }

    private IEnumerator Attack()
    {
        isAttack = true;

        transform.LookAt(targetMonster.gameObject.transform);

        switch(curSkill) // 스킬을 순서대로 사용
        {
            case 0:
                animator.SetTrigger("Attack1");
                curSkill = 1;
                break;
            case 1:
                animator.SetTrigger("Attack2");
                curSkill = 2;
                break;
            case 2:
                animator.SetTrigger("Attack3");
                curSkill = 0;
                break;
        }

        yield return new WaitForSeconds(1.0f/attackCount);

        isAttack = false;
    }

    private void OnAttack1Trigger() // 단일 공격
    {
        effect.EnableEffect(0);
        targetMonster.GetComponent<Monster>().Hp -= atk;
        targetMonster.GetComponent<Monster>().updateDamageUI(atk);
    }

    private void OnAttack2Trigger() // 범위 공격
    {
        effect.EnableEffect(1);
        for(int i=0;i<spawner.Monsters.Count;i++)
        {
            float dist = Vector3.Distance(gameObject.transform.position, spawner.Monsters[i].transform.position);
            if(dist <= skill2range)
            {
                spawner.Monsters[i].GetComponent<Monster>().Hp -= atk;
                spawner.Monsters[i].GetComponent<Monster>().updateDamageUI(atk);
            }
        }
    }

    private void OnAttack3Trigger() // 체력 회복
    {
        effect.EnableEffect(2);
        Hp += atk;
    }

    private GameObject GetNearest() // 가장 가까운 적 구하기
    {
        GameObject nearest = spawner.Monsters[0];
        float minDist = Vector3.Distance(gameObject.transform.position, spawner.Monsters[0].transform.position);

        for(int i=1;i<spawner.Monsters.Count;i++)
        {
            float dist = Vector3.Distance(gameObject.transform.position, spawner.Monsters[i].transform.position);
            if(minDist > dist)
            {
                minDist = dist;
                nearest = spawner.Monsters[i];
            }
        }

        return nearest;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public delegate void UpdateHpUI(float curHp, float maxHp);
    public UpdateHpUI updateHpUI;
    public delegate void InactiveHpUI();
    public InactiveHpUI inactiveHpUI;
    public delegate void UpdateDamageUI(float damage);
    public UpdateDamageUI updateDamageUI;

    private MonsterSpawner spawner;
    private Player player;
    public Animator animator;

    public float maxHp = 100;
    public float atk = 10;
    public float attackCount = 1;
    public float range = 1;

    [SerializeField] private float curHp;
    [SerializeField] private int exp = 10;

    public State curState;

    private bool isAttack = false;
    private bool isDead = false;

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

    private void Start()
    {
        spawner = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();
        player = GameObject.Find("Player").GetComponent<Player>();
        
        Hp = maxHp;
        curState = State.Idle;
        
        isAttack = false;
    }

    private void Update()
    {
        if(Hp <= 0)
        {
            ChangeState(State.Dead);
        }
        else if(Vector3.Distance(player.transform.position, gameObject.transform.position) <= range)
        {
            ChangeState(State.Attack);
        }
        else
        {
            ChangeState(State.Move);
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
                if(!isDead)
                {
                    Die();
                }
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
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime);
        transform.LookAt(player.transform);
    }

    private IEnumerator Attack()
    {
        isAttack = true;
        
        transform.LookAt(player.transform);
        
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.0f/attackCount);

        isAttack = false;
    }

    private void Die()
    {
        isDead = true;

        inactiveHpUI();

        spawner.Monsters.Remove(gameObject);
        
        player.Exp += exp;
    }

    private void OnAttack1Trigger()
    {
        player.Hp -= atk;
    }

    private void OnDeadTrigger()
    {
        Destroy(gameObject);
    }
}

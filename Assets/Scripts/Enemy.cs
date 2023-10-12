using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Header("Preset Fields")] 
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject splashFx;
    
    [Header("Settings")]
    [SerializeField] private float attackRange;

    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("VFX")]
    [SerializeField] private GameObject damageVFXPrefab;

    [Header("Wander Settings")]
    public float wanderRadius = 10f;
    public float wanderTime = 5f;  // Wandar cooldown
    private float wanderTimer;

    private NavMeshAgent navMeshAgent;

    public enum State 
    {
        None,
        Idle,
        Attack,
        Wander
    }
    
    [Header("Debug")]
    public State state = State.None;
    public State nextState = State.None;

    private bool attackDone;

    private void Start()
    { 
        state = State.None;
        nextState = State.Wander;
        currentHealth = maxHealth;
        navMeshAgent = GetComponent<NavMeshAgent>();
        wanderTimer = wanderTime;
        GameManager.Instance.RegisterEnemy(this);
    }

    private void Update()
    {
        //1. 스테이트 전환 상황 판단
        if (nextState == State.None) 
        {
            switch (state) 
            {
                case State.Idle:
                    //1 << 6인 이유는 Player의 Layer가 6이기 때문
                    if (Physics.CheckSphere(transform.position, attackRange, 1 << 6, QueryTriggerInteraction.Ignore))
                    {
                        nextState = State.Attack;
                    }
                    break;
                case State.Attack:
                    if (attackDone)
                    {
                        nextState = State.Idle;
                        attackDone = false;
                    }
                    break;
                case State.Wander:
                    if (wanderTimer <= 0)
                    {
                        wanderTimer = wanderTime;
                        SetRandomDestination();
                    }
                    else
                    {
                        wanderTimer -= Time.deltaTime;
                    }
                    break;
            }
        }
        
        //2. 스테이트 초기화
        if (nextState != State.None) 
        {
            state = nextState;
            nextState = State.None;
            switch (state) 
            {
                case State.Idle:
                    break;
                case State.Attack:
                    Attack();
                    break;
                case State.Wander:
                    SetRandomDestination();
                    animator.SetTrigger("walk");
                    break;
            }
        }
        
        //3. 글로벌 & 스테이트 업데이트
        //insert code here...
    }
    
    private void Attack() //현재 공격은 애니메이션만 작동합니다.
    {
        animator.SetTrigger("attack");
    }

    public void InstantiateFx() //Unity Animation Event 에서 실행됩니다.
    {
        Instantiate(splashFx, transform.position, Quaternion.identity);
    }
    
    public void WhenAnimationDone() //Unity Animation Event 에서 실행됩니다.
    {
        attackDone = true;
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos를 사용하여 공격 범위를 Scene View에서 확인할 수 있게 합니다. (인게임에서는 볼 수 없습니다.)
        //해당 함수는 없어도 기능 상의 문제는 없지만, 기능 체크 및 디버깅을 용이하게 합니다.
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, attackRange);
    }

    private void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, wanderRadius, -1);
        navMeshAgent.SetDestination(navHit.position);

        animator.SetTrigger("walk");
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (damageVFXPrefab)
        {
            Instantiate(damageVFXPrefab, transform.position, Quaternion.identity);
        }
        Debug.Log($"Damage Taken. Current Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 playerForward = GameObject.FindGameObjectWithTag("Player").transform.forward;
        Vector3 spawnPosition = playerPosition + playerForward * 3f;   // 10f == distance
        ItemManager.Instance.SpawnPowerUpItem(spawnPosition);

        GameManager.Instance.UnregisterEnemy(this);
        GameManager.Instance.CheckGameEndCondition();
        Destroy(gameObject);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

}

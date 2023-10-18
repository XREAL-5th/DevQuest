using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public GameObject hitFx;
    [Header("Preset Fields")] 
    [SerializeField] private Animator animator;
    [SerializeField] private Animator WalkAnimator;
    [SerializeField] private GameObject splashFx;
    
    
    [Header("Settings")]
    [SerializeField] private float attackRange;
    int playerAttackPower;

    [Header("Wander Settings")]
    [SerializeField] private float wanderRadius = 10f;
    [SerializeField] private float wanderTimer = 5f; // wander 유지 시간

    [SerializeField] private Slider healthBar;



    private NavMeshAgent navMeshAgent;
    private float timer;
    private Vector3 randomPoint;

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
    [SerializeField] public int maxHealth = 100;
    public int currentHealth;

    private void Start()
    { 
        navMeshAgent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        state = State.Wander;
        // nextState = State.Idle;
        currentHealth = maxHealth; // 체력 초기화
    }

    private void Update()
    {
        // Player를 찾을 때까지 반복적으로 시도
        if (playerAttackPower == 0 || playerAttackPower == 10)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                Player player = playerObject.GetComponent<Player>();
                if (player != null)
                {
                    playerAttackPower = player.attackPower;
                }
            }
        }
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
                    if (timer <= 0) // wander 종료
                    {
                        randomPoint = RandomWanderPoint();
                        navMeshAgent.SetDestination(randomPoint);
                        timer = wanderTimer;
                    }
                    timer -= Time.deltaTime;
                    if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
                    {
                        timer = 0;
                    }
                    break;
                 //insert code here...
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
                    break;
            }
        }
        
        //3. 글로벌 & 스테이트 업데이트
        //insert code here...
    }

    private Vector3 RandomWanderPoint()
    {
        WalkAnimator.SetTrigger("walk");
        Vector3 randomPoint = Random.insideUnitSphere * wanderRadius;
        randomPoint += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, wanderRadius, 1);
        return hit.position;
    }

    public void Attack() //현재 공격은 애니메이션만 작동합니다. 
    {
        animator.SetTrigger("attack");
        // 공격을 받았을 때 체력을 감소시키는 로직
        TakeDamage(playerAttackPower); // 현재 palyer의 attect power 만큼
        Debug.Log("[enemy] -" + playerAttackPower);

        Instantiate(hitFx, transform.position, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthBar(); // 체력바 업데이트
    }

    private void Die()
    {
        GameManager.Instance.EnemyKilled(); // 적이 처치되었을 때 GameManager에게 알림
        Destroy(gameObject);
    }

    private void UpdateHealthBar()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBar.value = healthPercentage;
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
}

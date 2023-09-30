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
    public Transform player;
    [SerializeField] private float chaseRange = 10f;

    [Header("Enemy State")]
    [SerializeField] private float HP = 100f;
    [SerializeField] private float originEnemySpeed = 3.5f;
    public GameObject DeadVfx;
    private GameObject deadVfxInstance;

    private NavMeshAgent navMeshAgent;

    public enum State 
    {
        None,
        Idle,
        Attack,
        Walk
    }
    
    [Header("Debug")]
    public State state = State.None;
    public State nextState = State.None;
    private bool attackDone;

    private void Start()
    { 
        state = State.None;
        nextState = State.Idle;
        navMeshAgent = GetComponent<NavMeshAgent>();
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
                    // 공격 범위 안에 있으면 Attack 상태로 전환
                    if (Physics.CheckSphere(transform.position, attackRange, 1 << 6, QueryTriggerInteraction.Ignore))
                    {
                        nextState = State.Attack;
                    }

                    // 추적 범위 안에 있으면 Walk 상태로 전환
                    else if (Physics.CheckSphere(transform.position, chaseRange, 1 << 6, QueryTriggerInteraction.Ignore))
                    {
                        nextState = State.Walk;
                    }
                    break;

                case State.Attack:
                    if (attackDone)
                    {
                        nextState = State.Idle;
                        attackDone = false;
                    }
                    break;
                //insert code here...

                case State.Walk:
                    // 목적지(추적 할 곳) 목표 위치 지정 함수 -> 플레이어의 위치를 목적지로 설정.
                    navMeshAgent.SetDestination(player.position);

                    // Walk 상태에서 공격 범위 안에 있으면 Attack 상태로 전환
                    if (Physics.CheckSphere(transform.position, attackRange, 1 << 6, QueryTriggerInteraction.Ignore))
                    {
                        nextState = State.Attack;
                    }

                    // 추적 범위 벗어날 시 Walk 상태에서 Idle 상태로 전환
                    else if (!Physics.CheckSphere(transform.position, chaseRange, 1 << 6, QueryTriggerInteraction.Ignore))
                    {
                        nextState = State.Idle;
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
                //insert code here...

                case State.Walk:
                    Walk();
                    break;
            }
        }
        
        //3. 글로벌 & 스테이트 업데이트
        //insert code here...
    }

    // 속도를 너프 합니다.
    public void GetDebuffSpeed(float speed)
    {
        if(navMeshAgent != null)
        {
            navMeshAgent.speed = speed;
            // 3초의 지속시간 부여 후 원래 속도로 돌아가기
            Invoke("ResetEnemySpeed", 3.0f);
        }
    }

    // 지속시간을 갖고 다시 원래속도로 복귀 합니다.
    private void ResetEnemySpeed()
    {
      //  Debug.Log("원래 속도로");
        navMeshAgent.speed = originEnemySpeed;
    }

    // 일정 데미지 부여 합니다.
    public void GetDamage(float damage)
    {
        HP -= damage;

        // Debug.Log(HP);

        // HP 소진 시 Enemy 사망
        if(HP <= 0)
        {
            // Enemy 사망 시 죽는 이펙트 생성
            deadVfxInstance =  Instantiate(DeadVfx, transform.position, Quaternion.identity);
            Dead();
        }
    }

    // Enemy 사망
    private void Dead()
    {
        // Enemy 제거
        Destroy(gameObject);

        // 죽는 이펙트 인스턴스 파괴
        Destroy(deadVfxInstance, 2f);
    }
    

    private void Attack() //현재 공격은 애니메이션만 작동합니다.
    {
        animator.SetTrigger("attack");
    }

    private void Walk()
    {
        animator.SetTrigger("walk");
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
       // Gizmos.DrawSphere(transform.position, attackRange);
        Gizmos.DrawSphere(transform.position, chaseRange);
    }
}

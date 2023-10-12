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

    [SerializeField] private float wanderRadius = 2.0f;
    private float wanderspeed;
    private Vector3 targetPoint; //wander시 이동할 targetPoint

    public float moveSpeed = 1.0f;
    private Rigidbody _rb;
    private float idletime = 2.0f;
    private float curtime = 0.0f;

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
        //nextState = State.Idle;
        nextState = State.Wander;
        _rb = GetComponent<Rigidbody>();
        SetRandomTarget();
    }

    private void Update()
    {
        //1. 스테이트 전환 상황 판단. nextState가 None일때 실행
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

                    //idletime에 해당하는 초만큼 대기후 다시 걷기
                    curtime += Time.deltaTime;
                    if (curtime > idletime)
                    {
                        curtime = 0;
                        nextState = State.Wander;
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
                case State.Wander:
                    // 현재 위치에서 목표 지점까지의 거리 계산
                    float distanceToTarget = Vector3.Distance(transform.position, targetPoint);
                    // 현재 위치에서 목표 지점까지의 방향 계산
                    Vector3 moveDirection = (targetPoint - transform.position).normalized;
                    // 이동
                    _rb.MovePosition(_rb.position + moveDirection * moveSpeed * Time.deltaTime);
                    // 회전
                    transform.LookAt(targetPoint);

                    if (distanceToTarget < 0.1f)
                    {
                        SetRandomTarget();
                        nextState = State.Idle;
                    }

                    if (Physics.CheckSphere(transform.position, attackRange, 1 << 6, QueryTriggerInteraction.Ignore))
                    {
                        nextState = State.Attack;
                    }
                    break;

            }
        }
        
        //2. 스테이트 초기화. nextState가 None이 아닐때 실행.
        if (nextState != State.None) 
        {
            state = nextState;
            nextState = State.None;
            //state를 nextState로, nextState를 None으로 갱신
            switch (state) 
            {
                case State.Idle:
                    break;
                case State.Attack:
                    Attack();
                    break;
                case State.Wander:
                    Wander();
                    break;
                //insert code here...
            }
        }
        
        //3. 글로벌 & 스테이트 업데이트
        //insert code here...
    }
    
    private void Attack() //현재 공격은 애니메이션만 작동합니다.
    {
        animator.SetTrigger("attack");
    }

    private void Wander()
    {
        animator.SetBool("walk", true);
        animator.SetBool("idle", false);
    }

    public void InstantiateFx() //Unity Animation Event 에서 실행됩니다.
    {
        Instantiate(splashFx, transform.position, Quaternion.identity);
    }
    
    public void WhenAnimationDone() //Unity Animation Event 에서 실행됩니다.
    {
        attackDone = true;
    }

    // 반경 내에서 랜덤한 목표 지점 설정 및 대기상태 애니메이션으로 변환
    void SetRandomTarget()
    {
        animator.SetBool("walk", false);
        animator.SetBool("idle", true);

        Vector2 randomPoint = Random.insideUnitCircle * wanderRadius;
        targetPoint = new Vector3(randomPoint.x, 0, randomPoint.y) + transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos를 사용하여 공격 범위를 Scene View에서 확인할 수 있게 합니다. (인게임에서는 볼 수 없습니다.)
        //해당 함수는 없어도 기능 상의 문제는 없지만, 기능 체크 및 디버깅을 용이하게 합니다.
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, attackRange);
    }
}

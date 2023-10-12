using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Header("Preset Fields")] 
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject splashFx;
    [SerializeField] private Rigidbody rigid;
    
    [Header("Settings")]
    [SerializeField] private float attackRange;

    public int HP = 100;
    
    public enum State 
    {
        None,
        Idle,
        Attack,
        Chase
    }

    
    
    [Header("Debug")]
    public State state = State.None;
    public State nextState = State.None;

    private bool attackDone;
    private bool chaseDone;
    [SerializeField] private float chaseSpeed;

    private void Start()
    { 
        state = State.None;
        nextState = State.Idle;
    }

    private void Update()
    {
        // 1. 스테이트 전환 상황 판단
        if (nextState == State.None)
        {
            switch (state)
            {
                case State.Idle:
                    // 1 << 6인 이유는 Player의 Layer가 6이기 때문
                    // if (Physics.CheckSphere(transform.position, attackRange - 2, 1 << 6, QueryTriggerInteraction.Ignore))
                    // {
                    //    nextState = State.Attack;
                    //}
                    if (Physics.CheckSphere(transform.position + new Vector3(0, 1.0f, 0), attackRange, 1 << 6, QueryTriggerInteraction.Ignore))
                    {
                        nextState = State.Chase;
                    }
                    break;
                case State.Chase:
                    if (chaseDone)
                    {
                        nextState = State.Idle;
                        chaseDone = false;
                    }
                    break;
                // insert code here...
                case State.Attack:
                    if (attackDone)
                    {
                        nextState = State.Idle;
                        attackDone = false;
                    }
                    break;
            }
        }

        // 2. 스테이트 초기화
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
                // insert code here...
                case State.Chase:
                    Chase();
                    break;
            }
        }

        // 3. 글로벌 & 스테이트 업데이트
        // insert code here...
       
    }

    private void FixedUpdate()
    {
        if (Physics.CheckSphere(transform.position + new Vector3(0, 1.0f, 0), attackRange, 1 << 6, QueryTriggerInteraction.Ignore))
        {
            transform.LookAt(GameEndingSingleton.main.player.transform.position);
            transform.position += transform.forward * chaseSpeed;
        }
    }

    private void Attack() // 현재 공격은 애니메이션만 작동합니다.
    {
        animator.SetTrigger("attack");
        HP -= GameEndingSingleton.main.player.GetComponent<PlayerItem>().damage;
        Debug.LogFormat("{0}", HP);
        if (HP == 0)
        {
            Destroy(gameObject);
        }
    }

    private void Chase()
    {
        animator.SetTrigger("walk");
    }

    public void InstantiateFx() // Unity Animation Event 에서 실행됩니다.
    {
        Instantiate(splashFx, transform.position, Quaternion.identity);
    }
    
    public void WhenAnimationDone() // Unity Animation Event 에서 실행됩니다.
    {
        attackDone = true;
        chaseDone = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (nextState == State.None)
        {
            if (collision.gameObject.name == "Bullet(Clone)")
            {
                nextState = State.Attack;
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        // Gizmos를 사용하여 공격 범위를 Scene View에서 확인할 수 있게 합니다. (인게임에서는 볼 수 없습니다.)
        // 해당 함수는 없어도 기능 상의 문제는 없지만, 기능 체크 및 디버깅을 용이하게 합니다.
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        // Gizmos.DrawSphere(transform.position, attackRange);
        Gizmos.DrawSphere(transform.position + new Vector3(0, 1.0f, 0), attackRange - 2);
    }
}

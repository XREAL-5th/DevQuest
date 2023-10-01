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

    public enum State
    {
        None,
        Idle,
        Attack
    }

    [Header("Debug")]
    public State state = State.None;
    public State nextState = State.None;

    private bool attackDone;
    public float maxHealth = 100f;
    private float currentHealth;
    private void Start()
    {
        state = State.None;
        nextState = State.Idle;
        currentHealth = maxHealth;
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
                    //insert code here...
            }
        }

        //3. 글로벌 & 스테이트 업데이트
        //insert code here...
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // 적 사망 처리 로직을 여기에 추가
        // 예를 들어, 애니메이션 재생 및 파괴
        Destroy(gameObject);
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
        // Gizmos를 사용하여 공격 범위를 Scene View에서 확인
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, attackRange);

        // 디버그 코드: Gizmo 그리기 시 로그 출력
        Debug.Log("Drawing Gizmos for Enemy");
    }
}

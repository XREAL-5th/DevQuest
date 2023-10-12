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
    [SerializeField] private float followRange;
    [SerializeField] private float followSpeed;

    public GameObject Player;

    // HP값 세팅
    int initialHP = 100;
    int currentHP;
    
    public enum State 
    {
        None,
        Idle,
        Follow,
        Attack,
        Die
    }
    
    [Header("Debug")]
    public State state = State.None;
    public State nextState = State.None;

    private bool attackDone;

    private void Start()
    {
        currentHP = initialHP;
        state = State.None;
        nextState = State.Idle;
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
                    if (Physics.CheckSphere(transform.position, followRange, 1 << 6, QueryTriggerInteraction.Ignore))
                    {
                        nextState = State.Follow;
                    }
                    break;
                case State.Follow:
                    //1 << 6인 이유는 Player의 Layer가 6이기 때문
                    if (Physics.CheckSphere(transform.position, attackRange, 1 << 6, QueryTriggerInteraction.Ignore))
                    {
                        animator.SetBool("follow", false);
                        nextState = State.Attack;
                    }
                    else if (!Physics.CheckSphere(transform.position, followRange, 1 << 6, QueryTriggerInteraction.Ignore))
                    {
                        animator.SetBool("follow", false);
                        nextState = State.Idle;
                    }
                    else
                        nextState = State.Follow;
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
                    Idle();
                    break;
                case State.Follow:
                    Follow();
                    break;
                case State.Attack:
                    Attack();
                    break;
                case State.Die:
                    Die();
                    break;
                    //insert code here...
            }
        }
    }
    private void Idle()
    {
        animator.SetTrigger("idle");
        
    }
    private void Follow()
    {     
        animator.SetBool("follow", true);
        transform.LookAt(Player.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, followSpeed * Time.deltaTime);
    }

    private void Attack() 
    {
        animator.SetTrigger("attack");
    }
    private void Die()
    {
        animator.SetTrigger("die");
        Debug.Log("Monster die");
    }


    public void InstantiateFx() //Unity Animation Event 에서 실행됩니다.
    {
        Instantiate(splashFx, transform.position, Quaternion.identity);
    }
    
    public void WhenAnimationDone() //Unity Animation Event 에서 실행됩니다.
    {
        attackDone = true;      
    }

    public void HPDamaged(int minDamage, int maxDamage)
    {
        animator.SetTrigger("damaged");
        // 공격 당하면 5 ~ 15 HP 감소
        currentHP -= Random.Range(minDamage, maxDamage);
        Debug.Log("HP: " + currentHP);
        animator.SetTrigger("idle");
        if (currentHP < 0)
        {
            nextState = State.Die;
        }            
    }
    private void OnCollisionEnter(Collision collision)
    {        
        HPDamaged(Player.GetComponent<Player>().minDamage, Player.GetComponent<Player>().maxDamage);
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos를 사용하여 공격 범위를 Scene View에서 확인할 수 있게 합니다. (인게임에서는 볼 수 없습니다.)
        //해당 함수는 없어도 기능 상의 문제는 없지만, 기능 체크 및 디버깅을 용이하게 합니다.
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, attackRange);
    }

}

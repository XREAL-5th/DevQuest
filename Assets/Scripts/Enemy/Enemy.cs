using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Header("Preset Fields")] 
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject splashFx;
    
    [Header("Settings")]
    [SerializeField] private float attackRange;
    [SerializeField] private float chaseRange;
    public float curHP = 50f;
    private float maxHP = 50f;
    public Transform target;

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
    NavMeshAgent m_enemy = null;
    private Slider hpBar;
    private Text attackDamage;


    private void Start()
    { 
        state = State.None;
        nextState = State.Chase;
        m_enemy = GetComponent<NavMeshAgent>();
        hpBar = GetComponentInChildren<Slider>();
        hpBar.value = (float)curHP / (float)maxHP;
        attackDamage = GetComponentInChildren<Text>();
        //InvokeRepeating("MoveToNextWayPoint", 0f, time);
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
                    else if (Physics.CheckSphere(transform.position, chaseRange, 1 << 6, QueryTriggerInteraction.Ignore))
                    {
                        nextState = State.Chase;
                    }
                    break;
                case State.Attack:
                    if (attackDone)
                    {
                        nextState = State.Idle;
                        attackDone = false;
                    }
                    break;
                case State.Chase:
                    if (Physics.CheckSphere(transform.position, attackRange, 1 << 6, QueryTriggerInteraction.Ignore))
                    {
                        nextState = State.Attack;
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
                case State.Chase:
                    Chase();
                    break;
                //insert code here...
            }
        }
        
        //3. 글로벌 & 스테이트 업데이트
        //insert code here...
    }
    
    private void Attack() //현재 공격은 애니메이션만 작동합니다.
    {
        animator.SetBool("isWalking", false);
        animator.SetTrigger("attack");
    }
    private void Chase()
    {
        print("chasing");
        animator.SetBool("isWalking", true);
        m_enemy.SetDestination(target.position);
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

    public void TakeDamage(float amount)
    {

        curHP -= amount;
        HandleHP();
        attackDamage.text = "-" + amount.ToString();
        StartCoroutine(textOff());
        if (curHP <= 0f)
        {
            Destroy(gameObject);
            GameManager.Instance.currEnemyNum -= 1;
            GameManager.Instance.GameClear();

            GameObject item = Instantiate(ItemManager.Instance.buffItem, this.transform.position, this.transform.rotation);
        }
    }

    private void HandleHP()
    {
        
        hpBar.value = (float)curHP / (float)maxHP;
    }
    
    IEnumerator textOff()
    {
        yield return new WaitForSeconds(1f);
        attackDamage.text = "";
    }

}

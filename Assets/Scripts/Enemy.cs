using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private float recognizationRange;
    [SerializeField] private float attackDamage;
    [SerializeField] public float enemyMovingSpeed;
    [SerializeField] public float enemyRotatingSpeed;
    [SerializeField] public int health;

    public enum State 
    {
        None,
        Idle,
        Recognization,
        Attack
    }
    
    [Header("Debug")]
    public State state = State.None;
    public State nextState = State.None;

    private bool attackDone;

    private void Start()
    { 
        state = State.None;
        nextState = State.Idle;
        attackDamage = 1;
        enemyMovingSpeed = 5f;
        enemyRotatingSpeed = 500f;
        health = 3;
        
    }

    private void Update()
    {
        //1. 현재 state에 대한 작동 및 스테이트 전환 상황 판단
        if (nextState == State.None) 
        {
            switch (state) 
            {
                case State.Idle:
                    //1 << 6인 이유는 Player의 Layer가 6이기 때문
                    if (Physics.CheckSphere(transform.position, recognizationRange, 1 << 6, QueryTriggerInteraction.Ignore))
                    {
                        nextState = State.Recognization;
                        //Debug.Log("적 인식!");
                    }
                    else if (Physics.CheckSphere(transform.position, attackRange, 1 << 6, QueryTriggerInteraction.Ignore))
                    {
                        nextState = State.Attack;
                        //Debug.Log("공격 개시!");
                    }
                    break;
                case State.Recognization:
                    transform.position = Vector3.MoveTowards(transform.position, GameMain.main.playerPosition, enemyMovingSpeed * Time.deltaTime);
                    //Quaternion temp = Quaternion.Euler(GameMain.main.playerRotation.eulerAngles[0], GameMain.main.playerRotation.eulerAngles[1] + 180, GameMain.main.playerRotation.eulerAngles[2]);
                    Vector3 direc = GameMain.main.playerPosition - transform.position;
                    Quaternion temp = Quaternion.LookRotation(direc);

                    transform.rotation = Quaternion.RotateTowards(transform.rotation, temp, enemyRotatingSpeed * Time.deltaTime);
                    //transform.Translate((this.transform.position - GameMain.main.playerPosition) * 0.001f);

                    if (Physics.CheckSphere(transform.position, attackRange, 1 << 6, QueryTriggerInteraction.Ignore))
                    {
                        nextState = State.Attack;
                        //Debug.Log("공격 개시!");
                    }
                    break;
                case State.Attack:
                    if (attackDone)
                    {
                        nextState = State.Recognization;
                        attackDone = false;
                        //Debug.Log("너 조심해라, 나 보고 있다");
                    }
                    break;
                //insert code here...
            }
        }
        
        //2. 스테이트 초기화, 애니메이션 작동
        if (nextState != State.None) 
        {
            state = nextState;
            nextState = State.None;
            switch (state) 
            {
                case State.Idle:
                    break;
                case State.Recognization:
                    RecognizeAnimation();
                    break;
                case State.Attack:
                    AttackAnimation();
                    break;
                //insert code here...
            }
        }
        
        if(state == State.Recognization)
        {
            //Debug.Log(GameMain.main.playerPosition);
        }
        //3. 글로벌 & 스테이트 업데이트
        //insert code here...

        if (health == 1)
        {
            //Debug.Log("살려줘");
        }
        else if (health == 0)
        {
            Destroy(this.gameObject);
        }
    }
    
    private void AttackAnimation() //현재 공격은 애니메이션 작동 및 캐릭터의 체력을 깎습니다.
    {
        animator.SetTrigger("attack");
    }

    private void RecognizeAnimation() //현재 모션은 애니메이션 및 캐릭터를 따라 움직입니다.
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
        Gizmos.DrawSphere(transform.position, attackRange);
        Gizmos.DrawSphere(transform.position, recognizationRange);
    }
}

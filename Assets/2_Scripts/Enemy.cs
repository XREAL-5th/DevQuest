using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.UI;
using static UnityEditor.PlayerSettings;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Header("Preset Fields")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject splashFx;

    [Header("Settings")]
    [SerializeField] private float runRange;
    [SerializeField] private float attackRange;
    public int HP;
    public enum State
    {
        None,
        Idle,
        Attack,
        Walking,
        Die
    }

    [Header("Debug")]
    public State state = State.None;
    public State nextState = State.None;

    private bool attackDone;
    private float aniFloat;
    private NavMeshAgent agent;
    private void Start()
    {
        HP = 60;
        state = State.Idle;
        agent = this.GetComponent<NavMeshAgent>();
        StartCoroutine("Idle");
    }

    private void Update()
    {
        //죽음 로직
        if (this.HP <= 0)       
        {
            animator.SetTrigger("Die");
            this.gameObject.layer = 0;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) //죽음 애니메이션 종료 후 제거
            {
                Destroy(this.gameObject);
                GameManager.Instance.CoinDrop(new Vector3(this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z), this.transform.rotation);
            }
        }

        #region Y Bot State
        switch (state)
        {
            case State.Idle:
                state = State.Walking;
                break;
            case State.Walking:
                if (Physics.CheckSphere(transform.position, attackRange, 1 << 6, QueryTriggerInteraction.Ignore))
                {
                    state = State.Attack;
                    aniFloat = 0.0f;
                }
                if ((Physics.CheckSphere(transform.position, runRange, 1 << 6, QueryTriggerInteraction.Ignore)))
                {
                    agent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
                    agent.speed = 1.5f;
                    animator.SetFloat("Walk", 2.0f);
                }
                else
                {
                    agent.speed = 1.0f;
                    animator.SetFloat("Walk", 1.0f);
                }

                break;
            case State.Attack:
                if (!Physics.CheckSphere(transform.position, attackRange, 1 << 6, QueryTriggerInteraction.Ignore))
                {
                    animator.SetBool("Attack", false);
                    state = State.Walking;
                }
                else
                {
                    animator.SetBool("Attack", true);
                }
                break;

        }
        #endregion

        #region 기존 State
        //1. 스테이트 전환 상황 판단
        //if (nextState == State.None)
        //{
        //    switch (state)
        //    {
        //        case State.Idle:
        //            //1 << 6인 이유는 Player의 Layer가 6이기 때문
        //            if (Physics.CheckSphere(transform.position, attackRange, 1 << 6, QueryTriggerInteraction.Ignore))
        //            {
        //                nextState = State.Attack;
        //            }
        //            break;
        //        case State.Attack:
        //            if (attackDone)
        //            {
        //                nextState = State.Idle;
        //                attackDone = false;
        //            }
        //            break;
        //            //insert code here...
        //    }
        //}

        ////2. 스테이트 초기화
        //if (nextState != State.None)
        //{
        //    state = nextState;
        //    nextState = State.None;
        //    switch (state)
        //    {
        //        case State.Idle:
        //            break;
        //        case State.Attack:
        //            Attack();
        //            break;
        //            //insert code here...
        //    }
        //}

        //3. 글로벌 & 스테이트 업데이트
        //insert code here...
        #endregion

    }
    #region 주변 배회 로직
    IEnumerator Idle()      //주변 배회 코루틴
    {
        while (true)
        {
            agent.SetDestination(SetPosition());
            yield return new WaitForSeconds(4);     //4초마다 타겟 포지션 변경
        }
    }

    private Vector3 SetPosition()       //NavMesh 타겟 포지션
    {
        float radius = 6.0f;

        Vector3 targetPos = this.transform.position + SetAngle(radius, Random.Range(0, 360));

        return targetPos;
    }
    private Vector3 SetAngle(float radius, int angle)   //적 주변 원 반경
    {
        Vector3 position = Vector3.zero;

        position.x = Mathf.Cos(angle) * radius;
        position.z = Mathf.Sin(angle) * radius;
    
        return position;
    }
    #endregion

    #region 미사용
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
    #endregion
}

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
    [SerializeField] private Transform player;
    [SerializeField] private float chaseRange = 10f;

    [Header("Enemy State")]
    [SerializeField] private float HP = 100f;
    [SerializeField] private float currentHP = 0f;
    [SerializeField] private float originEnemySpeed = 3.5f;
    public GameObject DeadVfx;
    private GameObject deadVfxInstance;
 //   public Image healthBarBackGround;
   // [SerializeField] private Image healthBarField;

    // 적이 사망했을 때 호출되는 이벤트
    public event System.Action OnDeath;

    private NavMeshAgent navMeshAgent;
    private PlayerState playerState; // Player State 컴포넌트를 저장할 변수

    public enum State 
    {
        None,
        Idle,
        Attack,
        Walk,
        
       LongRangeAttack // 원거리 공격 상태를 넣을려다 실패함.
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

        player = GameObject.Find("Player").GetComponent<Transform>();

        currentHP = HP;
        

        if (player != null )
        {
            playerState = player.GetComponent<PlayerState>();

            if(playerState == null )
            {
                Debug.LogError("Player State 컴포넌트를 찾을 수 없습니다.");
            }
        }
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
                    navMeshAgent.SetDestination(player.transform.position);

                    // Walk 상태에서 공격 범위 안에 있으면 Attack 상태로 전환
                    if (Physics.CheckSphere(transform.position, attackRange, 1 << 6, QueryTriggerInteraction.Ignore))
                    {
                        nextState = State.Attack;
                    }

                    // 추적 범위 벗어날 시 Walk 상태에서 Idle 상태로 전환
                   // 추적 범위 벗어날 시 LongRangeAttack (원거리 공격) 상태로 전환을 시도해보려다 실패..
                    else if (!Physics.CheckSphere(transform.position, chaseRange, 1 << 6, QueryTriggerInteraction.Ignore))
                    {
                        //nextState = State.LongRangeAttack;
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


                //// 구현 실패한 스테이트
                //case State.LongRangeAttack:
                //    ReadyLongAttack();
                //    break;
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
        // HP -= damage;

        currentHP -= damage;
       

        // Debug.Log(HP);

        // HP 소진 시 Enemy 사망
        if(currentHP <= 0)
        {
            // Enemy 사망 시 죽는 이펙트 생성
            deadVfxInstance =  Instantiate(DeadVfx, transform.position, Quaternion.identity);

            Dead();
        }
    }

    // Enemy 사망
    private void Dead()
    {
        // OnDeath 이벤트를 발생시켜 다른 스크립트에서 적의 사망을 감지할 수 있도록 함
        OnDeath?.Invoke();

        //HPBarScript.HPBar_instance.isEnmeyDead = true;
        HPBarScript.HPBar_instance.RemoveHPBar(gameObject);

        // Enemy 제거
        Destroy(gameObject);
       // DestroyImmediate(gameObject);

        // 죽는 이펙트 인스턴스 파괴
        Destroy(deadVfxInstance, 2f);
    }
    

    private void Attack() //현재 공격은 애니메이션만 작동합니다.
    {
        // 2023.10.10 문제 발생
        // 원거리 공격(LongRangeAttack) 스테이트를 추가하려다 실패해서 
        // 기존에 구현했던 Idle <-> Walk <-> Attack 상태로 되돌리는데 walk 상태에서 attack 상태 전환으로 이루어진 이후에 발동하는 attack 애니메이션이 작동이 안하는 문제가 발생함.. 
        // 혹시나 원거리 공격 구현했던 코드를 다 주석 처리 해보았으나 효과는 없었음.. 
        // 애니메이션 부분을 무언가를 잘못 만진 거 같은데 원인을 못 찾고 있습니다.. 

        animator.SetTrigger("attack");

        // Player가 사정거리 안에 들어와 공격 모션 발동 시 플레이어 HP 깎는다.
        playerState.currentPlayerHp -= 10;
    }

    private void Walk()
    {
        animator.SetTrigger("walk");
    }


    public void InstantiateFx() //Unity Animation Event 에서 실행됩니다.
    {
        Debug.Log("공격 이펙트 발동");
        Instantiate(splashFx, transform.position, Quaternion.identity);
    }

    public void WhenAnimationDone() //Unity Animation Event 에서 실행됩니다.
    {
        attackDone = true;
    }




    //// 원거리 공격 애니메이션 추가하려다 실패함.
    //private void ReadyLongAttack()
    //{
    //    animator.SetTrigger("longRange");
    //    Debug.Log("원거리 애니매이션 호출");
    //}


    //// RushJump 애니메이션에 Animation Event로 실행해보려 했으나 실패
    //public void HighJump()
    //{
    //    // 2023.10.10 문제 발생
    //    // 이 부분은 애니메이션을 호출하지도 않았는데, 처음 게임 실행 할 때 애니메이션 실행과 함께 디버그가 출력 됨.
    //    Debug.Log("최대 점프 순간 ");
    //}



    private void OnDrawGizmosSelected()
    {
        //Gizmos를 사용하여 공격 범위를 Scene View에서 확인할 수 있게 합니다. (인게임에서는 볼 수 없습니다.)
        //해당 함수는 없어도 기능 상의 문제는 없지만, 기능 체크 및 디버깅을 용이하게 합니다.
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, attackRange);
       // Gizmos.DrawSphere(transform.position, chaseRange);
    }
}

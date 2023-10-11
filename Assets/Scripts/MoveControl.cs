using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

public class MoveControl : MonoBehaviour
{
    [Header("Preset Fields")]
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private CapsuleCollider col;

    [Header("Settings")]
    [SerializeField][Range(1f, 10f)] private float moveSpeed;
    [SerializeField][Range(1f, 10f)] private float jumpAmount;
    public GameObject itemVFXPrefab;

    [Header("Shooting")]
    [SerializeField] private GameObject projectilePrefab;
    public GameObject bulletTrailPrefab;

    [Header("Speed Boost Skill")]
    [SerializeField] private float boostedSpeedMultiplier = 2f; 
    [SerializeField] private float speedBoostDuration = 3f;
    [SerializeField] private float speedBoostCooldown = 8f;
    public GameObject speedBoostVFXPrefab;
    private bool isBoosted = false;
    private float speedBoostCooldownTimer = 0f;

    [Header("UI")]
    [SerializeField] private TMPro.TextMeshProUGUI speedBoostUIText; 

    //FSM(finite state machine)에 대한 더 자세한 내용은 세션 3회차에서 배울 것입니다!
    public enum State 
    {
        None,
        Idle,
        Jump
    }
    
    [Header("Debug")]
    public State state = State.None;
    public State nextState = State.None;
    public bool landed = false;
    public bool moving = false;
    
    private float stateTime;
    private Vector3 forward, right;

    private float defaultDamage = 50f;
    private float currentDamage;
    private bool damageBoosted = false;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        state = State.None;
        nextState = State.Idle;
        stateTime = 0f;
        forward = transform.forward;
        right = transform.right;

        currentDamage = defaultDamage;
    }

    private void Update()
    {
        //0. 글로벌 상황 판단
        stateTime += Time.deltaTime;
        CheckLanded();
        //insert code here...

        //1. 스테이트 전환 상황 판단
        if (nextState == State.None) 
        {
            switch (state) 
            {
                case State.Idle:
                    if (landed) 
                    {
                        if (Input.GetKey(KeyCode.Space)) 
                        {
                            nextState = State.Jump;
                        }
                    }
                    break;
                case State.Jump:
                    if (landed) 
                    {
                        nextState = State.Idle;
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
                case State.Jump:
                    var vel = rigid.velocity;
                    vel.y = jumpAmount;
                    rigid.velocity = vel;
                    break;
                //insert code here...
            }
            stateTime = 0f;
        }

        //3. 글로벌 & 스테이트 업데이트
       
        // detect user's shooting input
        if (Input.GetMouseButtonDown(0))  // left mouse down
        {
            ShootHitscan();
        }

        // Speed Boost Skill Activation
        if (Input.GetKeyDown(KeyCode.F) && speedBoostCooldownTimer <= 0 && !isBoosted) // F = the skill activation key
        {
            StartCoroutine(SpeedBoostCoroutine());
        }
        if (speedBoostCooldownTimer > 0)
        {
            speedBoostCooldownTimer -= Time.deltaTime;
        }

        if (isBoosted)
        {
            speedBoostUIText.text = "Speed Boost Active!";
        }
        else if (speedBoostCooldownTimer > 0)
        {
            speedBoostUIText.text = $"Speed Boost Cooldown: {Mathf.Ceil(speedBoostCooldownTimer)}s";
        }
        else
        {
            speedBoostUIText.text = "Speed Boost Ready!";
        }
    }

    private void FixedUpdate()
    {
        UpdateInput();
    }

    private void CheckLanded() {
        //발 위치에 작은 구를 하나 생성한 후, 그 구가 땅에 닿는지 검사한다.
        //1 << 3은 Ground의 레이어가 3이기 때문, << 는 비트 연산자
        var center = col.bounds.center;
        var origin = new Vector3(center.x, center.y - ((col.height - 1f) / 2 + 0.15f), center.z);
        landed = Physics.CheckSphere(origin, 0.45f, 1 << 3, QueryTriggerInteraction.Ignore);
    }
    
    private void UpdateInput()
    {
        var direction = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W)) direction += forward; //Forward
        if (Input.GetKey(KeyCode.A)) direction += -right; //Left
        if (Input.GetKey(KeyCode.S)) direction += -forward; //Back
        if (Input.GetKey(KeyCode.D)) direction += right; //Right
        
        direction.Normalize(); //대각선 이동(Ex. W + A)시에도 동일한 이동속도를 위해 direction을 Normalize
        
        transform.Translate( moveSpeed * Time.deltaTime * direction); //Move
    }

    void ShootHitscan()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 1000f)) // 1000f is the max distance
        {
            Vector3 shootOrigin = Camera.main.transform.position;
            Vector3 hitPoint = hitInfo.point;

            SpawnBulletTrail(shootOrigin, hitPoint);

            Enemy enemy = hitInfo.collider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log($"Hitscan hit enemy: {enemy.name}");
                enemy.TakeDamage(currentDamage);
            }
        }
    }

    void SpawnBulletTrail(Vector3 start, Vector3 end)
    {
        GameObject trail = Instantiate(bulletTrailPrefab, start, Quaternion.identity);

        LineRenderer lr = trail.GetComponent<LineRenderer>();
        if (lr != null)
        {
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
        }

        Destroy(trail, 0.5f);
    }

    //TODO: fix - not working
    private void ShootProjectile()
    {
        // TODO idea: remove raycasting?
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            Vector3 fireDirection = (hitInfo.point - transform.position).normalized;

            GameObject bulletInstance = Instantiate(projectilePrefab, transform.position, Quaternion.LookRotation(fireDirection));
            Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();
            if (bulletRb)
            {
                bulletRb.velocity = fireDirection * 20f;
            }
            else
            {
                Debug.LogError("No Rigidbody found on the bullet.");
            }
            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 5f);
            Debug.Log($"Hit object: {hitInfo.collider.name} at {hitInfo.point}");
        }

    }

    // Item

    public void BoostDamage(float boostedPercentage, float duration)
    {
        if (!damageBoosted)
        {
            StartCoroutine(DamageBoostCoroutine(boostedPercentage, duration));
        }
    }

    private IEnumerator DamageBoostCoroutine(float boostedPercentage, float duration)
    {
        damageBoosted = true;
        currentDamage = defaultDamage*boostedPercentage;
        yield return new WaitForSeconds(duration);
        currentDamage = defaultDamage;
        damageBoosted = false;
    }

    // Skill

    private IEnumerator SpeedBoostCoroutine()
    {
        isBoosted = true;
        moveSpeed *= boostedSpeedMultiplier; // Boost the speed

        yield return new WaitForSeconds(speedBoostDuration);

        moveSpeed /= boostedSpeedMultiplier; // Return to normal speed
        isBoosted = false;
        speedBoostCooldownTimer = speedBoostCooldown; // Reset the cooldown timer
    }
}
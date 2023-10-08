using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerControl : MonoBehaviour
{
    [Header("Preset Fields")]
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private CapsuleCollider col;

    [Header("Settings")]
    [SerializeField][Range(1f, 10f)] public float moveSpeed;
    [SerializeField][Range(1f, 10f)] public float jumpAmount;
    [SerializeField] public int healthPoint;


    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private TextMeshProUGUI hpText;


    [SerializeField]
    public float currentHp;

    //FSM(finite state machine)�� ���� �� �ڼ��� ������ ���� 3ȸ������ ��� ���Դϴ�!
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

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        state = State.None;
        nextState = State.Idle;
        stateTime = 0f;
        forward = transform.forward;
        right = transform.right;

        currentHp = healthPoint;
        hpBar.value = (float)currentHp / (float)healthPoint;
        hpText.text = currentHp.ToString();
    }

    private void Update()
    {
        //0. �۷ι� ��Ȳ �Ǵ�
        stateTime += Time.deltaTime;
        CheckLanded();
        //insert code here...

        //1. ������Ʈ ��ȯ ��Ȳ �Ǵ�
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

        //2. ������Ʈ �ʱ�ȭ
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

        //3. �۷ι� & ������Ʈ ������Ʈ
        //insert code here...


        hpBar.value = (float)currentHp / (float)healthPoint;
        hpText.text = currentHp.ToString();
    }

    private void FixedUpdate()
    {
        UpdateInput();
    }

    private void CheckLanded()
    {
        //�� ��ġ�� ���� ���� �ϳ� ������ ��, �� ���� ���� ����� �˻��Ѵ�.
        //1 << 3�� Ground�� ���̾ 3�̱� ����, << �� ��Ʈ ������
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

        direction.Normalize(); //�밢�� �̵�(Ex. W + A)�ÿ��� ������ �̵��ӵ��� ���� direction�� Normalize

        transform.Translate(moveSpeed * Time.deltaTime * direction); //Move
    }
   

    public void Damaged(int damage)
    {
        GameManager.main.player.currentHp -= damage;
        if (GameManager.main.player.currentHp <= 0)
        {
            GameManager.main.player.currentHp = 0;
            GameManager.main.GameEnd();
        }
    }
  
}

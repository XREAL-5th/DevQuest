using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using static UnityEngine.EventSystems.EventTrigger;

public class GameMain : MonoBehaviour
{
    [Header("EnemySettings")]
    [SerializeField] public GameObject enemyContainer;
    [SerializeField] public Enemy[] enemy;
    [HideInInspector] public int enemy_count;

    [Header("UserScreenSettings")]
    [SerializeField] public Camera mainCamera;
    [SerializeField] public GameObject gun;

    [Header("VFX")]
    [SerializeField] public GameObject effect;

    // Singleton
    public static GameMain main;

    // ## Game Manager
    [HideInInspector] public float gameTimer;
    private bool isShoot;
    private bool isOnhit;

    private bool isEffectOn;
    private float effetTimer;


    // ## User Power
    private int gunPower;
     

    // ## VFX
    private LineRenderer lineRenderer;
    private Vector3 shootPoint;
    private Ray ray;
    private RaycastHit hit;
    private float layDistance;
    //private int layerMask;
    //[HideInInspector] public Vector3 userPosition;
    //[HideInInspector] public Quaternion userOrientation;



    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }





    private void Start()
    {
        //main = this;
        gameTimer = 100f;
        effetTimer = 0f;

        enemy_count = enemyContainer.transform.childCount;
        for (int i = 0; i < enemy_count; i++) { enemy[i] = GameObject.Find($"Enemy ({i})").GetComponent<Enemy>(); }

        layDistance = 500.0f;
        isShoot = false;
        isOnhit = false;
        isEffectOn = false;
        effetTimer = 0f;

        gunPower = 1;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        if (gameTimer >= 0f)
        {
            TimeManage();
            InputManage();
            EnemyManage();
            EffectManage();

            ResetState();
        }
    }

    private void TimeManage()
    {
        gameTimer -= Time.deltaTime;
    }


    private void InputManage()
    {
        isShoot = Input.GetMouseButtonDown(0);

        //userPosition = mainCamera.transform.position;
        //userOrientation = mainCamera.transform.rotation;
        //Debug.Log(userPosition);
    }

    private void EnemyManage()
    {
        enemy_count = enemyContainer.transform.childCount;

        if (isShoot)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, layDistance))
            {
                isOnhit = true;
                Debug.DrawRay(ray.origin, ray.direction * layDistance, Color.red);

                if (hit.collider.gameObject.name == "Enemy (0)") { enemy[0].health -= gunPower; }
                else if (hit.collider.gameObject.name == "Enemy (1)") { enemy[1].health -= gunPower; }
                else if (hit.collider.gameObject.name == "Enemy (2)") { enemy[2].health -= gunPower; }
                else if (hit.collider.gameObject.name == "Enemy (3)") { enemy[3].health -= gunPower; }
                else if (hit.collider.gameObject.name == "Enemy (4)") { enemy[4].health -= gunPower; }
                else if (hit.collider.gameObject.name == "Enemy (5)") { enemy[5].health -= gunPower; }
                else if (hit.collider.gameObject.name == "Enemy (6)") { enemy[6].health -= gunPower; }
                else if (hit.collider.gameObject.name == "Enemy (7)") { enemy[7].health -= gunPower; }
                else if (hit.collider.gameObject.name == "Enemy (8)") { enemy[8].health -= gunPower; }
                else if (hit.collider.gameObject.name == "Enemy (9)") { enemy[9].health -= gunPower; }
            }
        }

        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");
        //int enemyCount = enemies.Length;
        //Debug.Log(enemyCount);
    }

    private void EffectManage()
    {
        if(isOnhit)
        {
            effetTimer = 0f;

            //Debug.Log(hit.point);
            shootPoint = gun.transform.position;

            Instantiate(effect, hit.point, Quaternion.identity);
            isEffectOn = true;
        }

        if(isEffectOn)
        {
            effetTimer += Time.deltaTime;
            //Debug.Log(effetTimer);
        }

        if (effetTimer <= 1f)
        {
            lineRenderer.SetPosition(0, shootPoint);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            isEffectOn = false;

            // ¼± ¾ø¾Ö´Â ¹ýÀ» ¸ô¶ó¼­ ¼û°Ü¹ö·È½À´Ï´Ù..
            lineRenderer.SetPosition(0, new(-100f,-100f,-100f));
            lineRenderer.SetPosition(1, new(-101f, -101f, -101f));
        }
    }


    private void ResetState()
    {
        isOnhit = false;
    }
}

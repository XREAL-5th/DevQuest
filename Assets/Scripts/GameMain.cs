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
    [SerializeField] public Enemy[] enemy;
    [SerializeField] public GameObject effect;

    [Header("UserScreenSettings")]
    [SerializeField] public GameObject gun;
    [SerializeField] public Camera mainCamera;
    private LineRenderer lineRenderer;
    private Vector3 shootPoint;

    // Timer, flag
    //private float gameTimer;
    private bool isShoot;
    private float effetTimer;
    private bool isEffectOn;

    private Ray ray;
    private RaycastHit hit;
    private float layDistance;
    
    //private int layerMask;

    [HideInInspector] public Vector3 userPosition;
    [HideInInspector] public Quaternion userOrientation;

    private void Start()
    {
        effetTimer = 0f;

        for (int i = 0; i < enemy.Length; i++) { enemy[i] = GameObject.Find($"Enemy ({i})").GetComponent<Enemy>(); }
        layDistance = 500.0f;
        isShoot = false;
        isEffectOn = false;
        effetTimer = 0f;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        UserManage();
        EnemyManage();
        EffectManage();
        ResetState();
    }


    private void UserManage()
    {
        isShoot = Input.GetMouseButtonDown(0);

        //userPosition = mainCamera.transform.position;
        //userOrientation = mainCamera.transform.rotation;
        //Debug.Log(userPosition);
    }

    private void EnemyManage()
    {
        if (isShoot)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, layDistance))
            {
                Debug.DrawRay(ray.origin, ray.direction * layDistance, Color.red);

                if (hit.collider.gameObject.name == "Enemy (0)") { enemy[0].health -= 1; }
                else if (hit.collider.gameObject.name == "Enemy (1)") { enemy[1].health -= 1; }
                else if (hit.collider.gameObject.name == "Enemy (2)") { enemy[2].health -= 1; }
                else if (hit.collider.gameObject.name == "Enemy (3)") { enemy[3].health -= 1; }
                else if (hit.collider.gameObject.name == "Enemy (4)") { enemy[4].health -= 1; }
                else if (hit.collider.gameObject.name == "Enemy (5)") { enemy[5].health -= 1; }
                else if (hit.collider.gameObject.name == "Enemy (6)") { enemy[6].health -= 1; }
                else if (hit.collider.gameObject.name == "Enemy (7)") { enemy[7].health -= 1; }
                else if (hit.collider.gameObject.name == "Enemy (8)") { enemy[8].health -= 1; }
                else if (hit.collider.gameObject.name == "Enemy (9)") { enemy[9].health -= 1; }
            }
        }
    }

    private void EffectManage()
    {
        if(isShoot)
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
        isShoot = false;
    }
}

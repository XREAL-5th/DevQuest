using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class RayCast : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private GameObject hpCanvas;
    [SerializeField] private ParticleSystem hitParticle;          //몸샷
    [SerializeField] private ParticleSystem criticalParticle;     //헤드샷
    [SerializeField] private ParticleSystem hitGround;            //바닥, 벽 등

    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();

    private Vector3 damagePos;
    private TextMeshProUGUI hpText;

    private RaycastHit hit;
    private float rayDistance = 25f;            //사거리
    private float hitTime = 0.0f;               //연사 속도 제어

    private void Start()
    {
        hpText = hpCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && hitTime >= 0.2f)
        {
            hitTime = 0.0f;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
            {
                if (!hit.collider.CompareTag("Ground")) damagePos = hit.transform.parent.GetComponent<Enemy>().damagePos.position;
                TargetCheck();
            }
        }
        else hitTime += Time.deltaTime;
    }

    //추후에 Enemy에서 제어로 수정
    void TargetCheck()      //부위 별 제어
    {
        if (hit.collider.name == "Head")
        {
            hit.transform.parent.GetComponent<Enemy>().HP -= GameManager.Instance.damage * 2;
            hpText.text = GameManager.Instance.damage * 2 + "";
            Destroy(Instantiate(criticalParticle, hit.point, Quaternion.identity),1.0f);
            Destroy(Instantiate(hpCanvas, damagePos, Quaternion.identity), 1.0f);     
            //hit.transform.GetComponent<Rigidbody>().AddForce(this.transform.forward * 150.0f, ForceMode.Force);
        }
        else if (hit.collider.name == "Body")
        {
            hit.transform.parent.GetComponent<Enemy>().HP -= GameManager.Instance.damage;
            hpText.text = GameManager.Instance.damage + "";
            Destroy(Instantiate(hitParticle, hit.point, Quaternion.identity), 1.0f);
            Destroy(Instantiate(hpCanvas, damagePos, Quaternion.identity), 1.0f);
            //hit.transform.GetComponent<Rigidbody>().AddForce(this.transform.forward * 100.0f,ForceMode.Force);
        }
        else if (hit.collider.CompareTag("Ground"))
        {
            Instantiate(hitGround, hit.point, Quaternion.identity);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class RayCast : MonoBehaviour
{

    private RaycastHit hit;
    private float rayDistance = 25f;            //사거리
    private float hitTime = 0.0f;               //연사 속도 제어

    public ParticleSystem hitParticle;          //몸샷
    public ParticleSystem criticalParticle;     //헤드샷
    public ParticleSystem hitGround;            //바닥, 벽 등

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && hitTime >= 0.2f)
        {
            hitTime = 0.0f;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
            {
                TargetCheck();
            }
        }
        else hitTime += Time.deltaTime;

    }

    void TargetCheck()      //부위 별 제어
    {
        if (hit.collider.name == "Head")
        {
            hit.transform.GetComponent<Enemy>().HP -= 20;
            Instantiate(criticalParticle, hit.point, Quaternion.identity);
            hit.transform.GetComponent<Rigidbody>().AddForce(this.transform.forward * 150.0f, ForceMode.Force);
        }
        else if (hit.collider.name == "Body")
        {
            hit.transform.GetComponent<Enemy>().HP -= 10;
            Instantiate(hitParticle, hit.point, Quaternion.identity);
            hit.transform.GetComponent<Rigidbody>().AddForce(this.transform.forward * 100.0f,ForceMode.Force);
        }
        else if (hit.collider.CompareTag("Ground"))
        {
            Instantiate(hitGround, hit.point, Quaternion.identity);
        }
    }
}

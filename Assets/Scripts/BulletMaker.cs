using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BulletMaker : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    // 이펙트 
    public Transform weaponImpact;
    [SerializeField]
    private ParticleSystem weaponEffect;

    private float destroyTime = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        weaponEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            // 카메라 정면 방향으로 Ray 쏘기
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                Destroy(bullet, destroyTime);

                // 무기 이펙트 재생
                weaponEffect.Play();
                // 레이 방향으로 이펙트 방향 설정
                weaponImpact.forward = hitInfo.normal;
                // 레이가 부딪힌 지점에서 이펙트 생성
                weaponImpact.position = hitInfo.point;
            }
        }

    }
}

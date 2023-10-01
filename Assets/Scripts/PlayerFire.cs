using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePoint;

    public GameObject Bullet;

    public float projectileSpeed = 100f; // 발사체 속도

    public float damage = 30f; // 발사체 데미지

    public GameObject bulletEffect;

    ParticleSystem ps;

    void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //레이 생성 발사될 위치 방향 설정
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            //레이가 부딫힌 대상의 정보 저장
            RaycastHit hitinfo = new RaycastHit();

            // if 물체에 부딫히면 이펙트 표시
            if(Physics.Raycast(ray, out hitinfo))
            {
                bulletEffect.transform.position = hitinfo.point;
                ps.Play();
            }
        }

    }

}

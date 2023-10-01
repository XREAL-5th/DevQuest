using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform crosshair; // 크로스헤어(조준점)의 위치
    public GameObject projectilePrefab; // 발사체 프리팹
    public float damage = 10f; // 발사체 데미지
    public float projectileSpeed = 100f; // 발사체 속도
    public Transform firepoint; // firepoint GameObject를 연결합니다.
    public GameObject bulletEffectPrefab; // 피격 이펙트 프리팹
    ParticleSystem ps;

    private Camera playerCamera;

    private void Start()
    {
        playerCamera = Camera.main;
        ps = bulletEffectPrefab.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        // 왼쪽 마우스 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hitinfo = new RaycastHit();

            // 레이 쏘기
            if (Physics.Raycast(ray, out hitinfo))
            {
                // 레이가 적과 충돌했을 경우
                if (hitinfo.collider.CompareTag("Enemy"))
                {
                    // 적의 체력을 감소시키고 체력이 0 이하일 경우 파괴
                    Enemy enemy = hitinfo.collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                    }

                    // 피격 이펙트 생성 및 초기화
                    GameObject bulletEffect = Instantiate(bulletEffectPrefab, hitinfo.point, Quaternion.identity);
                    ps = bulletEffect.GetComponent<ParticleSystem>();
                    ps.Play();
                    Destroy(bulletEffect, ps.main.duration);
                }

                // 발사체 생성 및 초기화
                GameObject projectile = Instantiate(projectilePrefab, firepoint.position, Quaternion.identity);
                Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
                Vector3 direction = playerCamera.transform.forward; // 카메라의 forward 방향으로 설정
                projectileRigidbody.velocity = direction * projectileSpeed;

                // 발사체 파괴 예약
                Destroy(projectile, 2f);
            }
            else
            {
                // 레이가 아무것도 충돌하지 않았을 경우, 허공에 발사체 생성
                GameObject projectile = Instantiate(projectilePrefab, firepoint.position, Quaternion.identity);
                Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
                Vector3 direction = playerCamera.transform.forward; // 카메라의 forward 방향으로 설정
                projectileRigidbody.velocity = direction * projectileSpeed;

                // 발사체 파괴 예약
                Destroy(projectile, 2f);
            }
        }
    }
}





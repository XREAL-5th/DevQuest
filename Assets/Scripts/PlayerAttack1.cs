using UnityEngine;

public class PlayerAttack1 : MonoBehaviour
{
    public Transform crosshair; // 크로스헤어(조준점)의 위치
    public GameObject projectilePrefab; // 발사체 프리팹
    public float damage = 10f; // 발사체 데미지
    public float projectileSpeed = 100f; // 발사체 속도
    public Transform firepoint; // firepoint GameObject를 연결합니다.

    private Camera playerCamera;

    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        // 왼쪽 마우스 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // 크로스헤어 위치에서 화면의 중심으로 레이를 쏨
        Ray ray = playerCamera.ScreenPointToRay(crosshair.position);
        RaycastHit hit;

        // 레이 쏘기
        if (Physics.Raycast(ray, out hit))
        {
            // 레이가 적과 충돌했을 경우
            if (hit.collider.CompareTag("Enemy"))
            {
                // 적의 체력을 감소시키고 체력이 0 이하일 경우 파괴
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }

            // 발사체 생성 및 초기화
            GameObject projectile = Instantiate(projectilePrefab, firepoint.position, Quaternion.identity);
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
            Vector3 direction = (hit.point - firepoint.position).normalized;
            projectileRigidbody.velocity = direction * projectileSpeed;

            // 발사체 파괴 예약
            Destroy(projectile, 2f);
        }
        else
        {
            // 레이가 아무것도 충돌하지 않았을 경우, 허공에 발사체 생성
            GameObject projectile = Instantiate(projectilePrefab, firepoint.position, Quaternion.identity);
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
            Vector3 direction = ray.direction; // 레이의 방향을 사용하여 발사
            projectileRigidbody.velocity = direction * projectileSpeed;

            // 발사체 파괴 예약
            Destroy(projectile, 2f);
        }
    }
}





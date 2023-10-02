using UnityEngine;

public class PlayerAttack1 : MonoBehaviour
{
    public Transform crosshair; // ũ�ν����(������)�� ��ġ
    public GameObject projectilePrefab; // �߻�ü ������
    public float damage = 10f; // �߻�ü ������
    public float projectileSpeed = 100f; // �߻�ü �ӵ�
    public Transform firepoint; // firepoint GameObject�� �����մϴ�.

    private Camera playerCamera;

    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        // ���� ���콺 Ŭ�� ����
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // ũ�ν���� ��ġ���� ȭ���� �߽����� ���̸� ��
        Ray ray = playerCamera.ScreenPointToRay(crosshair.position);
        RaycastHit hit;

        // ���� ���
        if (Physics.Raycast(ray, out hit))
        {
            // ���̰� ���� �浹���� ���
            if (hit.collider.CompareTag("Enemy"))
            {
                // ���� ü���� ���ҽ�Ű�� ü���� 0 ������ ��� �ı�
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }

            // �߻�ü ���� �� �ʱ�ȭ
            GameObject projectile = Instantiate(projectilePrefab, firepoint.position, Quaternion.identity);
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
            Vector3 direction = (hit.point - firepoint.position).normalized;
            projectileRigidbody.velocity = direction * projectileSpeed;

            // �߻�ü �ı� ����
            Destroy(projectile, 2f);
        }
        else
        {
            // ���̰� �ƹ��͵� �浹���� �ʾ��� ���, ����� �߻�ü ����
            GameObject projectile = Instantiate(projectilePrefab, firepoint.position, Quaternion.identity);
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
            Vector3 direction = ray.direction; // ������ ������ ����Ͽ� �߻�
            projectileRigidbody.velocity = direction * projectileSpeed;

            // �߻�ü �ı� ����
            Destroy(projectile, 2f);
        }
    }
}





using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform crosshair; // ũ�ν����(������)�� ��ġ
    public GameObject projectilePrefab; // �߻�ü ������
    public float damage = 10f; // �߻�ü ������
    public float projectileSpeed = 100f; // �߻�ü �ӵ�
    public Transform firepoint; // firepoint GameObject�� �����մϴ�.
    public GameObject bulletEffectPrefab; // �ǰ� ����Ʈ ������
    ParticleSystem ps;

    private Camera playerCamera;

    private void Start()
    {
        playerCamera = Camera.main;
        ps = bulletEffectPrefab.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        // ���� ���콺 Ŭ�� ����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hitinfo = new RaycastHit();

            // ���� ���
            if (Physics.Raycast(ray, out hitinfo))
            {
                // ���̰� ���� �浹���� ���
                if (hitinfo.collider.CompareTag("Enemy"))
                {
                    // ���� ü���� ���ҽ�Ű�� ü���� 0 ������ ��� �ı�
                    Enemy enemy = hitinfo.collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                    }

                    // �ǰ� ����Ʈ ���� �� �ʱ�ȭ
                    GameObject bulletEffect = Instantiate(bulletEffectPrefab, hitinfo.point, Quaternion.identity);
                    ps = bulletEffect.GetComponent<ParticleSystem>();
                    ps.Play();
                    Destroy(bulletEffect, ps.main.duration);
                }

                // �߻�ü ���� �� �ʱ�ȭ
                GameObject projectile = Instantiate(projectilePrefab, firepoint.position, Quaternion.identity);
                Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
                Vector3 direction = playerCamera.transform.forward; // ī�޶��� forward �������� ����
                projectileRigidbody.velocity = direction * projectileSpeed;

                // �߻�ü �ı� ����
                Destroy(projectile, 2f);
            }
            else
            {
                // ���̰� �ƹ��͵� �浹���� �ʾ��� ���, ����� �߻�ü ����
                GameObject projectile = Instantiate(projectilePrefab, firepoint.position, Quaternion.identity);
                Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
                Vector3 direction = playerCamera.transform.forward; // ī�޶��� forward �������� ����
                projectileRigidbody.velocity = direction * projectileSpeed;

                // �߻�ü �ı� ����
                Destroy(projectile, 2f);
            }
        }
    }
}





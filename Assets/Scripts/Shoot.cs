using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject shootPrefab;
    public GameObject hitEffectPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("�߻�");
            ShootStart();
        }
    }

    void ShootStart()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            /*
            // �÷��̾��� ��ġ���� Ŀ���� ��ġ�� ���ϴ� ���� ���� ���
            Vector3 direction = (hit.point - transform.position).normalized;

            // �߻�ü ����
            GameObject shoot = Instantiate(shootPrefab, transform.position, Quaternion.identity);

            // �߻�ü�� ���� ����
            shoot.transform.forward = direction;
            */

            GameObject shootInstance = Instantiate(shootPrefab, hit.point, Quaternion.identity);
            Instantiate(hitEffectPrefab, hit.point, Quaternion.identity);

            float destroyTime = 2f;
            Destroy(shootInstance, destroyTime);

            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Enemy 10 ����");
                enemy.TakeDamage(10); // 10��ŭ�� �������� ����
            }
        }
    }
}

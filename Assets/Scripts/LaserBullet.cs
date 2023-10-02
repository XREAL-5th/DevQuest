using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Threading.Tasks;

public class LaserBullet : MonoBehaviour
{
    public GameObject magicPrefab; // ���� Prefab�� �Ҵ��� ����
    private float damage = 40f;



    // �� ���� Collider�� �浹���� �� ȣ��˴ϴ�.
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ��ü�� �±׸� Ȯ���Ͽ� ���ϴ� ������ �����մϴ�.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // ���� ��ġ ���� ��������
            Vector3 enemyPosition = collision.gameObject.transform.position;

            // �� �ֺ��� ���� ǥ���ϰ� ���� �������� ���� ����
            MagicMeteros(enemyPosition);

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                // Enemy���� ������ �ο��Ѵ�.
                enemy.GetDamage(damage);
            }

            Destroy(gameObject);
        }
    }

    // ����ȿ���� �����ϰ�, �����ð� ���� �ν��Ͻ��� �����մϴ�.
    async void MagicMeteros(Vector3 CollPostion)
    {
        // ���� Prefab�� �ν��Ͻ�ȭ�Ͽ� �� �ֺ��� ����
        GameObject magicInstance = Instantiate(magicPrefab, CollPostion, Quaternion.identity);

        await WaitForSecondsAsync(2f);

        // ���� ȿ�� �ı�
        Destroy(magicInstance);
    }

    // �񵿱� �������� �����ϴ� �޼ҵ�
    private async Task WaitForSecondsAsync(float seconds)
    {
        await Task.Delay(Mathf.FloorToInt(seconds * 1000));
    }

}

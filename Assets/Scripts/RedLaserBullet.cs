using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class RedLaserBullet : MonoBehaviour
{
    public GameObject DebuffMagic; // ���� Prefab�� �Ҵ��� ����
    private float DebuffSpeed = 1.0f;



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
                // Enemey�� �ӵ��� ���� �Ѵ�.
                enemy.GetDebuffSpeed(DebuffSpeed);
            }

            Destroy(gameObject);
        }
    }

    // ����ȿ���� �����ϰ�, �����ð� ���� �ν��Ͻ��� �����մϴ�.
    async void MagicMeteros(Vector3 CollPostion)
    {
        // ���� Prefab�� �ν��Ͻ�ȭ�Ͽ� �� �ֺ��� ����
        GameObject magicInstance = Instantiate(DebuffMagic, CollPostion, Quaternion.identity);

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

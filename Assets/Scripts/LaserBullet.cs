using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Threading.Tasks;

public class LaserBullet : MonoBehaviour
{
    public GameObject magicPrefab; // 마법 Prefab을 할당할 변수
    private float damage = 40f;



    // 두 개의 Collider가 충돌했을 때 호출됩니다.
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 객체의 태그를 확인하여 원하는 동작을 수행합니다.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 적의 위치 정보 가져오기
            Vector3 enemyPosition = collision.gameObject.transform.position;

            // 적 주변에 원을 표시하고 돌이 떨어지는 마법 생성
            MagicMeteros(enemyPosition);

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                // Enemy에게 데미지 부여한다.
                enemy.GetDamage(damage);
            }

            Destroy(gameObject);
        }
    }

    // 마볍효과를 생성하고, 일정시간 이후 인스턴스를 제거합니다.
    async void MagicMeteros(Vector3 CollPostion)
    {
        // 마법 Prefab을 인스턴스화하여 적 주변에 생성
        GameObject magicInstance = Instantiate(magicPrefab, CollPostion, Quaternion.identity);

        await WaitForSecondsAsync(2f);

        // 마법 효과 파괴
        Destroy(magicInstance);
    }

    // 비동기 웨이팅을 구현하는 메소드
    private async Task WaitForSecondsAsync(float seconds)
    {
        await Task.Delay(Mathf.FloorToInt(seconds * 1000));
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Threading.Tasks;

public class LaserBullet : MonoBehaviour
{
    
    public GameObject magicPrefab; // 마법 Prefab을 할당할 변수
    private bool hasCollided = false; // 충돌 상태를 기록하는 변수

    //private void OnTriggerEnter(Collider other)
    // {
    //     //// 레이저와 충돌한 객체의 Layer가 "Enemy"인 경우
    //     //if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //     //{
    //     //    Debug.Log("레이저가 적과 충돌했습니다!");
    //     //}

    //     if (other.CompareTag("Enemy"))
    //     {
    //         Debug.Log("레이저가 적과 충돌했습니다!");
    //     }
    // }


    // 두 개의 Collider가 충돌했을 때 호출됩니다.
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 객체의 태그를 확인하여 원하는 동작을 수행합니다.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 충돌한 객체가 특정 태그를 가지고 있다면 여기에 코드를 추가하세요.
            Debug.Log("레이저가 적과 충돌했습니다!");

            //// 적 주변에 원을 표시하고 돌이 떨어지는 마법 생성
            //StartCoroutine(DropMagicStone(collision.transform.position));

           
            // 적의 위치 정보 가져오기
            Vector3 enemyPosition = collision.gameObject.transform.position;

            // 적 주변에 원을 표시하고 돌이 떨어지는 마법 생성
          //  StartCoroutine(DropMagicStone(enemyPosition));
              MagicMeteros(enemyPosition);

            Destroy(gameObject);
        }
    }


    async void MagicMeteros(Vector3 CollPostion)
    {
        // 마법 Prefab을 인스턴스화하여 적 주변에 생성
        GameObject magicInstance = Instantiate(magicPrefab, CollPostion, Quaternion.identity);

        await WaitForSecondsAsync(2f);
        Debug.Log("메테오 제거");

        // 마법 효과 파괴
        Destroy(magicInstance);
    }

    // 비동기 웨이팅을 구현하는 메소드
    private async Task WaitForSecondsAsync(float seconds)
    {
        await Task.Delay(Mathf.FloorToInt(seconds * 1000));
    }



    // 돌이 떨어지는 Coroutine
    private IEnumerator DropMagicStone(Vector3 position)
    {
        // 마법 Prefab을 인스턴스화하여 적 주변에 생성
        GameObject magicInstance = Instantiate(magicPrefab, position, Quaternion.identity);


        Debug.Log(magicInstance.name); // 메테오 Prefab 이름 디버그 출력

        // 일정 시간 대기
        yield return new WaitForSeconds(1.0f);

        // 충돌을 판단할 레이저를 디스트로이 해서 그 디스트로이 한 레이저를 기반으로 코루틴을 호출시키니 로직에 문제가 생겼나? -> 이 원인도 아닌 거 같음..
        Debug.Log("메테오 제거");

        // 마법 효과 파괴
        Destroy(magicInstance);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
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
            Destroy(gameObject);
        }
    }
}

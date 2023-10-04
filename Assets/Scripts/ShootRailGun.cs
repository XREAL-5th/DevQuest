using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRailGun : MonoBehaviour
{

    public GameObject[] laserPrefab; // 레이저 Prefab
    [SerializeField]
    private GameObject laserSpawnPoint; // 레이저 발사 위치


    void Update()
    {
     

        // 마우스 왼쪽 클릭 시 데미지 부여 레이저 발사
        if (Input.GetMouseButtonDown(0))
        {
            // 레이저 생성
            // ShootLaser();
             CreateLaser();

      
        }

        // 마우스 오른쪽 클릭 시 속도 너프 레이저 발사
        if(Input.GetMouseButtonDown(1))
        {
            CreateRedLaser();
        }
    }

    // 레이캐스트 충돌 체크
    void ShootLaser()
    {
        // 메인 카메라에서 마우스 포인터 방향으로 레이 발사
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Ray ray = new Ray(laserSpawnPoint.transform.position, laserSpawnPoint.transform.forward);
        RaycastHit hit;

        int enemyLayMask = LayerMask.GetMask("Enemy");

        // 레이케스트 충돌 확인
        if (Physics.Raycast(ray, out hit, 1000f, enemyLayMask))
        {
            Debug.Log("적 감지");
        }
    }

   // Projectile(발사체) 충돌 체크
    private void CreateLaser()
    {
        StartCoroutine(ShowLaser(laserSpawnPoint.transform.position));
    }

    private void CreateRedLaser()
    {
        StartCoroutine(ShoRedwLaser(laserSpawnPoint.transform.position));
    }


    // 데미지 레이저 생성 코루틴
    IEnumerator ShowLaser(Vector3 startPosition)
    {
        // 레이저 생성 및 설정
        GameObject laser = Instantiate(laserPrefab[0], startPosition, Quaternion.Euler(90, 0, 0));
        laser.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);

        // 일정 시간 후 레이저 제거
        yield return new WaitForSeconds(1f);

        Destroy(laser);
    }

    // 속도 너프 레이저 생성 코루틴 
    IEnumerator ShoRedwLaser(Vector3 startPosition)
    {
        // 레이저 생성 및 설정
        GameObject laser = Instantiate(laserPrefab[1], startPosition, Quaternion.Euler(90, 0, 0));
        laser.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);

        // 일정 시간 후 레이저 제거
        yield return new WaitForSeconds(1f);

        Destroy(laser);
    }

}

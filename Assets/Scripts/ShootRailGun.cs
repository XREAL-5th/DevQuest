using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRailGun : MonoBehaviour
{
    public float laserRange = 2f;
    public GameObject laserPrefab; // 레이저
    [SerializeField]
    private GameObject laserSpawnPoint; // 레이저 발사 위치


    void Update()
    {
        // 마우스 클릭을 감지
        if (Input.GetMouseButtonDown(0))
        {
            // 레이저 생성
            // ShootLaser();
            CreateLaser();
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
    void CreateLaser()
    {
        StartCoroutine(ShowLaser(laserSpawnPoint.transform.position));
    }

    IEnumerator ShowLaser(Vector3 startPosition)
    {
        // 레이저 생성 및 설정
        GameObject laser = Instantiate(laserPrefab, startPosition, Quaternion.Euler(90, 0, 0));
        laser.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
       
        // 일정 시간 후 레이저 제거
        yield return new WaitForSeconds(1f);

       
        Destroy(laser);
    }

}

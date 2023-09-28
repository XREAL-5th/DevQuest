using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRailGun : MonoBehaviour
{
    public float laserRange = 2f;
    public GameObject laserPrefab;
    [SerializeField]
    private GameObject laserSpawnPoint; // 레이저 발사 위치


    void Update()
    {
        // 마우스 클릭을 감지
        if (Input.GetMouseButtonDown(0))
        {
            // 레이저 생성
            ShootLaser();
        }
    }

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


            //// 레이저를 적의 위치까지 표시
            //StartCoroutine(ShowLaser(laserSpawnPoint.transform.position, hit.point));

        }
    }

    IEnumerator ShowLaser(Vector3 startPosition, Vector3 endPosition)
    {
        // 레이저 생성 및 설정
        GameObject laser = Instantiate(laserPrefab, startPosition, Quaternion.identity);
        laser.GetComponent<Rigidbody>().AddForce(transform.forward * 600);
        //LineRenderer lineRenderer = laser.GetComponent<LineRenderer>();
        //lineRenderer.SetPosition(0, startPosition);
        //lineRenderer.SetPosition(1, endPosition);

        // 일정 시간 후 레이저 제거
        yield return new WaitForSeconds(0.2f);

        Destroy(laser);
    }
}

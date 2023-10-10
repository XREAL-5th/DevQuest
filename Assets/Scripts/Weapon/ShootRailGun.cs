using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum LaserType
{
    DamageLaser,
    DebuffLaser,
    None
}

public class ShootRailGun : MonoBehaviour
{
    public GameObject[] laserPrefab; // 레이저 Prefab

    [SerializeField] private GameObject laserSpawnPoint; // 레이저 발사 위치
    private PlayerState playerState;
    public bool canAttack = true; // 공격 가능 여부 


    private void Start()
    {
        playerState = transform.parent.GetComponent<PlayerState>();
    }

    void Update()
    {
     
        // 마우스 왼쪽 클릭 시 그리고 공격 부여 물약을 획득 시에 데미지 부여 레이저 발사
        if (Input.GetMouseButtonDown(0) && playerState.IsAttack == true && canAttack)
        {
            // 레이저 생성
            // ShootLaser();
             CreateLaser(LaserType.DamageLaser); // == laserPrefab[0]
        }

        // 마우스 오른쪽 클릭 시 속도 너프 레이저 발사
        if(Input.GetMouseButtonDown(1) && canAttack)
        {
            CreateLaser(LaserType.DebuffLaser); // == laserPrefab[1]
        }
    }



    // 레이캐스트 충돌 체크
    // 테스트 용으로 현재 게임에서는 쓰이지는 않는다.
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
    private void CreateLaser(LaserType laserType)
    {
        StartCoroutine(ShowLaser(laserSpawnPoint.transform.position, laserType));
        
        // 공격 쿨다운 중에는 다시 공격을 할 수 없음
        canAttack = false;
    }


    // 레이저 생성 코루틴
    // 데미지 레이저 - laserPrefab[0] , 속도 디버프 레이저 - laserPrefab[1]
    IEnumerator ShowLaser(Vector3 startPosition, LaserType laserType)
    {
        // 레이저 생성 및 설정
        GameObject laser = Instantiate(laserPrefab[(int)laserType], startPosition, Quaternion.Euler(90, 0, 0));
        laser.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);

        // 레이저 인스턴스는 1초 후 사라지기
        Destroy(laser, 1f);

        // 3초 쿨타임을 가지고 공격을 다시 가능하게 만든다.
        yield return new WaitForSeconds(3f);

        canAttack = true;
    }
}

using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform gunTransform; // 총의 위치 및 방향을 나타내는 Transform
    public float raycastDistance = 100f; // Raycast의 최대 거리
    public LayerMask hitLayer; // 타격을 판정할 레이어
    public GameObject hitFx;

    private void Update()
    {
        // 마우스 왼쪽 버튼을 누르면 발사
        if (Input.GetMouseButtonDown(0))
        {
            ShootRaycast();
        }
    }

    void ShootRaycast()
    {
        // Raycast를 발사할 시작 위치와 방향 설정
        // 총의 위치와 방향을 플레이어 위치와 방향으로 설정
        gunTransform.position = transform.position; // 플레이어의 위치로 설정
        gunTransform.rotation = transform.rotation; // 플레이어가 바라보는 방향으로 설정

        RaycastHit hitInfo; // Raycast를 실행하고 결과를 저장할 변수

        // Raycast
        if (Physics.Raycast(gunTransform.position, gunTransform.forward, out hitInfo, raycastDistance)) // layerMask 생략
        {
            Debug.DrawRay(gunTransform.position, gunTransform.forward * raycastDistance, Color.green);

            Instantiate(hitFx, gunTransform.position, Quaternion.identity);

            // Ray가 오브젝트와 충돌 시
            Debug.Log("Raycast hit: " + hitInfo.collider.name);

            // 타격한 오브젝트의 Enemy 스크립트 확인
            Enemy enemy = hitInfo.collider.GetComponent<Enemy>();

            
            if (enemy != null)
            {
                // Enemy 타격한 경우, 체력을 감소
                enemy.Attack();
                Debug.Log("[enemy]" + hitInfo.collider.name + "Take Damage");
            }
        }
        else
        {
            // Raycast가 아무 오브젝트와도 충돌하지 않았을 때
            Debug.Log("Raycast miss");
        }
    }
}

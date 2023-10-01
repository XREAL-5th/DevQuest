using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shootFX;

    [Header("Settings")]
    [SerializeField] private float fireDelay;
    [SerializeField] private float shootRange = 10f;
    [SerializeField] private float shootPower = 300f;
    [SerializeField] private Vector3 gunPosition = new Vector3(0.55f, -0.2f, 0.5f);

    private float elapsedTime;
    private Transform bulletSpawnPoint;
    private Camera playerCamera;

    private void Start()
    {
        if (bulletPrefab != null)
            bulletSpawnPoint = transform.Find("BulletSpawnPoint");
        transform.localPosition = gunPosition;
        playerCamera = transform.parent.GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        CheckFire();
    }
    private void CheckFire()
    {
        if (Input.GetMouseButton(0) && elapsedTime >= fireDelay)
        {
            Debug.Log("발사");
            elapsedTime = 0;
            if (bulletPrefab != null)
            {
                Shoot();
            }
        }
        else
        {
            elapsedTime += Time.deltaTime;
        }
    }

    private void Shoot()
    {
        Vector3 targetPoint;
        RaycastHit hit;

        /* 카메라를 기준으로 전방 범위내에 목표가 있다면 목표를 조준하고, 없다면 최대범위를 조준.
        조금 더 정밀한 조준 매커니즘을 위해 추가함.*/
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, shootRange))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = playerCamera.transform.position + playerCamera.transform.forward * shootRange;
        }

        Instantiate(shootFX, bulletSpawnPoint);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // 방향을 구하기 위해 목표 지점에서 총알 생성지점을 뺀 후 정규화.
        Vector3 shootDirection = (targetPoint - bulletSpawnPoint.position).normalized;
        bulletRb.AddForce(shootDirection * shootPower);
    }
}

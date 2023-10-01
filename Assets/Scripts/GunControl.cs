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
    private ParticleSystem[] ShootFXParticleSystems;
    private Quaternion bulletSpawnRotation;
    private Camera playerCamera;

    private void Start()
    {
        if(bulletPrefab != null)
        bulletSpawnPoint = transform.Find("BulletSpawnPoint");
        transform.localPosition = gunPosition;
        bulletSpawnRotation = bulletSpawnPoint.rotation;
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
            Debug.Log("น฿ป็");
            elapsedTime = 0;
            if(bulletPrefab != null)
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

        Vector3 shootDirection = (targetPoint - bulletSpawnPoint.position).normalized;
        bulletRb.AddForce(shootDirection * shootPower);
    }
}

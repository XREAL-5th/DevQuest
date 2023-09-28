using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject bullet;

    [Header("Settings")]
    [SerializeField] private float fireDelay;
    [SerializeField] private Vector3 gunPosition = new Vector3(0.55f, -0.2f, 0.5f);

    private float elapsedTime;
    private Transform bulletSpawnPoint;
    private Quaternion bulletSpawnRotation;

    private void Start()
    {
        if(bullet != null)
        bulletSpawnPoint = transform.Find("BulletSpawnPoint");
        transform.localPosition = gunPosition;
        bulletSpawnRotation = bulletSpawnPoint.rotation;
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
            Shoot();
        }
        else
        {
            elapsedTime += Time.deltaTime;
        }
    }

    private void Shoot()
    {
        if(bullet != null)
        Instantiate(bullet,bulletSpawnPoint.position, bulletSpawnRotation);
    }
}

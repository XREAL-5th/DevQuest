using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float range = 100f;
    public Camera aim;
    public GameObject attackEffect;

    [Header("Gun Properties")]
    public GameObject bulletPrefab;

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        GameObject enemy;


        if (Physics.Raycast(aim.transform.position, aim.transform.forward, out hit, range))
        {
            var bullet = Instantiate(bulletPrefab, aim.transform.position, aim.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(aim.transform.forward* 50, ForceMode.Impulse);

            if (hit.transform.gameObject.layer == 7)
            {
                enemy = hit.transform.gameObject;
                enemy.GetComponent<Enemy>().TakeDamage(10);
            }

            GameObject impactObj = Instantiate(attackEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactObj, 2f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float range = 100f;
    public Camera aim;
    public GameObject attackEffect;
    public GameObject fireEffect;

    private RaycastHit hit;
    private GameObject enemy;
    private bool canFire = true;

    [Header("Gun Properties")]
    public GameObject bulletPrefab;

    private void Start()
    {
        GameManager gameManager = GameManager.Instance;
    }
    public void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetButtonDown("Fire2") && canFire)
        {
            StartCoroutine(Bomb());
        }
    }

    private void Shoot()
    {



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

    IEnumerator Bomb()
    {
        canFire = false;
        ShootFireBall();
        yield return new WaitForSeconds(10);
        canFire = true;


    }

    private void ShootFireBall()
    {
        if (Physics.Raycast(aim.transform.position, aim.transform.forward, out hit, range))
        {
            var fireBall = Instantiate(fireEffect, aim.transform.position, aim.transform.rotation);
            fireBall.GetComponent<Rigidbody>().AddForce(aim.transform.forward * 50, ForceMode.Impulse);

            if (hit.transform.gameObject.layer == 7)
            {
                enemy = hit.transform.gameObject;
                enemy.GetComponent<Enemy>().TakeDamage(20);
            }

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.Rendering;

public class PlayerRay : MonoBehaviour
{
    public Transform weapon, BulletIniPos;
    public GameObject Fire, HitPoint, Bullet;
    private GameObject BulletClone;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 6; // only enemies layer
        layerMask = ~layerMask;


        if (Input.GetMouseButtonDown(0))
        {
            float time = Time.time;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // pixel coordinates
            RaycastHit hit; // used in raycast, but switched to Projectile method.

            // Debug.LogFormat("Start: {0}, End: {1}", Bullet.transform.position, ray.origin);
            // Debug.DrawLine(Bullet.transform.position, (Bullet.transform.position - ray.origin) * 1000, Color.red, 5.0f, true);
            // Debug.DrawLine(transform.position, transform.position + ray.direction * 4, Color.red, 1.0f, true);

            OnFireRaycast();
            BulletClone = Instantiate(Bullet, BulletIniPos.position, BulletIniPos.rotation * Quaternion.Euler(90.0f, 0.0f, 0.0f));
        }
    }

    private void OnFireRaycast()
    {
        GameObject hitting = Instantiate(Fire, weapon.position, Quaternion.identity);
        Destroy(hitting, 1);
    }

    // Coroutine으로 총알 구현 - 끊기는 듯한 효과 -> AddForce로 전환
    IEnumerator BulletMove(GameObject cloneBullet, Ray ray, float time)
    {
        
        while (Time.time - time < 10) // 10 seconds
        {
            // cloneBullet.transform.position += ray.direction.normalized * BulletSpeed; // nullException??
            yield return new WaitForSeconds(0.02f);
        }
    }

}


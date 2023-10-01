using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.Rendering;

public class PlayerRay : MonoBehaviour
{
    public Transform FirePoint, weapon;
    public GameObject Fire;
    public GameObject HitPoint;
    public GameObject Bullet;
    public GameObject variantBullet;

    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        weapon = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 6; // only enemies layer
        layerMask = ~layerMask;


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // pixel coordinates
            RaycastHit hit;

            GameObject tempBullet = Instantiate(variantBullet, Bullet.transform.position, Bullet.transform.rotation);
            tempBullet.GetComponent<Rigidbody>().velocity = (Bullet.transform.position - ray.origin).normalized * bulletSpeed * Time.deltaTime;
            // Debug.LogFormat("Start: {0}, End: {1}", Bullet.transform.position, ray.origin);
            // Debug.DrawLine(Bullet.transform.position, (Bullet.transform.position - ray.origin) * 1000, Color.red, 5.0f, true);
            // Debug.DrawLine(transform.position, transform.position + ray.direction * 4, Color.red, 1.0f, true);

            if (Physics.Raycast(transform.position, ray.direction, out hit, Mathf.Infinity, layerMask))
            {
                OnFireRaycast(hit);
            }
            else
            {
                GameObject hitting = Instantiate(Fire, weapon.position, Quaternion.identity);
                Destroy(hitting, 1);
            }
            
        }
    }

    private void OnFireRaycast(RaycastHit hit)
    {
        GameObject hitting = Instantiate(Fire, weapon.position, Quaternion.identity);
        Destroy(hitting, 1);
    }

}


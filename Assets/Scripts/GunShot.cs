using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunShot : MonoBehaviour
{
    //Gun stats
    struct GunStat
    {
        public int damage, ammo;
        public float speed, reloadTime;
        public bool auto; //Auto=ture/semi=false
    }
    GunStat gunStat;
    //CurrentState
    public enum State
    {
        Shooting,
        readyToShoot,
        Reloading,
        None
    }
    public State state = State.readyToShoot;
    int leftAmmo; //bullet left 

    //Reference
    public Camera cam;
    public Transform muzzle;
    public RaycastHit rayHit;

    public TextMeshProUGUI ammoText;
    public GameObject muzzleFlash,bulletHole,spark;
    // Start is called before the first frame update
    void Start()
    {
        //temporary Gunstat setting for test
        GunStat pistol;
        pistol.damage = 30;
        pistol.ammo = 8;
        pistol.speed = 1.0f;
        pistol.reloadTime = 3.0f;
        pistol.auto = false;
        gunStat = pistol;

        leftAmmo = gunStat.ammo;
        state = State.readyToShoot;

    }

    // Update is called once per frame
    private void Update()
    {
        if (state == State.readyToShoot)
        {
            if (gunStat.auto)
            {
                if (Input.GetKey(KeyCode.Mouse0)) state = 0;
            }
            else
                if (Input.GetKeyDown(KeyCode.Mouse0)) state = 0;
        }

        //Shoot
        if (state == State.Shooting) Shoot();

        //Reload
        if (Input.GetKeyDown(KeyCode.R) && state < State.Reloading && leftAmmo < gunStat.ammo) Reload();
        if (leftAmmo == 0 && state < State.Reloading) Reload();
        ammoText.SetText(leftAmmo + " / " + gunStat.ammo);
    }


    private void Shoot()
    {
        state = State.None;
       

        //Enemy Attack
        if(Physics.Raycast(cam.transform.position,cam.transform.forward,out rayHit))
        {
            Debug.Log(rayHit.collider.name);
            if (rayHit.collider.CompareTag("Enemy"))
                //give Damage to Enemy
                rayHit.collider.GetComponent<HpControl>().Damaged(gunStat.damage);
                
        }
        Instantiate(bulletHole, rayHit.point, Quaternion.Euler(rayHit.normal));
        Instantiate(spark, rayHit.point, Quaternion.Euler(rayHit.normal));
        Instantiate(muzzleFlash, muzzle.position, Quaternion.identity);

        leftAmmo--;
        StartCoroutine(ReadyToShoot());
    }
    IEnumerator ReadyToShoot()
    {
        yield return new WaitForSeconds(1.0f/gunStat.speed);
        state = State.readyToShoot;
    }
    private void Reload()
    {
        state = State.Reloading;
        Debug.Log("Reloading!");
        StartCoroutine(Reloading());

    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(gunStat.reloadTime);
        leftAmmo = gunStat.ammo;
        state = State.readyToShoot;
    }
}

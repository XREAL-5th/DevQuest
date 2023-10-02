using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AttackManager : MonoBehaviour
{
    //public GameObject bulletFactory;
    //public Transform firePosition;
    public ParticleSystem hiteffect;
    
    void Start(){

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Shot();
        }
    }

    /*
    void ShotBullet()
    {
        GameObject bullet = Instantiate(bulletFactory);
        bullet.transform.position = firePosition.position;
        bullet.transform.forward = firePosition.transform.right;
    }
    */

    void Shot()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Physics.Raycast(ray, out RaycastHit hitInfo);

        ParticleSystem newParticleSystem = Instantiate(hiteffect, hitInfo.point, Quaternion.identity);
        newParticleSystem.Play();
        
        if (hitInfo.collider.tag == "Enemy")
        {
            HPManager HPMGR = hitInfo.transform.gameObject.GetComponent<HPManager>();
            HPMGR.hp -= 1;
            HPMGR.hitpoint = hitInfo;
            HPMGR.hit = true;
            //Debug.Log(HPMGR.hp);
        }

        Destroy(newParticleSystem.gameObject, newParticleSystem.main.duration);
        
    }
}

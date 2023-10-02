using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePoint;

    public GameObject Bullet;

    public float projectileSpeed = 100f; // �߻�ü �ӵ�

    public float damage = 30f; // �߻�ü ������

    public GameObject bulletEffect;

    ParticleSystem ps;

    void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //���� ���� �߻�� ��ġ ���� ����
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            //���̰� �΋H�� ����� ���� ����
            RaycastHit hitinfo = new RaycastHit();

            // if ��ü�� �΋H���� ����Ʈ ǥ��
            if(Physics.Raycast(ray, out hitinfo))
            {
                bulletEffect.transform.position = hitinfo.point;
                ps.Play();
            }
        }

    }

}

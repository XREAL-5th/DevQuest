using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BulletMaker : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    // ����Ʈ 
    public Transform weaponImpact;
    [SerializeField]
    private ParticleSystem weaponEffect;


    private float destroyTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        weaponEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            // ī�޶� ���� �������� Ray ���
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                Destroy(bullet, destroyTime);

                // ���� ����Ʈ ���
                weaponEffect.Play();
                // ���� �������� ����Ʈ ���� ����
                weaponImpact.forward = hitInfo.normal;
                // ���̰� �ε��� �������� ����Ʈ ����
                weaponImpact.position = hitInfo.point;
            }
        }
    }

}

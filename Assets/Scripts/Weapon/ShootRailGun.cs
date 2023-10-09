using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ShootRailGun : MonoBehaviour
{
    public GameObject[] laserPrefab; // ������ Prefab

    [SerializeField] private GameObject laserSpawnPoint; // ������ �߻� ��ġ
    [SerializeField] private PlayerState playerState;

    private void Start()
    {
        playerState = transform.parent.GetComponent<PlayerState>();
    }

    void Update()
    {
     
        // ���콺 ���� Ŭ�� �� �׸��� ���� �ο� ������ ȹ�� �ÿ� ������ �ο� ������ �߻�
        if (Input.GetMouseButtonDown(0) && playerState.IsAttack == true)
        {
            // ������ ����
            // ShootLaser();
             CreateLaser();

      
        }

        // ���콺 ������ Ŭ�� �� �ӵ� ���� ������ �߻�
        if(Input.GetMouseButtonDown(1))
        {
            CreateRedLaser();
        }
    }

    // ����ĳ��Ʈ �浹 üũ
    void ShootLaser()
    {
        // ���� ī�޶󿡼� ���콺 ������ �������� ���� �߻�
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Ray ray = new Ray(laserSpawnPoint.transform.position, laserSpawnPoint.transform.forward);
        RaycastHit hit;

        int enemyLayMask = LayerMask.GetMask("Enemy");

        // �����ɽ�Ʈ �浹 Ȯ��
        if (Physics.Raycast(ray, out hit, 1000f, enemyLayMask))
        {
            Debug.Log("�� ����");
        }
    }

   // Projectile(�߻�ü) �浹 üũ
    private void CreateLaser()
    {
        StartCoroutine(ShowLaser(laserSpawnPoint.transform.position));
    }

    private void CreateRedLaser()
    {
        StartCoroutine(ShoRedwLaser(laserSpawnPoint.transform.position));
    }


    // ������ ������ ���� �ڷ�ƾ
    IEnumerator ShowLaser(Vector3 startPosition)
    {
        // ������ ���� �� ����
        GameObject laser = Instantiate(laserPrefab[0], startPosition, Quaternion.Euler(90, 0, 0));
        laser.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);

        // ���� �ð� �� ������ ����
        yield return new WaitForSeconds(1f);

        Destroy(laser);
    }

    // �ӵ� ���� ������ ���� �ڷ�ƾ 
    IEnumerator ShoRedwLaser(Vector3 startPosition)
    {
        // ������ ���� �� ����
        GameObject laser = Instantiate(laserPrefab[1], startPosition, Quaternion.Euler(90, 0, 0));
        laser.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);

        // ���� �ð� �� ������ ����
        yield return new WaitForSeconds(1f);

        Destroy(laser);
    }

}

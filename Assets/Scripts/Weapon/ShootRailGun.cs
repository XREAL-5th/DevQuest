using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum LaserType
{
    DamageLaser,
    DebuffLaser,
    None
}

public class ShootRailGun : MonoBehaviour
{
    public GameObject[] laserPrefab; // ������ Prefab

    [SerializeField] private GameObject laserSpawnPoint; // ������ �߻� ��ġ
    private PlayerState playerState;
    public bool canAttack = true; // ���� ���� ���� 


    private void Start()
    {
        playerState = transform.parent.GetComponent<PlayerState>();
    }

    void Update()
    {
     
        // ���콺 ���� Ŭ�� �� �׸��� ���� �ο� ������ ȹ�� �ÿ� ������ �ο� ������ �߻�
        if (Input.GetMouseButtonDown(0) && playerState.IsAttack == true && canAttack)
        {
            // ������ ����
            // ShootLaser();
             CreateLaser(LaserType.DamageLaser); // == laserPrefab[0]
        }

        // ���콺 ������ Ŭ�� �� �ӵ� ���� ������ �߻�
        if(Input.GetMouseButtonDown(1) && canAttack)
        {
            CreateLaser(LaserType.DebuffLaser); // == laserPrefab[1]
        }
    }



    // ����ĳ��Ʈ �浹 üũ
    // �׽�Ʈ ������ ���� ���ӿ����� �������� �ʴ´�.
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
    private void CreateLaser(LaserType laserType)
    {
        StartCoroutine(ShowLaser(laserSpawnPoint.transform.position, laserType));
        
        // ���� ��ٿ� �߿��� �ٽ� ������ �� �� ����
        canAttack = false;
    }


    // ������ ���� �ڷ�ƾ
    // ������ ������ - laserPrefab[0] , �ӵ� ����� ������ - laserPrefab[1]
    IEnumerator ShowLaser(Vector3 startPosition, LaserType laserType)
    {
        // ������ ���� �� ����
        GameObject laser = Instantiate(laserPrefab[(int)laserType], startPosition, Quaternion.Euler(90, 0, 0));
        laser.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);

        // ������ �ν��Ͻ��� 1�� �� �������
        Destroy(laser, 1f);

        // 3�� ��Ÿ���� ������ ������ �ٽ� �����ϰ� �����.
        yield return new WaitForSeconds(3f);

        canAttack = true;
    }
}

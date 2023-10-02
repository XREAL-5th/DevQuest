using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject shootPrefab;
    public GameObject hitEffectPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("발사");
            ShootStart();
        }
    }

    void ShootStart()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // 플레이어의 위치에서 커서의 위치로 향하는 방향 벡터 계산
            Vector3 direction = (hit.point - transform.position).normalized;

            // 발사체 생성
            GameObject shoot = Instantiate(shootPrefab, transform.position, Quaternion.identity);

            // 발사체의 방향 설정
            shoot.transform.forward = direction;

            //Instantiate(shootPrefab, hit.point, Quaternion.identity);
            Instantiate(hitEffectPrefab, hit.point, Quaternion.identity);
        }
    }
}

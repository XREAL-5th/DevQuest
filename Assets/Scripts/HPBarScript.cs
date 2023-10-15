using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 스폰할 때 마다 적 머리위에 HP바를 생성하고, HealthBar는 Enemey 위치를 따라다닌다 -> 현재 Not Use
public class HPBarScript : MonoBehaviour
{
    public static HPBarScript HPBar_instance;

    // 프리팹 변수 선언
    public GameObject m_goPrefab = null;
    private Image healthBarFilled;

    // 몬스터 위치를 담을 리스트
    List<Transform> m_objectList = new List<Transform>();
    //HP바 리스트
    List<GameObject> m_hpBarList = new List<GameObject>();

    Camera m_camera = null;

    public bool isEnmeyDead = false;

    private void Awake()
    {
        HPBar_instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_camera = Camera.main;
        // 자식오브젝트의 1번째 오브젝트를 가져온다.
        healthBarFilled = m_goPrefab.GetComponentsInChildren<Image>()[1];

    }

    public void CreateHPBar()
    {
        // 특정 태그를 가진 객체들을 배열에 저장 한다.
        GameObject[] t_objects = GameObject.FindGameObjectsWithTag("Enemy");

        // 각 객체 개수 만큼 몬스터 위치 리스트에 추가
        for (int i = 0; i < t_objects.Length; i++)
        {
            // 2023.10.14 문제
            // HP바 인스턴스가 1명 -> 1개, 2명 -> 3개, 3명 -> 6개로 늘어가는 문제

            m_objectList.Add(t_objects[i].transform);
            // 몬스터 위치에 HP바 프리팹 생성 한다.
            GameObject t_hpbar = Instantiate(m_goPrefab, t_objects[i].transform.position, Quaternion.identity, transform);

            // 생성된 객체는 HP바 리스트에 추가
            m_hpBarList.Add(t_hpbar);
      
        }
    }

    public void RemoveHPBar(GameObject enemy)
    {
        int index = m_objectList.IndexOf(enemy.transform);
        if (index != -1)
        {
            Destroy(m_hpBarList[index]);
            m_objectList.RemoveAt(index);
            m_hpBarList.RemoveAt(index);
        }
    }


    // Update is called once per frame
    void LateUpdate()
    {
        //if(isEnmeyDead == true)
        //{
        //    Debug.Log("Dead 알림 받음");

        //    isEnmeyDead = false;
        //    m_hpBarList.Clear(); 
        //    return;

        //}

        // HP바는 매 프레임마다 몬스터 머리위로 따라다니게 설정 한다.
        for (int i = m_objectList.Count - 1; i >= 0; i--)
        {
            if (m_objectList[i] == null)
            {
                // 해당 적이 파괴되었으면 리스트에서 제거
                Destroy(m_hpBarList[i]);
                m_objectList.RemoveAt(i);
                m_hpBarList.RemoveAt(i);
            }
            else
            {
                m_hpBarList[i].transform.position = m_camera.WorldToScreenPoint(m_objectList[i].position + new Vector3(0, 3.5f, 0));

                var screenPos = m_hpBarList[i].transform.position;

                if(screenPos.z < 0.0f)
                {
                    screenPos *= -1.0f;
                }
            }
        }
    }
}

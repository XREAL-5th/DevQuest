using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ������ �� ���� �� �Ӹ����� HP�ٸ� �����ϰ�, HealthBar�� Enemey ��ġ�� ����ٴѴ� -> ���� Not Use
public class HPBarScript : MonoBehaviour
{
    public static HPBarScript HPBar_instance;

    // ������ ���� ����
    public GameObject m_goPrefab = null;
    private Image healthBarFilled;

    // ���� ��ġ�� ���� ����Ʈ
    List<Transform> m_objectList = new List<Transform>();
    //HP�� ����Ʈ
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
        // �ڽĿ�����Ʈ�� 1��° ������Ʈ�� �����´�.
        healthBarFilled = m_goPrefab.GetComponentsInChildren<Image>()[1];

    }

    public void CreateHPBar()
    {
        // Ư�� �±׸� ���� ��ü���� �迭�� ���� �Ѵ�.
        GameObject[] t_objects = GameObject.FindGameObjectsWithTag("Enemy");

        // �� ��ü ���� ��ŭ ���� ��ġ ����Ʈ�� �߰�
        for (int i = 0; i < t_objects.Length; i++)
        {
            // 2023.10.14 ����
            // HP�� �ν��Ͻ��� 1�� -> 1��, 2�� -> 3��, 3�� -> 6���� �þ�� ����

            m_objectList.Add(t_objects[i].transform);
            // ���� ��ġ�� HP�� ������ ���� �Ѵ�.
            GameObject t_hpbar = Instantiate(m_goPrefab, t_objects[i].transform.position, Quaternion.identity, transform);

            // ������ ��ü�� HP�� ����Ʈ�� �߰�
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
        //    Debug.Log("Dead �˸� ����");

        //    isEnmeyDead = false;
        //    m_hpBarList.Clear(); 
        //    return;

        //}

        // HP�ٴ� �� �����Ӹ��� ���� �Ӹ����� ����ٴϰ� ���� �Ѵ�.
        for (int i = m_objectList.Count - 1; i >= 0; i--)
        {
            if (m_objectList[i] == null)
            {
                // �ش� ���� �ı��Ǿ����� ����Ʈ���� ����
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

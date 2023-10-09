using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine;

public class HPManager : MonoBehaviour
{

    public int hp = 5;
    public Transform uipoint;
    public GameObject heart1;
    public Material heart0;
    public List<GameObject> hpui;
    public bool hit = false;

    public RaycastHit hitpoint;
    private Rigidbody rb;
    public float knockbackForce = 10f; // 넉백 힘

    // Start is called before the first frame update
    void Start()
    {
        // ui 넣는방식 질문.. 더 좋은 방법이 있을거 같은데 .. ㅠㅠ

          for (int i = 0; i < hp; i++)
        {

            if (i != 0 && i % 5 == 0)
            {
                uipoint.transform.position += Vector3.up * 0.8f;
                uipoint.transform.position -= Vector3.right * 4.0f;
            }

            //GameObject heart = Instantiate(heart1, uipoint.transform.position, Quaternion.identity);
            GameObject heart = Instantiate(heart1, uipoint.transform.position, uipoint.rotation);
            //heart.transform.eulerAngles = new Vector3(90, 0, 0);
            hpui.Add(heart);

            heart.transform.SetParent(gameObject.transform);

            uipoint.transform.position += Vector3.right * 0.8f;

            
        }
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            hit = false;
            Renderer renderer = hpui[hp].GetComponent<Renderer>();
            renderer.material = heart0;
            Vector3 knockbackDirection = (transform.position - hitpoint.point).normalized; // 넉백 방향 계산
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse); // 넉백 힘을 Rigidbody에 적용
        }

        if (hp == 0)
        {
            /*
            코루틴에 대해 질문. 이런방식은 쓰면 안되는건지

            float targettime = 3f;
            float currenttime = 0;
            while (targettime > currenttime)
            {
                currenttime += Time.deltaTime;
                transform.Rotate(Vector3.up, 0.5f);

            }
            
            
            Destroy(gameObject);
            */

            StartCoroutine(RotateAndDestroy());
        }
    }


    IEnumerator RotateAndDestroy()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 3f)
        {
            transform.Rotate(Vector3.up, 5f * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null; // 한 프레임을 기다립니다.
        }

        // 회전이 끝난 후 게임 오브젝트 파괴
        Destroy(gameObject);
        // 싱글톤 -> 모든 오리 죽었는지 확인
        GameOverMGR.main.enemiesnumleft -= 1;
        bool dead = GameOverMGR.main.IfAllEnemiesdead();
    }

}

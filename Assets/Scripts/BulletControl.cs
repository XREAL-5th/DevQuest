using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public GameObject HeadHitPoint, BodyHitPoint, DefaultHitPoint;
    public GameObject Head, Body;
    [SerializeField] private Rigidbody m_Rigidbody;
    [SerializeField] private float BulletSpeed;
    private Vector3 iniPos;

    // Start is called before the first frame update
    void Start()
    {
        iniPos = transform.position;
        m_Rigidbody = GetComponent<Rigidbody>(); // Instantiate���� �߻��� BulletClone ���� ��쵵
        // Inspector���� �����ϴ� �� ������?
        // m_Rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        m_Rigidbody.AddForce(transform.up * BulletSpeed, ForceMode.VelocityChange);


        Debug.Log(transform.position.magnitude.ToString());
    }

    // Update is called once per frame
    void Update()
    {        
    }

    private void FixedUpdate()
    {
        Debug.LogFormat("Velocity: {0}", m_Rigidbody.velocity.magnitude);
        // coroutine ���ٴ� ����� �� ����
        if ((iniPos - gameObject.transform.position).magnitude > 100)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 0)
        {
            Debug.Log(collision.gameObject.name.ToString()); // returns parent
            Debug.Log(collision.collider.gameObject.name.ToString()); // returns child

            // Body�� �پ��ִ� collider�� �̸��� ã�� ���� else if ���ǹ�.
            // ������ Inspector���� ����÷��� ������ Enemy�� Child�� Body�� 
            // prefabȭ ���� �ʾƼ� �Ұ���.
            if (collision.collider.gameObject.name == Body.transform.GetChild(1).gameObject.name)
            {
                GameObject BodyHitted = Instantiate(BodyHitPoint, gameObject.transform.position, Quaternion.identity);
                Destroy(BodyHitted, 1);
                Destroy(gameObject);
            }
            // �׳� gameObject == Head �� �ȵ�.
            else if (collision.collider.gameObject.name == Head.name)
            {
                GameObject HeadHitted = Instantiate(HeadHitPoint, gameObject.transform.position, Quaternion.identity);
                Destroy(HeadHitted, 1);
                Destroy(gameObject);
            }
            
        }
        else if (collision.gameObject.layer == 3)
        {
            GameObject DefaultHitted = Instantiate(DefaultHitPoint, gameObject.transform.position, Quaternion.identity);
            Destroy(DefaultHitted, 1);
            Destroy(gameObject);
        }

    }

}

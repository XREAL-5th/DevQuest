using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour
{

    [SerializeField] float _speed;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * _speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == (int)Define.Layer.Enemy)
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour
{

    [SerializeField] float _speed;
    [SerializeField] GameObject _hitEffect;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * _speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == (int)Define.Layer.Enemy)
        {
            GameObject effect = Instantiate(_hitEffect, collision.transform.position + new Vector3(0.0f, 2.0f, 0.0f), collision.transform.rotation);
            Managers.Resource.Destroy(gameObject);          
        }
    }
}

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == (int)Define.Layer.Enemy)
        {
            GameObject effect = Instantiate(_hitEffect, other.transform.position + new Vector3(0.0f, 2.0f, 0.0f), other.transform.rotation);
            Managers.Resource.Destroy(gameObject);          
        }
    }
}

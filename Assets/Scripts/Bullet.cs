using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float duration = 2f;
    [SerializeField] private int damage = 1;

    private int enemyLayer = 8;

    private void Awake()
    {
        StartCoroutine(CountDuration());
    }
    private IEnumerator CountDuration()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObjcet = collision.gameObject;

        if (collidedObjcet.layer.Equals(enemyLayer))
            collidedObjcet.GetComponent<Enemy>().GetDamage(damage, collision.contacts[0].point);
        Destroy(gameObject);
    }
}

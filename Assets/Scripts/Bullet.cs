using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float duration = 2f;

    private int enemyLayer;

    private void Start()
    {
        enemyLayer = 1 << 8;
    }
    private void Awake()
    {
        StartCoroutine(CountDuration());
    }
    private IEnumerator CountDuration()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject); 
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedObjcet = other.gameObject;

        if(collidedObjcet.layer.Equals(enemyLayer))
        {
            collidedObjcet.GetComponent<Enemy>().GetDamage();
        }
    }
}

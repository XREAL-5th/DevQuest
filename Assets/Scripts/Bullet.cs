using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float duration = 2f;

    private void Awake()
    {
        StartCoroutine(CountDuration());
    }
    private IEnumerator CountDuration()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject); 
    }
}

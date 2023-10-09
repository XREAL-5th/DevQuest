using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCorpse : MonoBehaviour
{
    void Start()
    {
        Destroy(this, 3f);
        foreach (Transform child in transform)
        {
            var rb = child.GetComponent<Rigidbody>();
            print(rb);
            rb.AddExplosionForce(15f, transform.position, 5f, 3f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float bulletspeed;
    void Start()
    {
         GetComponent<Rigidbody>().AddForce(transform.forward * bulletspeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

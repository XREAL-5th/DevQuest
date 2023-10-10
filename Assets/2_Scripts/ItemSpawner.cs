using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    Vector3 pos;
    private float speed = 3;
    private float limit = 4.5f;

    private void Start()
    {
        pos = this.transform.position;
    }
    private void FixedUpdate()
    {
        Vector3 v = pos;

        v.x += limit * Mathf.Sin(Time.time * speed);
        transform.position = v;

    }

}

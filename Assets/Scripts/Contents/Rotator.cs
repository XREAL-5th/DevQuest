using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    float rotateSpeed = 100f;

	void Update ()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.World);

    }
}

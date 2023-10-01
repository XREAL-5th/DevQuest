using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "floor")
        {
            Destroy(gameObject, 3);
        }
        else if(collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItem : MonoBehaviour
{
    public Item item;
    private GameObject vfx;

    private void Start()
    {
        vfx = item.vfx;
        Instantiate(vfx, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<MoveControl>().moveSpeed += 2;
            print("collisoin");

            Destroy(this.gameObject);
        }
    }
}

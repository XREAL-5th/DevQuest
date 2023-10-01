using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public GameObject HitPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        int layerMask = 1 << 0;

        if (collision.gameObject.layer == layerMask)
        {
            Destroy(gameObject);
            Debug.LogFormat("Thisisisi {0}", collision.gameObject.name);
            GameObject hitted = Instantiate(HitPoint, gameObject.transform.position, Quaternion.identity);
            Destroy(hitted, 1);
        }

    }

}

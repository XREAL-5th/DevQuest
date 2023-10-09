using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rpj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.transform.parent == null && other.gameObject.name == "Player")
        {
            for (int i = ItemsSingleton.main.items.Count - 1; i >= 0; i--)
            {
                if (ItemsSingleton.main.items[i].transform == gameObject.transform)
                {
                    ItemsSingleton.main.items.RemoveAt(i);
                    ItemsSingleton.main.itemBitArray[i] = false;
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}

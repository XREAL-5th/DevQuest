using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePortal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            for (int i = ItemsSingleton.main.items.Count - 1; i >= 0; i--)
            {
                if (ItemsSingleton.main.items[i].transform == gameObject.transform)
                {
                    GameEndingSingleton.main.player.transform.position = ItemsSingleton.main.exitPortalPos[Random.Range(0, ItemsSingleton.main.exitPortalPos.Length)].position;
                    ItemsSingleton.main.items.RemoveAt(i);
                    ItemsSingleton.main.itemBitArray[i] = false;
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }

}

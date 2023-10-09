using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public int playermode;
    Item item;

    private void Start()
    {
        playermode = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Item(Clone)")
        {
            item = other.GetComponent<Item>();
            playermode = item.ItemData.Function;
            //Debug.Log(item.ItemData.Function);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerItem : MonoBehaviour
{
    private bool hasItemSpawned = false;
    private GameObject instantiateTemp;
    public int damage;

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
        if (!hasItemSpawned)
        {
            if (other.gameObject.name == "Rifle 2(Clone)")
            {
                instantiateTemp = Instantiate(ItemsSingleton.main.itemsSpawn.Rifle, gameObject.transform.GetChild(1), false);
                damage = ItemsSingleton.main.itemsSpawn.RifleDamage;
                hasItemSpawned = true;
            }
            else if (other.gameObject.name == "RPJ(Clone)")
            {
                instantiateTemp = Instantiate(ItemsSingleton.main.itemsSpawn.Rpj, gameObject.transform.GetChild(1), false);
                damage = ItemsSingleton.main.itemsSpawn.RpjDamage;
                hasItemSpawned = true;
            } 
        } else if (other.gameObject.transform.parent == null)
        {
            if (other.gameObject.name == "Rifle 2(Clone)")
            {
                Destroy(instantiateTemp);
                Instantiate(ItemsSingleton.main.itemsSpawn.Rifle, gameObject.transform.GetChild(1), false);
                damage = ItemsSingleton.main.itemsSpawn.RifleDamage;
            }
            else if (other.gameObject.name == "RPJ(Clone)")
            {
                Destroy(instantiateTemp);
                Instantiate(ItemsSingleton.main.itemsSpawn.Rpj, gameObject.transform.GetChild(1), false);
                damage = ItemsSingleton.main.itemsSpawn.RpjDamage;
            } 
        }
    }
}

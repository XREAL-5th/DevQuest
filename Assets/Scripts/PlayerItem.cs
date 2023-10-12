using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    private GameObject instantiateTemp = null;
    public int damage;

    [SerializeField]
    private Sprite[] aimList = { };
    [SerializeField]
    private GameObject aim;

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
        if (instantiateTemp == null)
        {
            if (other.gameObject.name == "Rifle 2(Clone)")
            {
                instantiateTemp = Instantiate(ItemsSingleton.main.itemsSpawn.Rifle, gameObject.transform.GetChild(1), false);
                damage = ItemsSingleton.main.itemsSpawn.RifleDamage;
                aim.GetComponent<Image>().sprite = aimList[(int)ItemsSingleton.ItemType.Rifle];
                aim.GetComponent<Image>().enabled = true;
            }
            else if (other.gameObject.name == "RPJ(Clone)")
            {
                instantiateTemp = Instantiate(ItemsSingleton.main.itemsSpawn.Rpj, gameObject.transform.GetChild(1), false);
                damage = ItemsSingleton.main.itemsSpawn.RpjDamage;
                aim.GetComponent<Image>().sprite = aimList[(int)ItemsSingleton.ItemType.Rpj];
                aim.GetComponent<Image>().enabled = true;
            }
        } else if (other.gameObject.transform.parent == null)
        {
            if (other.gameObject.name == "Rifle 2(Clone)")
            {
                Destroy(instantiateTemp);
                instantiateTemp = Instantiate(ItemsSingleton.main.itemsSpawn.Rifle, gameObject.transform.GetChild(1), false);
                damage = ItemsSingleton.main.itemsSpawn.RifleDamage;
                aim.GetComponent<Image>().sprite = aimList[(int)ItemsSingleton.ItemType.Rifle];
            }
            else if (other.gameObject.name == "RPJ(Clone)")
            {
                Destroy(instantiateTemp);
                instantiateTemp = Instantiate(ItemsSingleton.main.itemsSpawn.Rpj, gameObject.transform.GetChild(1), false);
                damage = ItemsSingleton.main.itemsSpawn.RpjDamage;
                aim.GetComponent<Image>().sprite = aimList[(int)ItemsSingleton.ItemType.Rpj];
            } 
        }
    }
}

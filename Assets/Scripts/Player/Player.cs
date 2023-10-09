using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int minDamage = 10;
    public int maxDamage = 15;
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
        if(other.tag == "Item")
        {
            ItemData hitObject = other.gameObject.GetComponent<Item>().item;
            if(hitObject != null)
            {
                print("Hit: "+ hitObject.name);
                switch(hitObject.itemType)
                {
                    case ItemData.ItemType.DamageUp:
                        PlayerDamageUp();
                        break;
                    case ItemData.ItemType.DamageDown:
                        PlayerDamageDown();
                        break;
                    default: 
                        break;
                }
                other.gameObject.SetActive(false);
            }
        }
    }

    private void PlayerDamageUp()
    {
        // 최대 공격력 30으로 제한
        if (maxDamage < 30)
        {
            minDamage += 5;
            maxDamage += 5;
        }
        Debug.Log("Min Damage: " + minDamage);
        Debug.Log("Max Damage: " + maxDamage);

    }

    private void PlayerDamageDown()
    {
        // 최소 공격력 5로 제한
        if (minDamage > 5)
        {
            minDamage -= 5;
            maxDamage -= 5;
        }
        Debug.Log("Min Damage: " + minDamage);
        Debug.Log("Max Damage: " + maxDamage);
    }

}

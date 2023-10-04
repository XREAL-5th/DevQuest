using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const int ITEM_LAYER = 9;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(ITEM_LAYER))
        {
            Item item = other.GetComponent<Item>();
            item.EffectPlayer(this);
        }
    }
}

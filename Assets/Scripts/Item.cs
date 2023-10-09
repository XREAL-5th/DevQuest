using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ItemData itemData;
    public ItemData ItemData { get { return itemData; } set { itemData = value; } }

    public void WatchItemInfo()
    {
        //Debug.Log(itemData.ItemName);
        //Debug.Log(itemData.Description);
        //Debug.Log(itemData.Frequency);
        //Debug.Log(itemData.Function);
        //Debug.Log(itemData.EffectVFX);
    }

    
}

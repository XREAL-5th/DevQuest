using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected ItemData itemData;
    public ItemData Data { get { return itemData; } set { itemData = value; } }
    public abstract void EffectPlayer(Player player);
}

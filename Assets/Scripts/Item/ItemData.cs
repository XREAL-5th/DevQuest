using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item")]

public class ItemData : ScriptableObject
{
    public string itemName;
    public GameObject itemPrefab;
    public float damageMultiplier;
    public enum ItemType
    {
        DamageUp,
        DamageDown,
    }
    public ItemType itemType;
}




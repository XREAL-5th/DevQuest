using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable/Item", order = 0)]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName;
    public string Name { get { return itemName; } set { itemName = value; } }

    [SerializeField] private float effectAmount;
    public float Amount { get { return effectAmount; } set { effectAmount = value; } }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "MonsterScriptable/CreateItemData", order = int.MaxValue)]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private string itemName;
    public string ItemName { get { return itemName; } set { itemName = value; } }

    [SerializeField]
    private string description;
    public string Description { get { return description; } set { description = value; } }

    [SerializeField]
    private int frequency;
    public int Frequency { get { return frequency; } set { frequency = value; } }

    [SerializeField]
    private int function;
    public int Function { get { return function; } set { function = value; } }

    [SerializeField]
    private string effectVFX;
    public string EffectVFX { get { return effectVFX; } set { effectVFX = value; } }
}

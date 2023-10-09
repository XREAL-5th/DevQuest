using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemData", menuName = "ItemScriptable/CreateItemData", order = int.MaxValue)]
public class ItemData : ScriptableObject
{

    [SerializeField]
    private float speedup;
    public float Speed { get { return speedup; } set { speedup = value; } }

    [SerializeField]
    private string ItemName;
    public string Name { get { return ItemName; } set { ItemName = value; } }

    [SerializeField]
    private string ItemInfo;
    public string Info { get { return ItemInfo; } set { ItemInfo = value; } }
}

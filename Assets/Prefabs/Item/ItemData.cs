using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemScriptable/CreateItemData", order = int.MaxValue)]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private float jumpstrength;
    public float JumpStrength { get { return jumpstrength; } set { jumpstrength = value; } }

    [SerializeField]
    private string itemName;
    public string Name { get { return itemName; } set { itemName = value; } }

    [SerializeField]
    private int itemIndex;
    public int Index { get { return itemIndex; } set { itemIndex = value; } }

    [SerializeField]
    private float speed;
    public float Speed { get { return speed; } set { speed = value; } }

    [SerializeField]
    private float heal;
    public float HealAmount { get { return heal; } set { heal = value; } }

    [SerializeField]
    private Color color;
    public Color Color { get { return color; } set { color = value; } }

}

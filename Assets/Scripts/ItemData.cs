using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemScriptable/ItemData", order = int.MaxValue)]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private string itemName;
    public string ItemName { get { return itemName; } set {  itemName = value; } }
    [SerializeField]
    private Color color;
    public Color Color { get { return color; } set {  color = value; } }
    [SerializeField]
    private float jump;
    public float Jump { get { return jump; } set { jump = value; } }
    [SerializeField]
    private float speed;
    public float Speed { get { return speed; } set {  speed = value; } }
}

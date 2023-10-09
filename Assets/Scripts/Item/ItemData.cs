using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Item")]
public class ItemData: ScriptableObject
{
    public enum Type
    {
        Soju,
        Pill
    }
    public Type type;
    public GameObject prefab;
}

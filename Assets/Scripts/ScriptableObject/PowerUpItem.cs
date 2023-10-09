using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpItem", menuName = "ScriptableObjects/PowerUpItem", order = 1)]
public class PowerUpItem : ScriptableObject
{
    public string itemName;
    public string description;
    public float playerEffectValue;
    public float effectDuration;
    public GameObject itemVFX; // Visual effect when player picks up item
}
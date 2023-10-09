using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private PowerUpItem powerUpItem;

    public void Initialize(PowerUpItem item)
    {
        this.powerUpItem = item;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyEffect();
            Destroy(gameObject);
        }
    }

    private void ApplyEffect()
    {
        if (powerUpItem.itemName == "Damage Booster")
        {
            MoveControl player = FindObjectOfType<MoveControl>();
            player.BoostDamage(powerUpItem.playerEffectValue, powerUpItem.effectDuration);
        }

        if (powerUpItem.itemVFX)
        {
            Instantiate(powerUpItem.itemVFX, transform.position, Quaternion.identity);
        }
    }
}

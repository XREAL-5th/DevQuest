using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkItem : Item
{
    [SerializeField] private ItemData shirinkItemData;

    private void Awake()
    {
        itemData = shirinkItemData;
    }
    public override void EffectPlayer(Player player)
    {
        Vector3 playerTransform = player.transform.localScale;
        player.transform.localScale = new Vector3(playerTransform.x, playerTransform.y, playerTransform.z) * itemData.Amount;
        gameObject.SetActive(false);
    }
}

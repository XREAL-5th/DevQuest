using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleItem : Item
{
    [SerializeField] private ItemData scaleItemData;

    private void Awake()
    {
        itemData = scaleItemData;
    }
    public override void EffectPlayer(Player player)
    {
        Debug.Log($"{itemData.Name}�� �������� ũ�Ⱑ ���մϴ�!");
        Vector3 playerTransform = player.transform.localScale;
        player.transform.localScale = new Vector3(playerTransform.x, playerTransform.y, playerTransform.z) * itemData.Amount;
        gameObject.SetActive(false);
    }
}

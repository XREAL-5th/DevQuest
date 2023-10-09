using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField]
    ItemData itemData;
    public ItemData ItemData { set { itemData = value; } }

    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = itemData.Color;
    }
    public void WatchItemInfo()
    {
        Debug.Log(itemData.Name);
        Debug.Log(itemData.HealAmount);
        Debug.Log(itemData.Speed);
        Debug.Log(itemData.JumpStrength);
    }

    public void ItemUsed()
    {
        GameManager.main.player.currentHp += itemData.HealAmount;
        if (GameManager.main.player.currentHp > GameManager.main.player.healthPoint)
            GameManager.main.player.currentHp = GameManager.main.player.healthPoint;
        GameManager.main.player.moveSpeed += itemData.Speed;
        GameManager.main.player.jumpAmount += itemData.JumpStrength;
        SpawnItem.main.SpawnItemObject(itemData.Index, gameObject.transform.parent.gameObject);
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            ItemUsed();
        }
    }
}

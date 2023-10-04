using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    ItemData itemData;
    public ItemData ItemData { set { itemData = value; } }

    public void ItemInfo()
    {
        Debug.Log(this.itemData.name);
        Debug.Log(this.itemData.Jump);
        Debug.Log(this.itemData.Color);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<MeshRenderer>().material.color = itemData.Color;
            other.GetComponent<MoveControl>().jumpAmount = itemData.Jump;
            other.GetComponent<MoveControl>().moveSpeed = itemData.Speed;
        }
    }
}

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemManager : MonoBehaviour
{
    public static ItemManager itemManager;

    public enum ItemType { Speed, Strong, TimeControl };
    public List<ItemData> ItemDatas = new List<ItemData>();

    public GameObject ItemPrefab;

    private void Awake()
    {
        if (itemManager == null)
        {
            itemManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SpawnItemObject();
    }

    public void SpawnItemObject()
    {
        for (int i = 0; i < ItemDatas.Count; i++)
        {
            var item = SpawnItemFunc((ItemType)i, new Vector3(i*35,1,i*-25));
            item.WatchItemInfo();
        }
    }

    public Item SpawnItemFunc(ItemType type, Vector3 itemPosition)
    {
        var newItem = Instantiate(ItemPrefab, itemPosition, Quaternion.identity).GetComponent<Item>();
        newItem.ItemData = ItemDatas[(int)(type)];
        return newItem;
    }
}
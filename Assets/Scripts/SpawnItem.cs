using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnItem : MonoBehaviour
{
    public static SpawnItem main;

    public enum ItemType { HealPack, JumpStrength, SpeedUp };
    public List<ItemData> itemDatas = new List<ItemData>();
    public List<GameObject> Spawners = new List<GameObject>();

    public GameObject itemPrefab;


    private void Start()
    {
        main = this;
        Spawners = new List<GameObject>(GameObject.FindGameObjectsWithTag("ItemSpawner"));
        for(int i = 0; i < Spawners.Count; i++)
        {
            GameObject spawner = Spawners[i];
            SpawnItemFunc((ItemType)(i%3),spawner);
        }
    }

    public void SpawnItemObject(int itemIndex, GameObject spawner)
    {
        StartCoroutine(SpawnCoolDown(itemIndex, spawner));
    }

    public Item SpawnItemFunc(ItemType type, GameObject spawner)
    {
        var newItem = Instantiate(itemPrefab, new Vector3(spawner.transform.position.x, spawner.transform.position.y + 0.8f, spawner.transform.position.z), Quaternion.identity).GetComponent<Item>();
        newItem.ItemData = itemDatas[(int)(type)];
        newItem.transform.parent = spawner.transform;
        return newItem;
    }

    IEnumerator SpawnCoolDown(int itemIndex, GameObject spawner)
    {
        yield return new WaitForSeconds(3f);
        var item = SpawnItemFunc((ItemType)itemIndex, spawner);
    }
}

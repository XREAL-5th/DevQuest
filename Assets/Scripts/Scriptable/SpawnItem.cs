using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
   [Header("Scriptable Object")]
   public List<ItemData> itemDatas = new List<ItemData>();
   [Header("Item")]
   public GameObject[] itemPrefabs;
   [Header("Item Spawn")]
   public Transform[] spawnPoints; // 서로 다른 위치에 배치할 때 사용할 위치 배열
    
   Dictionary<int, int> itemToSpawnPointMap = new Dictionary<int, int>();

    private void Start()
    {
        SpawnItemObject();
    }

    public void SpawnItemObject()
    {
        // 아이템 프리팹과 스폰 포인트를 매핑
        itemToSpawnPointMap.Add(0, 0);
        itemToSpawnPointMap.Add(1, 1);
        itemToSpawnPointMap.Add(2, 2);
        itemToSpawnPointMap.Add(3, 3);

        for (int i = 0; i < itemDatas.Count; i++)
        {
            var item = SpawnItemFunc((ItemData.ItemEffectType)i, itemToSpawnPointMap[i]);
        }
    }

    public Item SpawnItemFunc(ItemData.ItemEffectType type, int spawnPointIndex)
    {
        // var newItem = Instantiate(itemDatas[(int)type].itemPrefab).GetComponent<Item>();
        
        //int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        //Transform spawnPoint = spawnPoints[spawnPointIndex];

        Transform spawnPoint = spawnPoints[spawnPointIndex];

        var newItem = Instantiate(itemPrefabs[(int)type], spawnPoint.position, Quaternion.identity).GetComponent<Item>();
        newItem.ItemData = itemDatas[(int)(type)];
        return newItem;
    }
}

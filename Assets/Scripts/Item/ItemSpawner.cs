using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : LazySingletonMonoBehavior<ItemSpawner>
{
    public List<ItemData> items;
    public int spawnCnt = 4;
    public Vector3 spawnArea = new(10f, 1f, 10f);

    void Start()
    {
        SpawnEntities();
    }

    void SpawnEntities()
    {
        foreach (var _ in Enumerable.Range(0, spawnCnt))
        {
            foreach (var item in items)
            {
                Vector3 randomPosition = new Vector3(
                    Random.Range(-spawnArea.x, spawnArea.x),
                    Random.Range(0, spawnArea.y),
                    Random.Range(-spawnArea.z, spawnArea.z)
                ) + transform.position;

                var obj = Instantiate(item.prefab, randomPosition, Quaternion.identity);
                var itemComponent = obj.GetComponent<ItemComponent>();
                if (itemComponent == null)
                {
                    // should not happen
                    Destroy(obj);
                    continue;
                }
                itemComponent.type = item.type;
            }
        }
    }
}
using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public GameObject entityToSpawn;

    int instanceNumber = 1;

    public enum PotionType { Small, Big, Advanced };
    public List<PotionData> spawnDatas = new List<PotionData>();

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        for (int i = 0; i < spawnDatas.Count; i++)
        {
            SpawnEntities(spawnDatas[i]);
        }
    }

    public void SpawnEntities(PotionData spawnManagerValues)
    {
        int currentSpawnPointIndex = 0;
        
        for (int i = 0; i < spawnManagerValues.numberOfPrefabsToCreate; i++)
        {
            var currentEntity = Instantiate(entityToSpawn, spawnManagerValues.spawnPoints[currentSpawnPointIndex], Quaternion.identity);
            Potion potion = currentEntity.GetComponent<Potion>();
            potion.PotionData = spawnManagerValues;
            potion.name = spawnManagerValues.prefabName + instanceNumber;
            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnManagerValues.spawnPoints.Length;
            instanceNumber++;
        }
    }

}

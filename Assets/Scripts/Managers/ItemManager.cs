using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    [SerializeField] private List<GameObject> itemPrefabs = new List<GameObject>();
    [SerializeField] private PowerUpItem defaultPowerUpItem;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnPowerUpItem(Vector3 position)
    {
        GameObject powerUpGO = Instantiate(itemPrefabs[0], position, Quaternion.identity); // itemPrefabs[0] == PowerUpItem
        PowerUp powerUp = powerUpGO.AddComponent<PowerUp>();
        powerUp.Initialize(defaultPowerUpItem);

        SphereCollider collider = powerUpGO.AddComponent<SphereCollider>();
        collider.isTrigger = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
// using UnityEditor.Build;
using System;
using UnityEngine;
using System.Linq;

public class ItemsSingleton : MonoBehaviour
{
    public static ItemsSingleton main;

    [HideInInspector]
    public List<GameObject> items = new List<GameObject>();

    public Transform[] itemPos = { };
    public Transform[] exitPortalPos = { };

    public BitArray itemBitArray = new BitArray(6, false); // itemPos.Length -> Error 

    public Items itemsSpawn; // 대신에 static이 가능한가?

    private bool coroutineAllowed = true;

    public enum ItemType { Rifle, Rpj, BluePortal};
    private Dictionary<ItemType, GameObject> itemTypeToPrefabMap;

    private void Awake()
    {
        main = this;
        itemTypeToPrefabMap = new Dictionary<ItemType, GameObject>
        {
            {ItemType.Rifle, itemsSpawn.Rifle },
            {ItemType.Rpj, itemsSpawn.Rpj },
            {ItemType.BluePortal, itemsSpawn.BluePortal }
        };
    }

    // Start is called before the first frame update
    void Start()
    {         
        foreach (ItemType typeOfItem in Enum.GetValues(typeof(ItemType)))
        {
            int index = UnityEngine.Random.Range(0, itemPos.Length);
            while (itemBitArray[index] == true)
                index = UnityEngine.Random.Range(0, itemPos.Length);
            items.Add(Instantiate(itemTypeToPrefabMap[typeOfItem], itemPos[index].position + new Vector3(0, 1.0f, 0), itemPos[index].rotation));
            itemBitArray[index] = true;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(items.Count);
        
        if (items.Count < 3 && coroutineAllowed)
        {
            StartCoroutine(itemSpawn());
        }
    }

    /*
    public GameObject SpawnItemsFunc(ItemType type, Transform where)
    {   
        
        GameObject prefab = null;
        GameObject newItem;

        foreach (ItemType typeOfItem in Enum.GetValues(typeof(ItemType))) {
            if (type == typeOfItem)
            { 
                string prefabName = typeOfItem.ToString();
                Debug.Log(prefabName);
                System.Reflection.FieldInfo prefabField = itemsSpawn.GetType().GetField(prefabName); // to get it by String value
                if (prefabField != null)
                {
                    Debug.Log("?");
                    prefab = prefabField.GetValue(itemsSpawn) as GameObject;
                }
                break;
            }
        }

        if (prefab != null)
        {
            newItem = Instantiate(prefab, where.position + new Vector3(0, 1.0f, 0), where.rotation);
            return newItem;
        } else
        {
            return null;
        }
    }
    */

    private IEnumerator itemSpawn()
    {
        coroutineAllowed = false;

        yield return new WaitForSeconds(3);

        int itemType = UnityEngine.Random.Range(0, 3);
        int index = UnityEngine.Random.Range(0, itemPos.Length);

        while (itemBitArray[index] == true) // Array Shuffle (Fisher-Yates O(N) )
        {
            index = UnityEngine.Random.Range(0, itemPos.Length);
        }

        itemBitArray[index] = true;
        items.Add(Instantiate(itemTypeToPrefabMap[(ItemType)itemType], itemPos[index].position + new Vector3(0, 1.0f, 0), itemPos[index].rotation));

        coroutineAllowed = true;
    }
}

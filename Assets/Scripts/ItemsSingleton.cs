using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsSingleton : MonoBehaviour
{
    public static ItemsSingleton main;

    [HideInInspector]
    public List<GameObject> items = new List<GameObject>();

    public Transform[] itemPos = { };
    public Transform[] exitPortalPos = { };

    public BitArray itemBitArray = new BitArray(6, false); // itemPos.Length -> Error 

    public Items itemsSpawn; // 대신에 static이 가능한가?

    public enum ItemType { Rifle, Rpj, Portal};

    private void Awake()
    {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        int itemType = 0;
        while (count < 3)
        {
            int index = Random.Range(0, itemPos.Length);
            if (itemBitArray[index] == true)
                continue;
            items.Add(SpawnItemsFunc((ItemType)itemType, itemPos[index]));
            itemBitArray[index] = true;
            itemType++;
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(items.Count);
        
        if (items.Count < 3)
        {
            StartCoroutine(itemSpawn());
        }
    }

    public GameObject SpawnItemsFunc(ItemType type, Transform where)
    {
        GameObject newItem;

        if (type == ItemType.Rifle)
        {
            newItem = Instantiate(itemsSpawn.Rifle, where.position + new Vector3(0, 1.0f ,0), where.rotation);
        } else if (type == ItemType.Rpj)
        {
            newItem = Instantiate(itemsSpawn.RPJ, where.position + new Vector3(0, 1.0f, 0), where.rotation);
        } else
        {
            newItem = Instantiate(itemsSpawn.BluePortal, where.position + new Vector3(0, 1.0f, 0), where.rotation);
        }
        return newItem;
    }

    private IEnumerator itemSpawn()
    {
        yield return new WaitForSeconds(3);

        int itemType = Random.Range(0, 3);
        int index = Random.Range(0, itemPos.Length);

        while (itemBitArray[index] == true) // 더 효율적인 방법은 없을까?
        {
            index = Random.Range(0, itemPos.Length);
        }

        itemBitArray[index] = true;
        items.Add(SpawnItemsFunc((ItemType)itemType, itemPos[index]));
    }
}

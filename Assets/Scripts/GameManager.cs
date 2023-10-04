using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public enum Spawner
    {
        ItemName,
        Color,
        JumpItem,
    }
    public List<ItemData> itemDatas = new List<ItemData>();

    [SerializeField] private GameObject coinText;
    private float spawnTime = 0.0f;
    private int itemCount = 0;
    private bool spawnStop = false;

    public Transform itemPos;
    public static Transform enemyPos;

    public static int coinCount;

    public List<GameObject> coins = new List<GameObject>();

    public GameObject itemPrefab;
    // ---½Ì±ÛÅæÀ¸·Î ¼±¾ð--- //
    private static GameManager instance;
    public static GameManager Instance
    {
        get 
        { 
            if (instance == null)
            {
                return null;
            }
            return instance; 
        }
    }
    
    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Update()
    {
        if (coinCount >= 5 || Input.GetKeyDown(KeyCode.V))
        {
            SceneManager.LoadScene(1);
        }
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            coinText.GetComponent<TextMeshProUGUI>().text = "Coin Count : " + coinCount;
            Debug.Log(coinCount);
            ItemSpawn();
        }
    }

    public void CoinDrop(Vector3 position, Quaternion rotation)
    {

        Instantiate(coins[coinCount],position,rotation);
    }
    void ItemSpawn()
    {
        if (!spawnStop)
        {
            spawnTime += Time.deltaTime * 1.0f;

            if (spawnTime >= 3.0f)
            {
                var monster = SpawnItem((Spawner)Random.Range(0, 3));
                monster.ItemInfo();
                itemCount++;
                spawnTime = 0.0f;
            }
            else if (itemCount >= 5) spawnStop = true;
        }

    }

    public Item SpawnItem(Spawner type)
    {
        var newItem = Instantiate(itemPrefab,itemPos.position,Quaternion.identity).GetComponent<Item>();
        newItem.ItemData = itemDatas[(int)(type)];
        return newItem;
    }
}

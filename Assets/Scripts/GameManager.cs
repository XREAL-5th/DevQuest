using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;
using System.Threading;

public class GameManager : MonoBehaviour
{
    #region setting
    public enum Spawner
    {
        ItemName,
        Color,
        Jump,
        Speed
    }
    public List<ItemData> itemDatas = new List<ItemData>();

    [SerializeField] private GameObject coinText;
    private float spawnTime = 0.0f;

    public Transform itemPos;
    public Transform enemyPos;

    public int damage = 10;

    public static int coinCount;

    public List<GameObject> coins = new List<GameObject>();
    public Queue<Item> items = new Queue<Item>();

    public GameObject itemPrefab;
    // ---싱글톤으로 선언--- //
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
    #endregion
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
        if (coinCount >= 5 || Input.GetKeyDown(KeyCode.V))          //씬 전환
        {
            SceneManager.LoadScene(1);
        }
        if (SceneManager.GetActiveScene().buildIndex == 0)          //씬 전환 시 에러 방지
        {
            coinText.GetComponent<TextMeshProUGUI>().text = "Coin Count : " + coinCount;
            Debug.Log(coinCount);
            StartCoroutine(ItemSpawn());
            //ItemSpawn();
        }
    }

    public void CoinDrop(Vector3 position, Quaternion rotation)     //적 처치 시 코인 생성
    {

        Instantiate(coins[coinCount],position,rotation);
    }
    //void ItemSpawn()          //아이템 생성 (Update)
    //{
    //    if (!spawnStop)
    //    {
    //        spawnTime += Time.deltaTime * 1.0f;

    //        if (spawnTime >= 3.0f)
    //        {
    //            var item = SpawnItem((Spawner)Random.Range(0, 3));
    //            item.ItemInfo();
    //            itemCount++;
    //            spawnTime = 0.0f;
    //        }
    //        else if (itemCount >= 5) spawnStop = true;
    //    }

    //}
    IEnumerator ItemSpawn()             //아이템 생성 코루틴
    {
        spawnTime += Time.deltaTime;
        while (spawnTime >= 2.0f)
        {
            if (items.Count <= 5)
            {
                var item = SpawnItem((Spawner)Random.Range(0, 4));
                items.Enqueue(item);
                Debug.Log("생성");
                //item.ItemInfo();
                spawnTime = 0.0f;
            }
            else yield return new WaitForSeconds(2.0f);
        }

    }
    public Item SpawnItem(Spawner type)
    {
        var newItem = Instantiate(itemPrefab,itemPos.position,Quaternion.identity).GetComponent<Item>();
        newItem.ItemData = itemDatas[(int)(type)];
        return newItem;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMGR : MonoBehaviour
{
    
    public static ItemMGR main;
    public enum ItemType { Fast, Slow };
    public List<ItemData> ItemDatas = new List<ItemData>();

    public GameObject ItemPrefab;


    public List<Transform> ItemSpawn = new List<Transform>();

    [HideInInspector] public MoveControl moveControl;


    private void Awake()
    {
        //싱글톤 설정
        main = this;

        //moveControl = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveControl>();
        //이 함수가 여기 들어가면 prefab 생성이 작동을 하지 않습니다. foreach 뒤로 내리니 정상작동하는데 왜그런건가요?


        // ItemSpawn 리스트의 위치에 ItemPrefab을 생성
        foreach (Transform spawnPoint in ItemSpawn)
        {
            // 아이템 데이터 중에서 무작위로 선택하거나 원하는 방식으로 선택
            int randomIndex = Random.Range(0, ItemDatas.Count);
            ItemData selectedData = ItemDatas[randomIndex];

            // 아이템 프리팹을 생성하고 위치 설정
            GameObject newItem = Instantiate(ItemPrefab, spawnPoint.position, ItemPrefab.transform.rotation);
            Item itemComponent = newItem.GetComponent<Item>();
            itemComponent.ItemData = selectedData;
            itemComponent.WatchInfo();
        }

        moveControl = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveControl>();

    }

    public void GetspeedItem(int value)
    {
        moveControl.moveSpeed += value;
    }

    
}

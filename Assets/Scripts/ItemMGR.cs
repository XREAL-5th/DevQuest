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
        //�̱��� ����
        main = this;

        //moveControl = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveControl>();
        //�� �Լ��� ���� ���� prefab ������ �۵��� ���� �ʽ��ϴ�. foreach �ڷ� ������ �����۵��ϴµ� �ֱ׷��ǰ���?


        // ItemSpawn ����Ʈ�� ��ġ�� ItemPrefab�� ����
        foreach (Transform spawnPoint in ItemSpawn)
        {
            // ������ ������ �߿��� �������� �����ϰų� ���ϴ� ������� ����
            int randomIndex = Random.Range(0, ItemDatas.Count);
            ItemData selectedData = ItemDatas[randomIndex];

            // ������ �������� �����ϰ� ��ġ ����
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

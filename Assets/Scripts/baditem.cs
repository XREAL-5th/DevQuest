using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baditem : MonoBehaviour
{
    // public ItemSO itemData; // 아이템 정보를 저장한 Scriptable Object
    public BadItemSO baditemData; // 아이템 정보를 저장한 Scriptable Object


    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.CompareTag("Player"))
        {
            Debug.Log("====충돌(in item)");
            Player player = other.GetComponent<Player>();
            Item item = other.GetComponent<Item>();

            
            // 아이템 효과 적용
            Debug.Log("====EatItem(in item)");
            player.EatItem(item.itemData);

            // 아이템 오브젝트 삭제 또는 비활성화
            // gameObject.SetActive(false);
            Destroy(gameObject);

            // 아이템을 먹었을 때 생성되는 VFX 등 추가 작업 수행 가능
        }
        */
    }
    public void destroyItem()
    {
        Destroy(gameObject);
    }
}

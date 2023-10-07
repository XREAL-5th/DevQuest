using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int attackPower = 10; // 초기 공격력
    private Player player;
    public ItemSO itemData; // 아이템 정보를 저장한 Scriptable Object



    private void Start()
    {
        SetAttackDamage(attackPower);
    }

    public void SetAttackDamage(int Power)
    {
        attackPower = Power;
    }


    public int GetAttackPower()
    {
        return player.attackPower;
    }



    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체가 아이템인지 확인
        Debug.Log("====충돌(in player)");
        Item item = other.GetComponent<Item>();
        if (item != null && item.itemData != null)
        {
            Debug.Log("null 아님");
            // 아이템을 먹음 (EatItem 함수 호출)
            EatItem(item); 
        }
        else
        {
            Debug.Log("null로 인식함");
        }
        
    }
    

    // 아이템을 먹을 때 호출되는 함수
    public void EatItem(Item item)
    {
        // 아이템의 효과를 적용
        attackPower *= (int)item.itemData.attackPowerMultiplier;
        Debug.Log("공격력 2배");
        Debug.Log("공격력 : "+ attackPower);


        item.destroyItem();
        Debug.Log("아이템 삭제");

        if (item.itemData.vfxPrefab != null)
        {
            // 아이템 먹었을 때 생성되는 효과 재생
            Instantiate(itemData.vfxPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("vfxPrefab이 null입니다.");
        }
    }

}

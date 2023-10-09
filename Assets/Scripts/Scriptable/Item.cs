using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    ItemData itemData;
    public ItemData ItemData { set { itemData = value; } }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 Player인지 확인
        if (other.CompareTag("Player"))
        {
            ApplyItemEffect(other.GetComponent<PlayerState>());
            
            // 아이템을 획득했으므로 해당 오브젝트 비활성화 또는 제거
            //gameObject.SetActive(false);
             Destroy(gameObject);
        }
    }

    private void ApplyItemEffect(PlayerState playerState)
    {
        // itemData에 따라서 플레이어 상태를 업데이트
        switch (itemData.itemEffectType)
        {
            case ItemData.ItemEffectType.IncreaseHP:
                playerState.PlusHP += itemData.ItemValue;
                break;

            case ItemData.ItemEffectType.IncreaseTime:
                playerState.RemainTime += itemData.ItemValue;
                GameControl.Instance.EscapePortal.SetActive(true);
                break;

            case ItemData.ItemEffectType.EnableAttack:
                // 공격을 가능하게 하는 효과 부여한다.
                playerState.IsAttack = true;
                break;

            // 여기에 추가적인 아이템 효과 타입을 처리하는 코드를 작성할 수 있음

            default:
                break;
        }

    }




    //public void DebugItemInfo()
    //{
    //    Debug.Log(itemData.ItemName);
    //    Debug.Log(itemData.itemEffectType);
    //    Debug.Log(itemData.ItemValue);
    //} 
}

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
        // �浹�� ������Ʈ�� Player���� Ȯ��
        if (other.CompareTag("Player"))
        {
            ApplyItemEffect(other.GetComponent<PlayerState>());
            
            // �������� ȹ�������Ƿ� �ش� ������Ʈ ��Ȱ��ȭ �Ǵ� ����
            //gameObject.SetActive(false);
             Destroy(gameObject);
        }
    }

    private void ApplyItemEffect(PlayerState playerState)
    {
        // itemData�� ���� �÷��̾� ���¸� ������Ʈ
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
                // ������ �����ϰ� �ϴ� ȿ�� �ο��Ѵ�.
                playerState.IsAttack = true;
                break;

            // ���⿡ �߰����� ������ ȿ�� Ÿ���� ó���ϴ� �ڵ带 �ۼ��� �� ����

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

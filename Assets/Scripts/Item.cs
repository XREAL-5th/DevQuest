using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField]
	ItemData itemData;
	public ItemData ItemData { set { itemData = value; } }

	public void WatchInfo()
	{
		Debug.Log(itemData.Name);
		Debug.Log(itemData.Info);
		Debug.Log(itemData.Speed);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
			if (itemData.Name == "���󵵳�")
			{
				ItemMGR.main.GetspeedItem((int)itemData.Speed);
				Debug.Log("���󵵳��� �Ծ����ϴ�!");
				Destroy(gameObject);
			}
			else if (itemData.Name == "��������..��")
			{
				ItemMGR.main.GetspeedItem(-(int)itemData.Speed);
				Debug.Log("��������..���� �Ծ����ϴ�!");
				Destroy(gameObject);
			}
			//Debug.Log("�浹");
			
		}

	}
}

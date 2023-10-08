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
			if (itemData.Name == "»¡¶óµµ³Ó")
			{
				ItemMGR.main.GetspeedItem((int)itemData.Speed);
				Debug.Log("»¡¶óµµ³ÓÀ» ¸Ô¾ú½À´Ï´Ù!");
				Destroy(gameObject);
			}
			else if (itemData.Name == "´À·ÁÁøµµ..³Ó")
			{
				ItemMGR.main.GetspeedItem(-(int)itemData.Speed);
				Debug.Log("´À·ÁÁøµµ..³ÓÀ» ¸Ô¾ú½À´Ï´Ù!");
				Destroy(gameObject);
			}
			//Debug.Log("Ãæµ¹");
			
		}

	}
}

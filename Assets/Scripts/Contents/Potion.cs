using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
	[SerializeField]
	PotionData potionData;
	public PotionData PotionData { set { potionData = value; } }

	public void WatchPotionInfo()
	{
		Debug.Log(potionData.Hp);
		Debug.Log(potionData.Attack);
		Debug.Log(potionData.Defense);
		Debug.Log(potionData.MaxHp);
	}
}
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

	private void OnTriggerEnter(Collider _other)
	{
		if (_other.gameObject.layer == (int)Define.Layer.Player)
		{
			GameObject effect = Instantiate(potionData.Effect, transform.position, transform.rotation);
			_other.GetComponent<PlayerControl>().useStatItem(potionData.Hp, potionData.MaxHp, potionData.Attack, potionData.Defense);
			Managers.Resource.Destroy(gameObject);
			//Managers.Resource.Destroy(effect);
		}
	}
}
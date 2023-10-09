using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public string itemName; // 아이템 이름
    public string itemDescription; // 아이템 설명

    public float attackPowerMultiplier = 2; // 공격력 배수
    public GameObject vfxPrefab; // 아이템 먹었을 때 생성되는 효과 프리팹
    public GameObject iconPrefab; // 아이템 아이콘 프리팹 (UI 등에서 사용)
}

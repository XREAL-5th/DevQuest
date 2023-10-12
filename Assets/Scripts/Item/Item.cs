using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "New Item/item")]
public class Item : ScriptableObject  // ���� ������Ʈ�� ���� �ʿ� X 
{
    public string itemName; // �������� �̸�
    public GameObject itemPrefab;  // �������� ������ (������ ������ ���������� ��)
    public GameObject vfx;

}

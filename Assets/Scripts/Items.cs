using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemScriptable/CreateItemData",
    order = int.MaxValue)] // to the last order
public class Items : ScriptableObject
{
    // [SerializeField]가 없으면 public이어도 inspector에서 안 보임.
    [SerializeField]
    private GameObject rifle;
    public GameObject Rifle { get { return rifle; } set { rifle = value; } }
    [SerializeField]
    private int rifleDamage;
    public int RifleDamage { get { return rifleDamage; } set {  rifleDamage = value; } }

    [SerializeField]
    private GameObject rpj;
    public GameObject RPJ { get { return rpj; } set {  rpj = value; } }
    [SerializeField]
    private int rpjDamage;
    public int RpjDamage { get { return rpjDamage; } set { rpjDamage = value; } }

    [SerializeField]
    private GameObject bluePortal;
    public GameObject BluePortal { get { return bluePortal; } set { bluePortal = value; } }

}

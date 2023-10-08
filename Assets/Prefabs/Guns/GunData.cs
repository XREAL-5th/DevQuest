using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "GunScriptable/CreateGunData", order = int.MaxValue)]
public class GunData : ScriptableObject
{
    [SerializeField]
    private float damage;
    public float Damage { get { return damage; } set { damage  = value; } }

    [SerializeField]
    private string gunName;
    public string Name { get { return gunName; } set { gunName = value; } }

    [SerializeField]
    private float speed;
    public float Speed { get { return speed; } set { speed = value; } }
}

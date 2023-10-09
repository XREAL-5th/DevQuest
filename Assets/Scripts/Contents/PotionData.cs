using UnityEngine;

[CreateAssetMenu(fileName = "PotionData", menuName = "Scriptable Object/Potion Data", order = int.MaxValue)]
public class PotionData : ScriptableObject
{
    public string prefabName;
    public int numberOfPrefabsToCreate;
    public Vector3[] spawnPoints;

    [SerializeField] protected int _hp;
    [SerializeField] protected int _maxHp;
    [SerializeField] protected int _attack;
    [SerializeField] protected int _defense;
    [SerializeField] protected GameObject _effect;

    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }

    public GameObject Effect{ get { return _effect; } set { _effect = value; } }
}
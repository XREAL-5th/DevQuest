using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField] protected int _exp;

    public int Exp { get { return _exp; } set { _exp = value; } }

    private void Start()
    {
        _hp = 100;
        _maxHp = 100;
        _attack = 40;
        _defense = 5;
        _exp = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text Enemy;
    [HideInInspector] public int enemy_count;

    private void Start()
    {
        //_main = GetComponent<GameMain>();
        //enemy_count = _main.enemy_count;
        //enemy_count = GameMain.main.enemy_count;
    }

    void Update()
    {
        enemy_count = GameMain.main.enemy_count;
        if (enemy_count != 0)
        {
            Enemy.text = "Remaining Enemy: " + enemy_count;
        }
        else
        {
            Enemy.text = "Killed All";
        }
        //Debug.Log(enemy_count);
    }
}

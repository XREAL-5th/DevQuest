using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    //private GameObject _mainObject;
    //private GameMain _main;
    public Text GameTimer;
    [HideInInspector] public float game_timer;


    void Update()
    {
        game_timer = GameMain.main.gameTimer;
        GameTimer.text = "Remaining time: " + game_timer.ToString("N1") + " sec";
        if (game_timer >= 0f)
        {
            GameTimer.text = "Remaining time: " + game_timer.ToString("N1") + " sec";
        }
        else
        {
            GameTimer.text = "Game Over";
        }
        //Debug.Log(_main.gameTimer);
    }
}

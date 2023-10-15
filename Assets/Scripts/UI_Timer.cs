using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    //private GameObject _mainObject;
    //private GameMain _main;
    public Text Text;
    public bool isGameTimer;
    public bool isSkillTimer;
    private float skillCoolTime;
    private float skillCoolLeftTime;
    [HideInInspector] public float game_timer;

    void Start()
    {
        skillCoolTime = 5f;
    }


    void Update()
    {
        if (isGameTimer)
        {
            game_timer = GameMain.main.gameTimer;
            Text.text = "Remaining time: " + game_timer.ToString("N1") + " sec";
            if (game_timer >= 0f)
            {
                Text.text = "Remaining time: " + game_timer.ToString("N1") + " sec";
            }
            else
            {
                Text.text = "Game Over";
            }
        }

        else if(isSkillTimer)
        {
            if (GameMain.main.isSkillOn == true)
            {
                Text.text = "Skill On";
                skillCoolLeftTime = 5f;
            }
            else
            {
                skillCoolLeftTime = skillCoolLeftTime - Time.deltaTime;
                Text.text = "Skill OFF / Left Time: " + skillCoolLeftTime.ToString("N0") + " sec";
            }
        }


        //Debug.Log(_main.gameTimer);
    }
}

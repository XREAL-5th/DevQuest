using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnemyHealth : MonoBehaviour
{
    private Image healthBar;
    public int thisEnemyNum = 0;

    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = GameMain.main.enemy_health_list[thisEnemyNum] / 3f;
        //healthBar.fillAmount = GameMain.main.player.playerCurrentHeath / 3f;
        healthBar.color = Color.green;
        //Debug.Log(GameMain.main.enemy_health_list[thisEnemyNum] / 3f);
    }
}

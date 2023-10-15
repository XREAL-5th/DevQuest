using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealth : MonoBehaviour
{
    private Image playerHealthBar;

    void Start()
    {
        playerHealthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthBar.fillAmount = GameMain.main.playerHealth / 3f;
        //healthBar.fillAmount = GameMain.main.player.playerCurrentHeath / 3f;
        playerHealthBar.color = Color.red;
        //Debug.Log(GameMain.main.playerHealth / 3f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CooldownNumber : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int id;
    [SerializeField] private Image disable;
    [SerializeField] private float cooldownMax;

    private void Update()
    {
        float cooldown = GameScene._game.player.GetCooldown(id);
        text.text = string.Format("{0:f0}s", cooldown);
        disable.fillAmount = cooldown / cooldownMax;
    }
}

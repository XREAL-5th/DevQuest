using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MoveControl moveControl;
    [SerializeField] private Enemy boss;
    
    [SerializeField] private Image dashCooldownMask;
    [SerializeField] private Image bossHealth;
    
    void Update()
    {
        dashCooldownMask.fillAmount = moveControl.DashRemainingCooldown();
        bossHealth.fillAmount = boss.RemainingHealthRatio();
    }
}

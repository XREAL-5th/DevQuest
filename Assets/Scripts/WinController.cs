using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinController: LazySingletonMonoBehavior<WinController>
{
    private int enemyCount;
    private event UnityAction OnClearHandlers;
    
    protected override void Awake()
    {
        base.Awake();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
    }

    public void EnemyKilled()
    {
        enemyCount -= 1;
        if (enemyCount <= 0)
        {
            Debug.Log("Clear!");
            OnClearHandlers?.Invoke();
        }
    }
    
    public void OnClearStage(UnityAction action) {
        OnClearHandlers += action;
    }

    public void RemoveOnClearStageHandler(UnityAction action)
    {
        OnClearHandlers -= action;
    }
}
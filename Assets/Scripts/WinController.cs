using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinController: LazySingletonMonoBehavior<WinController>
{
    private static int enemyCount;
    private event UnityAction OnClearHandlers;

    public void EnemyKilled()
    {
        enemyCount -= 1;
        if (enemyCount <= 0)
        {
            OnClearHandlers?.Invoke();
        }
    }

    public void SpawnEnemy()
    {
        enemyCount += 1;
    }
    
    public void OnClearStage(UnityAction action) {
        OnClearHandlers += action;
    }

    public void RemoveOnClearStageHandler(UnityAction action)
    {
        OnClearHandlers -= action;
    }
}
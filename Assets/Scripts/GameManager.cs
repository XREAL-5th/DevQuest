using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private int remainingEnemies = 5; // 처치해야 할 적의 수


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        
    }


    public void EnemyKilled()
    {
        remainingEnemies--;

        if (remainingEnemies <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("게임 종료 - 모든 적 처치");

        SceneManager.LoadScene("GameOver");
    }
}

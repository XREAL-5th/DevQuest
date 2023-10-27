using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private int remainingEnemies = 5; // 처치해야 할 적의 수

    public GameObject menuSet;



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

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) // ESC 버튼
        {
            if (menuSet.activeSelf)
            {
                menuSet.SetActive(false);
            }
            else
            {
                menuSet.SetActive(true);
            }
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

    public void EndGame()
    {
        Debug.Log("게임 종료 - 모든 적 처치");

        SceneManager.LoadScene("GameOver");
    }
}

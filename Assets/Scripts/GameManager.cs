using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public int currEnemyNum;

    private bool isWin = false;

    public GameObject gameWInUI;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;

        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        currEnemyNum = 3;
    }
    private void Update()
    {
        if (isWin)
        {
            Debug.Log("Game Clear");
            gameWInUI.SetActive(true);
        }
    }

    public void GameClear()
    {
        if(currEnemyNum == 0)
        {
            isWin = true;
        }
    }

}

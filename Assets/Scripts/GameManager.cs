using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameClearUI;

    private static GameManager instance = null;    
    public static GameManager Instance
    {
        get 
        {
            if(instance == null)
            {
                return null;
            }    
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
             
            // Scene이 전환되어도 유지
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        } 
        
        gameClearUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool isGameOver = false;
    public void GameOver()
    {
        isGameOver = true;
        gameClearUI.SetActive(true);
    }
    public void GameComplete()
    {
        gameClearUI.SetActive(true);
    }
}

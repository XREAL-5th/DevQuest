using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] itemPrefabs;
 
    private static GameManager instance;

    private bool isGameStarted = false;
    private bool[] isEnemyAlive;
    int numberOfEnemies;
    int numberOfItems;

    public static GameManager Instance { 
        get 
        { if (instance.Equals(null))
            {
                instance = FindObjectOfType<GameManager>();

                if(instance.Equals(null))
                {
                    GameObject gameManager = new GameObject();
                    instance = gameManager.AddComponent<GameManager>();
                }
            }

        return instance;
        } 
    }

    public void Awake()
    {
        SetSingleton();
        if(!isGameStarted)
            StartGame();
    }

    private void SetSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void StartGame()
    {
        SetEnemy();
    }

    private void SetEnemy()
    {
        numberOfEnemies = enemyPrefabs.Length;
        isEnemyAlive = new bool[numberOfEnemies];

        for (int i = 0; i < numberOfEnemies; ++i)
        {
            GameObject enemy = Instantiate(enemyPrefabs[i], enemyPrefabs[i].transform.position, Quaternion.Euler(0, 180, 0));
            enemy.GetComponent<Enemy>().InitSetting(i);
            isEnemyAlive[i] = true;
        }

        isGameStarted = true;
    }
    
    private void SetItems()
    {
        numberOfItems = itemPrefabs.Length;
        for (int i = 0; i < numberOfItems; ++i)
        {
            GameObject Item = Instantiate(enemyPrefabs[i], enemyPrefabs[i].transform.position, Quaternion.identity);
        }
    }
    public void UpdateEnemyDeath(int id)
    {
        isEnemyAlive[id] = false;
        --numberOfEnemies;
        Debug.Log($"³²Àº Àû : {numberOfEnemies}");
        if (numberOfEnemies <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
    }
}

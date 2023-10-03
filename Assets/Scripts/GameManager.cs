using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject[] enemyPrefabs;

    private static GameManager instance;

    private bool isGameStarted = false;
    private GameObject[] enemies;
    private List<bool> isEnemyAlive;
    int numberOfEnemies;

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
        numberOfEnemies = enemyPrefabs.Length;
        enemies = new GameObject[numberOfEnemies];
        isEnemyAlive = new List<bool>();

        for(int i = 0; i < numberOfEnemies; ++i)
        {
            enemies[i] = Instantiate(enemyPrefabs[i], enemyPrefabs[i].transform.position, Quaternion.Euler(0, 180, 0));
            enemies[i].GetComponent<Enemy>().InitSetting(i);
            isEnemyAlive.Add(true);
        }

        isGameStarted = true;
    }

    public void UpdateEnemyDeath(int id)
    {
        isEnemyAlive[id] = false;
        enemies[id].gameObject.SetActive(false);
        --numberOfEnemies;
        Debug.Log($"{enemies[id].name} 해치움!");
        Debug.Log($"남은 적 : {numberOfEnemies}");
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

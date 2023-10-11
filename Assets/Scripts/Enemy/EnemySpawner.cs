using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 적 프리팹
    public Transform[] spawnPoints; // 적 소환 위치
    private List<GameObject> enemies = new List<GameObject>();
    float timer;
    float waitTime = 0.8f;
   
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            Transform spawnPoint = spawnPoints[i];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            enemies.Add(enemy);
            enemy.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetComponent<Enemy>().state == Enemy.State.Die)
            {
                timer += Time.deltaTime;
                if(timer > waitTime)
                {
                    enemies[i].SetActive(false);
                    enemies.RemoveAt(i);
                    Debug.Log(enemies.Count + " enemies left");
                    timer = 0;
                }
            }
        }
        if (enemies.Count <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}

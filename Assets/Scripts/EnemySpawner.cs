using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 적을 SpawnPoint 위치를 받아 생성하고, 생성된 적이 죽는다면, 다음 적을 그 위치에 생성한다.
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;


    private void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        // SpawnPoint에 위치하여 enemyPrefab을 생성
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        Enemy enemyState = enemy.GetComponent<Enemy>();
        if (enemyState != null)
        {
            // 생성된 적이 죽을 때를 감지하는 이벤트 핸들러를 등록
            enemyState.OnDeath += OnEnemyDeath;
        }

        else
        {
            Debug.Log("Enemy 정보 없음..");
        }
    }

    void OnEnemyDeath()
    {
        // 적이 죽으면 다음 적을 생성합니다.
        SpawnEnemy();
    }
}

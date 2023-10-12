using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���� SpawnPoint ��ġ�� �޾� �����ϰ�, ������ ���� �״´ٸ�, ���� ���� �� ��ġ�� �����Ѵ�.
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
        // SpawnPoint�� ��ġ�Ͽ� enemyPrefab�� ����
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        Enemy enemyState = enemy.GetComponent<Enemy>();
        if (enemyState != null)
        {
            // ������ ���� ���� ���� �����ϴ� �̺�Ʈ �ڵ鷯�� ���
            enemyState.OnDeath += OnEnemyDeath;
        }

        else
        {
            Debug.Log("Enemy ���� ����..");
        }
    }

    void OnEnemyDeath()
    {
        // ���� ������ ���� ���� �����մϴ�.
        SpawnEnemy();
    }
}

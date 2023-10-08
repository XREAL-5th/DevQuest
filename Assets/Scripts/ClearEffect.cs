using System.Collections;
using UnityEngine;

public class ClearEffect : MonoBehaviour
{
    public GameObject clearEffect;
    public float effectRate = 10f;
    public float effectDuration = 5f;
    public Vector3 spawnArea = new(10f, 10f, 10f);

    public void Awake()
    {
        WinController.Instance.OnClearStage(OnClearStage);
    }

    private void OnClearStage()
    {
        StartCoroutine(SpawnFireworks());
        Debug.Log("clear");
    }


    private IEnumerator SpawnFireworks()
    {
        float spawnTimer = 0f;

        while (spawnTimer < effectDuration)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnArea.x, spawnArea.x),
                Random.Range(0, spawnArea.y),
                Random.Range(-spawnArea.z, spawnArea.z)
            ) + transform.position; // Adjusts by the spawner's position

            Instantiate(clearEffect, randomPosition, Quaternion.identity);

            spawnTimer += 1f / effectRate;
            yield return new WaitForSeconds(1f / effectRate);
        }
    }
}
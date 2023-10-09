using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameEndingSingleton : MonoBehaviour
{
    [HideInInspector] public GameObject player;

    public static GameEndingSingleton main;

    public GameObject respawnEffect;
    public GameObject[] enemies = { };
    public Transform[] playerSpawn = { };
    private int index = 0;

    // non-lazy, non-DDOL
    private void Awake()
    {
        main = this;
        player = GameObject.Find("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (allDead())
        {
            player.transform.position = playerSpawn[index].position;
            player.transform.rotation = playerSpawn[index].rotation;
            var effect = Instantiate(respawnEffect, player.transform.position - new Vector3(0,1.0f,0), player.transform.rotation);
            Destroy(effect, 2);
            foreach (GameObject enemy in enemies) {
                enemy.SetActive(true);
                enemy.GetComponent<Enemy>().HP = 100;
            }
            index = Random.Range(0, playerSpawn.Length); // [min, max)
        }
    }

    public bool allDead()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<Enemy>().HP > 0)
            {
                return false;
            }
        }
        return true;
    }
}

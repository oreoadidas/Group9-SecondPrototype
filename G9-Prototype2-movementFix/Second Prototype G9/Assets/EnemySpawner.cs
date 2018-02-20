using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy; //enemy prefab
    float maxSpawnRate = 5f;

    // Use this for initialization
    void Start()
    {
        Invoke("SpawnEnemy", maxSpawnRate);

        //increase spawn rate every 30 seconds
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //end of screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //end of screen

        GameObject anEnemy = Instantiate(Enemy);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInNSeconds;
        if (maxSpawnRate > 1f)
        {
            spawnInNSeconds = Random.Range(1f, maxSpawnRate);
        }
        else
        {
            spawnInNSeconds = 1f;
            Invoke("SpawnEnemy", spawnInNSeconds);
        }
    }

    //difficulty increase:
    void IncreaseSpawnRate()
    {
        if (maxSpawnRate > 1f)
            maxSpawnRate--;
        if (maxSpawnRate == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }
}

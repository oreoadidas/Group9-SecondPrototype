using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour {

    [System.Serializable]
    public class Enemy //Enemy class stores each wave of a particular enemy
    {
        public enum SpawnState { Waiting, Spawning, Finished }

        public GameObject enemyPrefab; //The actual enemy GameObject
        public int amount; //The total amount of this enemy that should be spawned
        public float frequency; //How much delay between each spawn
        public float frequencyVariance; //How much random offset should be applied when delaying the next spawn
        //public float incrementScale; //How much faster the spawning gets as the wave progresses, set to 0 to have no change
        public int wave; //The wave that will start spawning this enemy
        public bool endless; //Set true to keep wave spawning forever once it has started, regardless of the amount set
        public SpawnState state = SpawnState.Waiting; //The state of this enemy wave
    }

    private int waveCount; //The total amount of waves in this level
    private int currentWave = 0; //The current wave the level is in
    private float waveDelay = 2; //The delay before the start of each wave

    public Enemy[] enemyList; //Add elements to this array to store multiple waves of different enemies

    private void Start()
    {
        StartCoroutine(StartWave(currentWave)); //Starts the first wave of the level
        int finalWave = 0;
        foreach (Enemy enemy in enemyList) //Calculates how many waves there are before the level is ended
        {
            if (enemy.wave > finalWave)
            {
                waveCount = enemy.wave + 1;
            }
        }
    }

    private IEnumerator StartWave(int wave)
    {
        yield return new WaitForSeconds(waveDelay);
        foreach (Enemy enemy in enemyList) //Cycles through each enemy wave
        {
            if (enemy.wave == wave && enemy.state == Enemy.SpawnState.Waiting) //If the enemy wave matches the current wave and is waiting to start
            {
                StartCoroutine(StartSpawning(enemy));
            }
        }
    }

    private IEnumerator StartSpawning(Enemy enemy) 
    {
        enemy.state = Enemy.SpawnState.Spawning;
        if (enemy.endless) //If endless is true, spawn this wave forever
        {
            int i = 0;
            while (true)
            {
                SpawnEnemy(enemy.enemyPrefab, i);
                i++;
                //Wait the length of the spawn delay before trying to spawn another enemy
                yield return new WaitForSeconds(Random.Range(enemy.frequency - enemy.frequencyVariance, enemy.frequency + enemy.frequencyVariance));
            }
        }
        else
        {
            for (int i = 0; i < enemy.amount; i++) //For as many enemies as set in the enemy amount
            {
                SpawnEnemy(enemy.enemyPrefab, i);
                if (i < enemy.amount - 1) //Dont wait for next spawn if this is the last iteration
                {
                    //Wait the length of the spawn delay before trying to spawn another enemy
                    yield return new WaitForSeconds(Random.Range(enemy.frequency - enemy.frequencyVariance, enemy.frequency + enemy.frequencyVariance));
                }
            }
        }   
        enemy.state = Enemy.SpawnState.Finished;
        if (!CheckWave(currentWave)) //Checks if the next wave should start, if so start it
        {
            currentWave++;
            StartCoroutine(StartWave(currentWave));
        }
        if (currentWave >= waveCount)
        {
            StartCoroutine(EndOfLevel());
        }
    }

    public GameObject SpawnEnemy(GameObject enemy, int enemyID)
    {
        //Spawn an enemy at a random point along the right edge of the screen
        GameObject newEnemy = Instantiate(enemy, Camera.main.ViewportToWorldPoint(new Vector2(1.1f, Random.Range(0.1f, 0.9f))), Quaternion.identity);
        Debug.Log("Wave: " + currentWave + " | " + enemy.name + " " + enemyID + " spawned!");
        return newEnemy;
    }

    private bool CheckWave(int wave) 
    {
        foreach (Enemy enemy in enemyList)
        {
            if (enemy.wave == wave)
            {
                if (enemy.state != Enemy.SpawnState.Finished) //If any enemy waves have not finished return true, otherwise return false
                {
                    return true;
                }
            }
        }
        return false;
    }

    private IEnumerator EndOfLevel() //Every so often check if there are any enemies left before starting the next level
    {
        bool startNextLevel = false;
        while (!startNextLevel)
        {
            if (GameObject.FindGameObjectWithTag("Enemy") == null) //Cycle through all GameObjects to find any enemies with the enemy tag
            {
                //Debug.Log(GameObject.FindGameObjectWithTag("Enemy"));
                startNextLevel = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            yield return new WaitForSeconds(2); //Should be a delay between checks as 'FindGameObjectsWithTag' should not be ran constantly
        }
    }
}

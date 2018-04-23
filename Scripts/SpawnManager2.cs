using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager2 : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject bossPrefab;
    [SerializeField]
    private GameObject enemy2Prefab;
    [SerializeField]
    private GameObject AsteroidPrefab;

    private int enemies = 1;

    /**
    * Start() method is called at the beginning of the scene.
    */
    void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerup());
    }

    /**
     * Routine for spawning enemies in the scene.
     */
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (enemies % 5 == 0) // if the enemy variable is dividable by 5 etc.
            {
                Instantiate(bossPrefab, new Vector3(Random.Range(-5f, 5f), 3.8f, 0), Quaternion.identity);
            }

            if (enemies % 3 == 0)
            {
                Instantiate(enemyPrefab, new Vector3(Random.Range(-5f, 5f), 3.8f, 0), Quaternion.identity);
            }

            if (enemies % 10 == 0)
            {
                Instantiate(AsteroidPrefab, new Vector3(Random.Range(-5f, 5f), 3.8f, 0), Quaternion.identity);
            }

            else
            {
                Instantiate(enemy2Prefab, new Vector3(Random.Range(-5f, 5f), 6, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(5);
            enemies++;
        }
    }

    /**
     * Routine for spawning powerups in the scene.
     */
    IEnumerator SpawnPowerup()
    {
        while (true)
        {
            int randomPower = Random.Range(0, 2);
            Instantiate(powerups[randomPower], new Vector3(Random.Range(5, -5), 6, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
}

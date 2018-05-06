using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace combat
{
    public class SpawnManager2 : MonoBehaviour
    {

        [SerializeField]
        private GameObject enemyPrefab;
        [SerializeField]
        public GameObject[] powerups;
        [SerializeField]
        private GameObject bossPrefab;
        [SerializeField]
        private GameObject enemyBomberPrefab;

        private int enemies = 1;

        /// Start() method is called at the beginning of the scene.
        void Start()
        {
            StartCoroutine(SpawnEnemy());
            StartCoroutine(SpawnPowerup());
        }

        /// Routine for spawning enemies in the scene.
        IEnumerator SpawnEnemy()
        {
            while (true)
            {
                if (enemies % 5 == 0) // if enemies variable is dividable by 5 etc.
                {
                    Instantiate(bossPrefab, new Vector3(Random.Range(-5f, 5f), 3.8f, 0), Quaternion.identity);
                }

                if (enemies % 3 == 0)
                {
                    Instantiate(enemyPrefab, new Vector3(Random.Range(-5f, 5f), 3.8f, 0), Quaternion.identity);
                }

                else
                {
                    Instantiate(enemyBomberPrefab, new Vector3(Random.Range(-5f, 5f), 6, 0), Quaternion.identity);
                }
                yield return new WaitForSeconds(3);
                enemies++;
            }
        }

        /// Routine for spawning powerups in the scene.
        IEnumerator SpawnPowerup()
        {
            while (true)
            {
                int randomPower = Random.Range(0, 4);
                Instantiate(powerups[randomPower], new Vector3(Random.Range(5, -5), 6, 0), Quaternion.identity);
                yield return new WaitForSeconds(3.0f);
            }
        }
    }
}
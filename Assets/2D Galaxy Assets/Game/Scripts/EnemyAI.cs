using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class EnemyAI : MonoBehaviour
    {
        private float speedHorizontal = 4.0f;
        private float speedVertical = 2.0f;
        private float fireRateLaser = 1f;
        private float canFireLaser = 0.0f;
        private float fireRateProjectile = 1.5f;
        private float canFireProjectile = 0.0f;
        private int mode = 1;
        public bool boss = false;
        public int bossHealth = 10;

        public GameObject enemyExplosionPrefab;
        public GameObject enemyLaserPrefab;
        public GameObject bossPrefab;
        public GameObject teleportPrefab;
        public GameObject enemyProjectilePrefab;

        /// Start() method is called when the Class is called.
        private void Start()
        {
            Instantiate(teleportPrefab, transform.position, Quaternion.identity);
        }

        /// Update() method is called 60 times per second.
        void Update()
        {
            Movement();
            if (Time.time > canFireLaser && tag != "EnemyBomber")
            {
                FireLaser();
            }
            SpawnBoss();
            if (tag == "Boss")
            {
                if (Time.time > canFireProjectile)
                {
                    FireProjectile();
                }
            }
        }

        /// Movement() method controls the movement of the enemy and sets boundaries for them.
        private void Movement()
        {
            if (mode == 1)
            {
                if (tag != "EnemyBomber")
                {
                    transform.Translate(Vector3.left * speedHorizontal * Time.deltaTime);
                }

                if (tag == "EnemyBomber")
                {
                    transform.Translate(Vector3.down * speedHorizontal * 2 * Time.deltaTime);
                    transform.Translate(Vector3.left * 0 * Time.deltaTime);

                    if (transform.position.y <= -6)
                    {
                        float randomX = Random.Range(-5.0f, 5.0f);
                        transform.position = new Vector3(randomX, 6, 0);
                    }
                }

                if (tag == "Boss")
                {
                    transform.Translate(Vector3.down * speedVertical * Time.deltaTime);
                }
            }

            if (mode != 1)
            {
                transform.Translate(Vector3.down * 0 * Time.deltaTime);
                transform.Translate(Vector3.left * 0 * Time.deltaTime);
            }

            if (transform.position.x <= -5)
            {
                speedHorizontal = speedHorizontal * (-1);
            }

            if (transform.position.x >= 5.5)
            {
                speedHorizontal = speedHorizontal * (-1);
            }

            if (transform.position.y >= 4)
            {
                speedVertical = speedVertical * (-1);
            }

            if (transform.position.y <= 2)
            {
                speedVertical = speedVertical * (-1);
            }

            if (transform.position.x < -5.4f || transform.position.x > 6) // if stuck, teleports to the middle of the screen.
            {
                Instantiate(teleportPrefab, transform.position, Quaternion.identity);
                transform.position = new Vector3(0, 3, 0);
            }
        }

        /// Collider for enemy prefab 
        /// Upon contact with player, initiates Player class' LoseHealth() method
        /// Destroys itself upon contact to laser or player.
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Laser" || other.tag == "Player")
            {
                Laser laser1 = other.GetComponent<Laser>();

                if (laser1 != null)
                {
                    if (bossHealth > 0)
                    {
                        bossHealth--;
                    }

                    else
                    {
                        Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
                        StartCoroutine(DeleteRoutine());
                    }
                }

                Player player1 = other.GetComponent<Player>();

                if (player1 != null)
                {
                    if (bossHealth > 0)
                    {
                        bossHealth--;
                        player1.LoseHealth();
                    }

                    else
                    {
                        player1.LoseHealth();

                        Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
                        StartCoroutine(DeleteRoutine());
                    }
                }
            }
        }

        /// Fire() Instantiates enemyLaserPrefab and sets its fire rate.
        private void FireLaser()
        {
            Instantiate(enemyLaserPrefab, transform.position + new Vector3(0, -0.83f, 0), Quaternion.identity);
            canFireLaser = Time.time + fireRateLaser;
        }

        /// Fire()2 Instantiates two enemyProjectilePrefabs and sets their fire rates.
        private void FireProjectile()
        {
            Instantiate(enemyProjectilePrefab, transform.position + new Vector3(1.375f, -0.47f, 0), Quaternion.identity);
            Instantiate(enemyProjectilePrefab, transform.position + new Vector3(-1.375f, -0.47f, 0), Quaternion.identity);
            canFireProjectile = Time.time + fireRateProjectile;
        }

        /// SpawnBoss() spawns a boss enemy instead of the normal enemy.
        private void SpawnBoss()
        {
            if (boss == true)
            {
                Instantiate(bossPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                bossHealth = 10 + bossHealth;
            }
            boss = false;
        }

        /// Routine for deleting unwanted clones from the scene.
        public IEnumerator DeleteRoutine()
        {
            mode++;
            yield return new WaitForSeconds(0.3f);
            Destroy(GameObject.Find("Enemy_explosion(Clone)"));
            Destroy(GameObject.Find("Teleport(Clone)"));
            Destroy(gameObject);
            mode = 1;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float speed = 4.0f;
    private float fireRate = 3.0f;
    private float canFire = 0.0f;
    private int mode = 1;
    public bool boss = false;
    public bool shield = false;

    public GameObject enemyExplosionPrefab;
    public GameObject enemyLaserPrefab;
    public GameObject bossPrefab;
    public GameObject teleportPrefab;



    /**
    * Start() method is called at the beginning of the scene.
    */
    private void Start()
    {
        Instantiate(teleportPrefab, transform.position, Quaternion.identity);
    }

    /**
    * Update() method is called 60 times per second.
    */
    void Update()
    {
        Movement();
        if (Time.time > canFire)
        {
            Fire();
        }

        SpawnBoss();
    }

    /**
    * Movement() method controls the movement of the enemy and sets boundaries for them.
    */
    private void Movement()
    {

        if (mode != 1)
        {
            transform.Translate(Vector3.down * 0 * Time.deltaTime);
        }

        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (transform.position.x < -5)
        {
            speed = speed * (-1);
        }

        if (transform.position.x > 5.5)
        {
            speed = speed * (-1);
        }

        if (transform.position.x < -6.5f || transform.position.x > 6.5f)
        {
            transform.position = new Vector3(0, 3, 0);
        }
    }

    /**
     * Collider for enemy prefab 
     * Upon contact with player, initiates Player class' LoseHealth() method
     * Destroys itself upon contact to laser or player.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser" || other.tag == "Player")
        {
            Laser laser1 = other.GetComponent<Laser>();

            if (laser1 != null)
            {
                Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
                StartCoroutine(DeleteRoutine());
            }

            Player player1 = other.GetComponent<Player>();

            if (player1 != null)
            {
                player1.LoseHealth();

                Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
                StartCoroutine(DeleteRoutine());
            }
        }
    }

    /**
     * Fire() Instantiates enemyLaserPrefab and sets its fire rate.
     */
    private void Fire()
    {
        Instantiate(enemyLaserPrefab, transform.position + new Vector3(0, -0.83f, 0), Quaternion.identity);
        canFire = Time.time + fireRate;
    }

    /**
     * SpawnBoss() spawns a boss enemy instead of the normal enemy.
     */
    private void SpawnBoss()
    {
        if (boss == true)
        {
            Instantiate(bossPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        boss = false;
    }

    /**
     * Routine for deleting unwanted clones from the scene.
     */
    public IEnumerator DeleteRoutine()
    {
        mode++;
        yield return new WaitForSeconds(0.5f);
        Destroy(GameObject.Find("Enemy_explosion(Clone)"));
        Destroy(GameObject.Find("Teleport(Clone)"));
        Destroy(gameObject);
        mode = 1;
    }
}

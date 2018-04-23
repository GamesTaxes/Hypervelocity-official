using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public int bossHealth = 10;
    public float speed = 3.0f;
    public float speed2 = 3.0f;
    public float fireRate = 1.5f;
    public float fireRate2 = 3.0f;
    private float canFire = 0.0f;
    private float canFire2 = 0.0f;
    private int mode = 1;
    public GameObject explosionPrefab;
    public GameObject enemyLaserPrefab;
    public GameObject enemyProjectilePrefab;
    public GameObject teleportPrefab;

    /**
    * Start() method is called at the beginning of the scene.
    */
    private void Start()
    {
        bossHealth = bossHealth + 10;
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

        if (Time.time > canFire2)
        {
            Fire2();
        }
    }

    /**
     * Method for movement for the boss enemy.
     */
    private void Movement()
    {

        transform.Translate(Vector3.left * speed * Time.deltaTime);
        transform.Translate(Vector3.down * speed2 * Time.deltaTime);

        if (mode != 1)
        {
            transform.Translate(Vector3.down * 0 * Time.deltaTime);
        }

        if (transform.position.x <= -5)
        {
            speed = speed * (-1);
        }

        if (transform.position.x >= 5.5)
        {
            speed = speed * (-1);
        }

        if (transform.position.y >= 4)
        {
            speed2 = speed2 * (-1);
        }

        if (transform.position.y <= 2)
        {
            speed2 = speed2 * (-1);
        }

        if (transform.position.x < -6 || transform.position.x > 6) // if stuck, teleports to the middle of the screen.
        {
            Instantiate(teleportPrefab, transform.position, Quaternion.identity);
            transform.position = new Vector3(0, 3, 0);
        }
    }

    /**
    * Collider for enemy prefab 
    * Upon contact with player, initiates Player class' LoseHealth() method
    * Loses health upon contacting the Laser or Player.
    */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser" || other.tag == "Player")
        {
            Laser laser1 = other.GetComponent<Laser>();
            Player player1 = other.GetComponent<Player>();

            if (laser1 != null)
            {
                bossHealth--;
            }

            if (player1 != null)
            {
                bossHealth--;
                player1.LoseHealth();
            }

            if (bossHealth < 1)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                StartCoroutine(DeleteRoutine());
            }
        }
    }

    /**
     * Fire() Instantiates enemyLaserPrefab and sets its fire rate.
     */
    private void Fire()
    {
        Instantiate(enemyLaserPrefab, transform.position + new Vector3(0, -1.2f, 0), Quaternion.identity);
        canFire = Time.time + fireRate;
    }

    /**
    * Fire()2 Instantiates two enemyProjectilePrefabs and sets their fire rates.
    */
    private void Fire2()
    {
        Instantiate(enemyProjectilePrefab, transform.position + new Vector3(1.375f, -0.47f, 0), Quaternion.identity);
        Instantiate(enemyProjectilePrefab, transform.position + new Vector3(-1.375f, -0.47f, 0), Quaternion.identity);
        canFire2 = Time.time + fireRate2;
    }

    /**
    * Routine for deleting unwanted clones from the scene.
    */
    public IEnumerator DeleteRoutine()
    {
        mode++;
        yield return new WaitForSeconds(0.5f);
        Destroy(GameObject.Find("Explosion 1(Clone)"));
        Destroy(gameObject);
        mode = 1;
    }
}

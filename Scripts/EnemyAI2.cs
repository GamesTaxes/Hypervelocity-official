using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI2 : MonoBehaviour
{
    private float speed = 4.0f;
    private int mode = 1;
    public GameObject enemyExplosionPrefab;

    /**
    * Update() method is called 60 times per second.
    * In EnemyAI2 class, it also controls asteroid horizontal movement to an extent.
    */
    void Update()
    {
        Movement();

        if (tag == "Asteroid" && mode == 1)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime * 0.5f);
        }
    }

    /**
     * Controls the movement of the enemy2 prefab.
     * If the enemy reaches the end of screen, it spawns it at random X position on the top of the screen.
     */
    public void Movement()
    {
        if (mode != 1) // for destroying unwanted clones
        {
            transform.Translate(Vector3.down * 0);
            transform.Translate(Vector3.left * 0);
        }

        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        if (transform.position.y <= -6)
        {
            float randomX = Random.Range(-5.0f, 5.0f);
            transform.position = new Vector3(randomX, 6, 0);
        }
    }

    /**
    * Collider for enemy2 prefab 
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
     * Routine for deleting unwante clones from the scene.
     */
    public IEnumerator DeleteRoutine()
    {
        mode++;
        yield return new WaitForSeconds(1f);
        Destroy(GameObject.Find("Enemy_explosion(Clone)"));
        Destroy(GameObject.Find("Explosion 1(Clone)"));
        Destroy(gameObject);
        mode = 1;
    }
}
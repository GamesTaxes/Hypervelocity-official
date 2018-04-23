using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private float speed = 4.0f;

    public GameObject explosionPrefab;

    /**
    * Update() method is called 60 times per second.
    */
    void Update()
    {
        Move();
        Destroy();
    }

    /**
     * Method for enemyLaser movement.
     */
    public void Move()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        Destroy();
    }

    /**
     * Method for destroying enemyLasers that travel out of screen.
     */
    public void Destroy()
    {
        if (transform.position.y <= -6)
        {
            Destroy(gameObject);
        }
    }

    /**
     * Collider for enemyLaser.
     * Upon contact with player, calls its' LoseHealth() method.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player1 = other.GetComponent<Player>();

            if (player1 != null)
            {
                player1.LoseHealth();
                Destroy(this.gameObject);
            }
        }
    }
}
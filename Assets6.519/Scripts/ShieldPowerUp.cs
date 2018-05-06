using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{

    public class ShieldPowerUp : MonoBehaviour
    {
        public bool shield = false;
        private float speed = 0.5f;

        /**
        * Update() method is called 60 times per second.
        */
        void Update()
        {
            PowerupMovement();
        }

        /**
         * Collider for shield powerup prefab 
         * Upon contact, activates shields on player and destroys itself
         */
        private void OnTriggerEnter2D(Collider2D other)
        {
            //Debug.Log("Collided with: " + other.name);
            shield = true;

            if (other.tag == "Player")
            {
                Player player1 = other.GetComponent<Player>();

                if (player1 != null)
                {
                    player1.ShieldPowerUpOn();
                }

                Destroy(gameObject);
            }
        }

        /**
        * Movement method for shield powerup prefab 
        * Despawns at the bottom of the screen and then spawns at random position on the top of the screen 
        */
        public void PowerupMovement()
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);

            if (transform.position.y <= -6)
            {
                float randomX = Random.Range(-5.0f, 5.0f);
                transform.position = new Vector3(randomX, 6, 0);
            }
        }
    }
}
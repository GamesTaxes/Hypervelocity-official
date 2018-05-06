using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{

    public class Powerup : MonoBehaviour
    {
        public int upgrade = 0;
        public float speed = 1f;

        /// Update() method is called 60 times per second.
        void Update()
        {
            PowerupMovement();
        }

        /// Method that controls the movement speed of the powerupPrefabs.
        /// Also despawns them if they go out of screen and spawns them back on the top of the screen.
        public void PowerupMovement()
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);

            if (transform.position.y <= -6)
            {
                float randomX = Random.Range(-5.0f, 5.0f);
                transform.position = new Vector3(randomX, 6, 0);
            }
        }


        /// Collider for powerups.
        /// Upon contact with player, destroys itself.
        private void OnTriggerEnter2D(Collider2D other)
        {
            //if (tag == "Drone")
            //{
            //    Player player1 = other.GetComponent<Player>();
            //
            //    if (player1 != null)
            //    {
            //        player1.DefenderOn();
            //    }
            //
            //    Destroy(this.gameObject);
            //}

            //else
            //{
                upgrade++;

                if (other.tag == "Player")
                {
                    Player player1 = other.GetComponent<Player>();

                    if (player1 != null)
                    {
                        player1.PowerUpOn();
                    }

                    Destroy(this.gameObject);
                }
            //}
        }
    }
}
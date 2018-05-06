using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{

    public class Defender_bot : MonoBehaviour
    {
        private float fireRate = 3.0f;
        private float canFire = 0.0f;

        public GameObject laserPrefab;

        /**
        * Update() method is called 60 times per second.
        */
        void Update()
        {
            if (Time.time > canFire)
            {
                Fire();
            }
        }

        /**
         * Fire() controls the firing of the defender.
         */
        private void Fire()
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            canFire = Time.time + fireRate;
        }

        /**
         * DefenderActive() uses DefenderOn() method from Player class. Its main function is to activate the defender bot.
         */
        public void DefenderActive()
        {

            Player player1 = GetComponent<Player>();

            player1.DefenderOn();
        }
    }
}

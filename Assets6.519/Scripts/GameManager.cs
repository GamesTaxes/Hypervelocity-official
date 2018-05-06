using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Combat
{
    public class GameManager : MonoBehaviour
    {
        public bool gameOver = false;
        public bool pause = false;
        public bool start = true;

        public GameObject playerPrefab;
        public GameObject pauseMenuPrefab;
        public GameObject endExplosionPrefab;
        public GameObject planetPrefab;
        private UiManager uimanager;

        Player player;
        Buttons buttons;

        ///Start() method is called when the class is first called.
        private void Start()
        {
            uimanager = GameObject.Find("Canvas").GetComponent<UiManager>();
            buttons = FindObjectOfType<Buttons>();
        }


        /// Update() method is called 60 times per second.
        /// In GameManager class, it checks for game over and destroys ALL clones if true.
        /// Then it waits for space key to continue.
        void Update()
        {
            var clones = GameObject.FindObjectsOfType<GameObject>().Where<GameObject>(list => list.name.Contains("(Clone)")).ToArray();

            if (gameOver == true)
            {

                Time.timeScale = 0f;

                if (start == true)
                {
                    foreach (var clone in clones)
                    {
                        Destroy(clone);
                        start = false;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    startGame();
                }

                start = true;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                Pause();
            }
        }


        /// Starts the game, creates the player and resets UI elements to default.
        public void startGame()
        {
            player = Instantiate(playerPrefab, new Vector3(0, -2, 0), Quaternion.identity).GetComponent<Player>();

            Destroy(GameObject.Find("planet"));
            Instantiate(planetPrefab, new Vector3(0, 10, 0), Quaternion.identity);

            gameOver = false;
            Time.timeScale = 1f;
            uimanager.HideTitle();

            Destroy(GameObject.Find("explosion"));
            Destroy(GameObject.Find("health0(Clone)"));
        }

        ///Method for pausing the game.
        private void Pause()
        {
            if (pause == true)
            {
                Destroy(GameObject.Find("paused(Clone)"));
                pause = false;
                Time.timeScale = 1f;
            }

            else
            {
                Instantiate(pauseMenuPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                pause = true;
                Time.timeScale = 0f;
            }
        }
    }
}


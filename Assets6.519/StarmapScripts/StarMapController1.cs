using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;

namespace StarMap
{

    public class StarMapController1 : MonoBehaviour
    {

        private bool move = false;
        private bool Orbiting = true;
        private Button star1;
        private Button star2;
        private Button star3;
        private Button star4;
        private GameObject playerShip1;
        public GameObject playerShip2;
        private GameObject playerShip3;
        private GameObject playerShip4;
        private GameObject ShipLocation;
        private int starLocation = 1;
        private int pressedLocation = 1;
        private Text locationText;
        private Text errorText;
        private Ship player;
        public bool ship2 = false;
        private List<Star> rooms = new List<Star>();
        private string savedProgressGameString;
        private int savedProgressGameInt;
        Orbit orbit1;

        /// Start() is called at the beginning of the code

        void Start()
        {

            loadGameProgress();

            rooms.Add(new Star("Starting location"));
            rooms.Add(new Star("Spacedock of Planet Unopia"));
            rooms.Add(new Star("Anu"));
            rooms.Add(new Star("Home, Uranus"));
            rooms[0].SetNextRooms(null, rooms[1]);
            rooms[1].SetNextRooms(rooms[0], rooms[2]);
            rooms[2].SetNextRooms(rooms[1], rooms[3]);
            rooms[3].SetNextRooms(rooms[2], null);
            player = new Ship(rooms[0]);
            locationText = GameObject.Find("TextLocation").GetComponent<Text>();
            errorText = GameObject.Find("TextError").GetComponent<Text>();
            star1 = GameObject.Find("Star1").GetComponent<Button>();
            star2 = GameObject.Find("Star2").GetComponent<Button>();
            star3 = GameObject.Find("Star3").GetComponent<Button>();
            star4 = GameObject.Find("Star4").GetComponent<Button>();
            star1.onClick.AddListener(() => buttonPressed(star1));
            star2.onClick.AddListener(() => buttonPressed(star2));
            star3.onClick.AddListener(() => buttonPressed(star3));
            star4.onClick.AddListener(() => buttonPressed(star4));
            locationText.text = player.GetLocation().GetName();
            playerShip1 = GameObject.Find("PlayerShip");
            playerShip1.SetActive(false);
            playerShip2 = GameObject.Find("PlayerShip2");
            playerShip2.SetActive(false);
            playerShip3 = GameObject.Find("PlayerShip3");
            playerShip3.SetActive(false);
            playerShip4 = GameObject.Find("PlayerShip4");
            playerShip4.SetActive(false);

            updateStarLocation(savedProgressGameInt);
        }

        /**buttonPressed is called when a button is pressed, with b becoming the button that was pressed.
           Next there are a bunch of if else sentences for different buttons.
        **/

        void buttonPressed(Button b)
        {

            Debug.Log("Button pressed:" + b);
            errorText.text = "";
            if (b == star1)
            {
                pressedLocation = 1;
                movementError();
            }
            else if (b == star2)
            {
                pressedLocation = 2;
                if (starLocation == 1)
                {
                    playerShip1.SetActive(false);
                    playerShip2.SetActive(true);
                    player.Move("right");
                    starLocation = 2;
                    ShipLocation = playerShip2;
                    shipActivate();
                    orbit1.setStar(star2);
                    SceneManager.LoadScene("CombatHyperVelocity");
                }
                else
                {
                    movementError();
                }
            }
            else if (b == star3)
            {
                pressedLocation = 3;
                if (starLocation == 2)
                {
                    playerShip2.SetActive(false);
                    playerShip3.SetActive(true);
                    player.Move("right");
                    starLocation = 3;
                    ShipLocation = playerShip3;
                    orbit1.setStar(star3);
                    SceneManager.LoadScene("CombatHyperVelocity");
                }
                else
                {
                    movementError();
                }
            }
            else if (b == star4)
            {
                pressedLocation = 4;
                if (starLocation == 3 || starLocation == 5)
                {
                    playerShip3.SetActive(false);
                    playerShip4.SetActive(true);
                    player.Move("right");
                    starLocation = 4;
                    ShipLocation = playerShip4;
                    orbit1.setStar(star4);
                    SceneManager.LoadScene("CombatHyperVelocity");
                }
                else
                {
                    movementError();
                }
            }


            locationText.text = player.GetLocation().GetName();
            Debug.Log(player.GetLocation().GetName() + ", Location: " + starLocation);
        }

        public void shipActivate()
        {
            ship2 = true;
            ShipLocation.SetActive(true);
        }

        /**
         movementError is called when the player tries to move to a star he cannot move to at the moment.
         **/

        public void movementError()
        {
            Debug.Log("Movement error.");
            if (starLocation < pressedLocation)
            {
                errorText.text = "We can't reach that star from over here!";
                Debug.Log("We can't reach that star from here!");

            }
            else if (starLocation > pressedLocation)
            {
                errorText.text = "We can't go backwards!";
                Debug.Log("We can't go backwards!");

            }
            else
            {
                errorText.text = "We are already here!";
                Debug.Log("We are already here!");
            }
            StartCoroutine(coolDown());
        }

        /// <summary>
        /// Reads saved progress from text file.
        /// @Torsti
        /// </summary>
        private void loadGameProgress()
        {
            savedProgressGameString = File.ReadAllText("Assets\\Resources\\Progress.txt");
            if (Int32.TryParse(savedProgressGameString, out savedProgressGameInt) != false)
            {
                savedProgressGameInt = Convert.ToInt32(savedProgressGameString);
            }
            else
            {
                savedProgressGameInt = 0;
            }

        }

        /// <summary>
        /// Updates starLocation according to loaded game progress
        /// @Torsti
        /// </summary>
        /// <param name="savedProgressLocation"></param>
        private void updateStarLocation(int savedProgressLocation)
        {
            starLocation = savedProgressLocation - 2;
            if (starLocation == 1)
            {
                ShipLocation = playerShip1;
                orbit1 = new Orbit(star1);
                playerShip1.SetActive(true);
            }
            else if (starLocation == 2)
            {
                ShipLocation = playerShip2;
                orbit1 = new Orbit(star2);
                playerShip2.SetActive(true);
            }
            else if (starLocation == 3 || starLocation == 5)
            {
                ShipLocation = playerShip3;
                orbit1 = new Orbit(star3);
                playerShip3.SetActive(true);
            }
            else if (starLocation == 4 || starLocation == 6)
            {
                ShipLocation = playerShip4;
                orbit1 = new Orbit(star4);
                playerShip4.SetActive(true);
            }

        }

        /**
         IEnumerator is called in movementError. It makes it so the error message from trying to move to an invalid location doesnt stay onscreen
            forever.
         **/

        public IEnumerator coolDown()
        {
            yield return new WaitForSeconds(2.5f);
            errorText.text = "";
        }
    }
}

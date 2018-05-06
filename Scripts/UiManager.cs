using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{


    public class UiManager : MonoBehaviour
    {

        public bool start = false;

        public GameObject titleScreen;
        public GameObject health33Prefab;
        public GameObject rightButtonPrefab;
        public GameObject leftButtonPrefab;
        public GameObject laserButtonPrefab;


        /// Update() method is called 60 times per second.

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                start = true;
            }
        }


        /// Method to show the title at the start of the scene.

        public void ShowTitle()
        {
            titleScreen.SetActive(true);
        }


        /// Method that hids the title when it's called.
        public void HideTitle()
        {
            titleScreen.SetActive(false);
            Instantiate(health33Prefab, new Vector3(-8f, -3.5f, 0), Quaternion.identity);
        }

        /// Method for activating buttons for mobile users.
        public void MobileButtonActivation()
        {
            rightButtonPrefab.SetActive(true);
            leftButtonPrefab.SetActive(true);
            laserButtonPrefab.SetActive(true);
        }
    }
}
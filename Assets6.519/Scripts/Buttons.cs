using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Combat
{


    public class Buttons : MonoBehaviour
    {
        private ButtonSideways buttonLeft;
        private ButtonSideways buttonRight;
        private Button buttonLaser;
        private Button buttonStart;

        private float speed = 5.0f;

        public GameObject laserPrefab;

        public void Start()
        {
            buttonLeft = GameObject.Find("ButtonLeftTrigger").GetComponent<ButtonSideways>();
            buttonRight = GameObject.Find("ButtonRightTrigger").GetComponent<ButtonSideways>();
            buttonLaser = GameObject.Find("ButtonLaserTrigger").GetComponent<Button>();
            buttonStart = GameObject.Find("ButtonStartTrigger").GetComponent<Button>();

            buttonLaser.onClick.AddListener(() => buttonPressed(buttonLaser));
            buttonStart.onClick.AddListener(() => buttonPressed(buttonStart));
        }
        /// <summary>
        /// The if-sentences allow the player to move by using the on-screen-buttons.
        /// @Torsti
        /// </summary>
        private void Update()
        {
            if (buttonLeft.GetPressed == true)
            {
                GameManager.FindObjectOfType<Player>().transform.Translate(Vector3.right * Time.deltaTime * speed * -1);
            }

            if (buttonRight.GetPressed == true)
            {
                GameManager.FindObjectOfType<Player>().transform.Translate(Vector3.right * Time.deltaTime * speed * 1);
            }

        }
        /// <summary>
        /// This implements the buttonLaser. It looks for the "player" object in GameManager and calls for its method, fire.
        /// @Torsti
        /// </summary>
        /// <param name="b"></param>
        public void buttonPressed(Button b)
        {
            if (b == buttonLaser)
            {
                GameManager.FindObjectOfType<Player>().Fire();
            }
            else if(b == buttonStart)
            {
                Player.FindObjectOfType<GameManager>().startGame();
                buttonStart.gameObject.SetActive(false);
            }
        }

    }
}


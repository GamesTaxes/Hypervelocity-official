using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

namespace Mainmenu
{


    public class MainMenuController : MonoBehaviour
    {
        private Button startGame;
        private Button quitGame;
        private string savedProgressGameString = "0";

        ///Start gets the button gameobjects from unity to use in the code

        void Start()
        {
            startGame = GameObject.Find("StartButton").GetComponent<Button>();
            quitGame = GameObject.Find("QuitButton").GetComponent<Button>();
            startGame.onClick.AddListener(() => buttonPressed(startGame));
            quitGame.onClick.AddListener(() => buttonPressed(quitGame));
        }

        ///buttonPressed is called when a button is pressed, with b becoming the button that was pressed.
        ///startGame moves on to the first scene of the game, quitGame exits the application.

        void buttonPressed(Button b)
        {
            if (b == startGame)
            {
                File.WriteAllText("Assets\\Resources\\Progress.txt", savedProgressGameString);
                SceneManager.LoadScene("DialogueHyperVelocity");
            }
            else if (b == quitGame)
            {
                Application.Quit();
            }
        }
    }
}
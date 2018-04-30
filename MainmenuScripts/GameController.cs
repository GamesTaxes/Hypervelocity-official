using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Button startGame;
    private Button quitGame;

    ///Start gets the button gameobjects from unity to use in the code

    void Start()
    {
        startGame = GameObject.Find("StartButton").GetComponent<Button>();
        quitGame = GameObject.Find("QuitButton").GetComponent<Button>();
        startGame.onClick.AddListener(() => buttonPressed(startGame));
        quitGame.onClick.AddListener(() => buttonPressed(quitGame));
    }

    ///buttonPressed is called when a button is pressed, with b becoming the button that was pressed.
    ///Next there are if else sentences for different buttons.

    void buttonPressed(Button b)
    {
        if (b == startGame)
        {
            SceneManager.LoadScene("DemoSceneDialogue");
        }
        else if (b == quitGame)
        {
            Application.Quit();
        }
    }
}
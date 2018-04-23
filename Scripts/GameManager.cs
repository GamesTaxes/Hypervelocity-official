using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;
    public bool pause = false;
    public int start = 0;

    public GameObject playerPrefab;
    public GameObject pauseMenuPrefab;
    public GameObject endExplosionPrefab;
    private UiManager uimanager;

    /**
    * Start() method is called at the beginning of the scene.
    */
    private void Start()
    {
        uimanager = GameObject.Find("Canvas").GetComponent<UiManager>();
    }

    /**
    * Update() method is called 60 times per second.
    * In GameManager class, it checks for game over and destroys ALL clones if true.
    * Then it waits for space key to continue.
    */
    void Update()
    {

        var clones = GameObject.FindObjectsOfType<GameObject>().Where<GameObject>(list => list.name.Contains("(Clone)")).ToArray();

        if (gameOver == true)
        {
            Time.timeScale = 0f;

            if (start > 0)
            {
                foreach (var clone in clones)
                {
                    Destroy(clone);
                    start--;
                }
            }

            start++;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(playerPrefab, new Vector3(0, -2, 0), Quaternion.identity);

                gameOver = false;
                Time.timeScale = 1f;
                uimanager.HideTitle();

                Destroy(GameObject.Find("explosion"));
                Destroy(GameObject.Find("health0(Clone)"));
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    /**
     * MEthod for pausing the game.
     */
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

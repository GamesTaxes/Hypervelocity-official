using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public float speed = 0.11f;
    private int text = 0;

    public GameObject planetPrefab;
    public GameObject textPrefab;
    private GameManager gameManager;

    /**
    * Start() method is called at the beginning of the scene.
    */
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    /**
    * Update() method is called 60 times per second.
    */
    void Update()
    {
        Movement();
    }

    /**
    * Movement() controls the movement of the planet, which is the finish line of the scene.
    * Also it displays the text prefab when the planet reaches a certain point.
    */
    public void Movement()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y <= 5)
        {
            if (text < 1)
            {
                Instantiate(textPrefab, new Vector3(0, -3.5f, 0), Quaternion.identity);
                text++;
            }
        }

        if (transform.position.y <= 4.5f)
        {
            gameManager.gameOver = true;
            transform.position = new Vector3(0, 10, 0);
            Destroy(GameObject.Find("SceneEndText(Clone)"));
            text = 0;
        }
    }
}

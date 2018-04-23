using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Player : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject tripleShotPrefab;
    public GameObject doubleShotPrefab;
    public GameObject explosionPrefab;
    public GameObject shieldPrefab;
    public GameObject health2Prefab;
    public GameObject health1Prefab;
    public GameObject health0Prefab;
    public GameObject defenderBotPrefab;
    public GameObject teleportPrefab;

    private bool tripleshot;
    private bool doubleshot;
    private bool shield = false;
    public bool defender = false;

    private int deaths;
    private int Health = 3;
    private int upgrade = 0;
    private float fireRate = 0.25f;
    private float canFire = 0.0f;
    private float speed = 5.0f;
    public int clones = 0;

    private UiManager uiManager;
    private GameManager gameManager;

    /**
    * Start() method is called at the beginning of the scene.
    */
    private void Start()
    {
        transform.position = new Vector3(0, -3, 0);
        Instantiate(teleportPrefab, transform.position, Quaternion.identity);

        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    /**
    * Update() method is called 60 times per second.
    * In Player class, it checks for button presses and if the defender should be active or not.
    */
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        if (defender == true)
        {
            DefenderOn();
        }
    }

    /**
    * Movement() method controls the movement of the player and sets the boundaries for the player.
    */
    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * Time.deltaTime * speed * verticalInput);

        if (transform.position.y > 4)
        {
            transform.position = new Vector3(transform.position.x, 4, 0);
        }

        if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }

        if (transform.position.x > 5.6f)
        {
            transform.position = new Vector3(5.6f, transform.position.y, 0);
        }

        if (transform.position.x < -6.5f)
        {
            transform.position = new Vector3(-6.5f, transform.position.y, 0);
        }
    }

        /**
    * Fire() method controls which weapons are in use and fires correspondingly upon mouse click or spacebar.
    * Also destroys unwanted clones from the scene.
    */
    private void Fire()
    {
        if (Time.time > canFire)
        {
            if (tripleshot == true)
            {
                Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
                canFire = Time.time + fireRate;
            }

            else if (doubleshot == true)
            {
                Instantiate(doubleShotPrefab, transform.position, Quaternion.identity);
                canFire = Time.time + fireRate;
            }

            else
            {
                Instantiate(laserPrefab, transform.position + new Vector3(0, 0.82f, 0), Quaternion.identity);
                canFire = Time.time + fireRate;

                Destroy(GameObject.Find("Triple Shot(Clone)"));
                Destroy(GameObject.Find("Double Shot(Clone)"));
            }
        }
    }

    /**
    * PowerUpOn() controls what weapon powerups the player has available.
    */
    public void PowerUpOn()
    {
        upgrade++;

        if (upgrade == 1)
        {
            doubleshot = true;
        }

        if (upgrade >= 2)
        {
            doubleshot = false;
            tripleshot = true;
        }

        StartCoroutine(PowerDownRoutine());
    }

    /**
    * ShieldPowerUpOn() activates the shield prefab that is set to the player.
    */
    public void ShieldPowerUpOn()
    {
        shield = true;
        shieldPrefab.SetActive(true);
    }

    /**
    * DefenderOn() activates the defender prefab that is set to the player.
    */
    public void DefenderOn()
    {
        defender = true;
        defenderBotPrefab.SetActive(true);
    }

    /**
     * IEnumerator PowerDownRoutine() sets a cooldown for all other methdos that use it. 
     */
    public IEnumerator PowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        doubleshot = false;
        tripleshot = false;
        upgrade = 0;
    }

    /**
     * LoseHealth() controls health the player has. If it reaches 0, it destroys the player prefab, plays the explosion prefab and displays the main menu screen.
     * It also controls the shield variable.
     */
    public void LoseHealth()
    {
        if (shield == true)
        {
            shield = false;
            shieldPrefab.SetActive(false);
            return;
        }

        Health--;

        if (Health == 2)
        {
            Destroy(GameObject.Find("health3(Clone)"));
            Instantiate(health2Prefab, new Vector3(-8f, -3.5f, 0), Quaternion.identity);
        }

        if (Health == 1)
        {
            Destroy(GameObject.Find("health2(Clone)"));
            Instantiate(health1Prefab, new Vector3(-8f, -3.5f, 0), Quaternion.identity);
        }

        if (Health < 1)
        {
            gameManager.gameOver = true;
            uiManager.ShowTitle();


            Destroy(GameObject.Find("health1(Clone)"));
            Instantiate(health0Prefab, new Vector3(-8f, -3.5f, 0), Quaternion.identity);

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}

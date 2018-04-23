using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject smallExplosionPrefab;

    public float speed = 10.0f;
    public float verticalInput = 0;

    /**
    * Update() method is called 60 times per second.
    */
    void Update()
    {
        Move();
        Destroy();
    }

    /**
     * Method for moving the laser.
     */
    public void Move()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        Destroy();
    }

    /**
     * Destroys lasers that travel out of screen.
     */
    public void Destroy()
    {
        if (transform.position.y >= 5)
        {
            Destroy(gameObject);
        }
    }

    /**
    * Collider for laser prefab.
    * Upon contact with enemy, destroys itself with an explosion.
    */
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Enemy")
        {
            EnemyAI enemy1 = other.GetComponent<EnemyAI>();
            EnemyAI2 enemy2 = other.GetComponent<EnemyAI2>();
            BossAI boss1 = other.GetComponent<BossAI>();

            if (enemy1 != null || enemy2 != null || boss1 != null)
            {
                StartCoroutine(ExplosionRoutine());
            }
        }
    }

    /**
    * Routine for deleting unwanted clones from the scene.
    */
    public IEnumerator ExplosionRoutine()
    {
        Instantiate(smallExplosionPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Destroy(GameObject.Find("Explosion_small(Clone)"));
        Destroy(gameObject);
    }
}

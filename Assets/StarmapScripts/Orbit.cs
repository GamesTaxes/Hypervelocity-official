using System.Collections;
using UnityEngine;
using UnityEngine.UI;

///class Orbit makes the player ship orbit around the star it is currently at. This is to indicate tha player's location.
///I used this tutorial as a base: https://www.youtube.com/watch?v=3PHc6vEckvc

public class Orbit
{
    private float speed = -50f;
    private float speed2 = 50f;
    private Button star;
    private Vector2 zAxis;
    Vector3 targetPosition;
    Vector3 currentPosition;
    Vector3 directionOfTravel;

    public Orbit(Button star) {
        
        this.star = star;
    }

    public void Start()
    {
        
    }

    public void Update()
    {
        

        ///angle += speed * Time.deltaTime;

        ///var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        ///playerShip.transform.position = zAxis + offset;
    }

    public void setStar(Button star)
    {
        this.star = star;
        Debug.Log("Rotating around: " + star);
    }

    public void orbitAround(GameObject ShipLocation)
    {
        ShipLocation.transform.RotateAround(star.transform.position, Vector3.forward, speed * Time.deltaTime);
    }

    /// /*https://stackoverflow.com/questions/33705366/unity-5-how-to-make-an-object-move-to-another-object */
    /// This was an attempt at making the ship move from one star to next. I couldn't get this to work and in the final game it was
    /// unnecessary anyway.

    ///public void nextDestination(bool move)
    ///{
    ///    targetPosition = star.transform.position;
    ///    currentPosition = playerShip.transform.position;
    ///    directionOfTravel = targetPosition - currentPosition;
    ///    if (Vector3.Distance(currentPosition, targetPosition) > 50)
    ///    {
    ///        playerShip.transform.Translate(
    ///            (directionOfTravel.x * speed2 * Time.deltaTime),
    ///            (directionOfTravel.y * speed2 * Time.deltaTime),
    ///            (directionOfTravel.z * speed2 * Time.deltaTime));
    ///    }
    ///    else
    ///    {
    ///        move = false;
    ///    }
    ///}
}

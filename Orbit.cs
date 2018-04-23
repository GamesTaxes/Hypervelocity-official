/** using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/**class Orbit is supposed to make the player ship orbit around the star it is currently at. This is to indicate tha player's location.
 * I have not managed to get this to work yet, however.
  **/

/**public class Orbit : MonoBehaviour
{
    private float speed = 5f;
    private float radius = 0.1f;

    private Vector2 zAxis;
    private float angle;

    public void Start()
    {
        zAxis = transform.position;
    }

    public void Update(Button b)
    {
        angle += speed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        b.transform.position = zAxis + offset;
    }

    /**  public void orbitAround(Button b)
      {
          transform.RotateAround(b.transform.position, zAxis, speed * Time.deltaTime);
      }**/
  
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private Button star1;
    private Button star2;
    private Button star3;
    private Button star4;
    private int starLocation = 1;
    private int pressedLocation = 1;
    private Text locationText;
    private Text errorText;
    private Ship player;
    private List<Star> rooms = new List<Star>();

    // Start() is called at the beginning of the code

    void Start()
    {
        
        rooms.Add(new Star("Star1"));
        rooms.Add(new Star("Star2"));
        rooms.Add(new Star("Star3"));
        rooms.Add(new Star("Star4"));
        rooms[0].SetNextRooms(null, rooms[1]);
        rooms[1].SetNextRooms(rooms[0], rooms[2]);
        rooms[2].SetNextRooms(rooms[1], rooms[3]);
        rooms[3].SetNextRooms(rooms[2], null);
        player = new Ship(rooms[0]);
        locationText = GameObject.Find("TextLocation").GetComponent<Text>();
        errorText = GameObject.Find("TextError").GetComponent<Text>();
        star1 = GameObject.Find("Star1").GetComponent<Button>();
        star2 = GameObject.Find("Star2").GetComponent<Button>();
        star3 = GameObject.Find("Star3").GetComponent<Button>();
        star4 = GameObject.Find("Star4").GetComponent<Button>();
        star1.onClick.AddListener(() => buttonPressed(star1));
        star2.onClick.AddListener(() => buttonPressed(star2));
        star3.onClick.AddListener(() => buttonPressed(star3));
        star4.onClick.AddListener(() => buttonPressed(star4));
        locationText.text = player.GetLocation().GetName();
    }

    /**buttonPressed is called when a button is pressed, with b becoming the button that was pressed.
       Next there are a bunch of if else sentences for different buttons.
    **/

    void buttonPressed(Button b)
    {
        //Orbit orbit1 = GetComponent<Orbit>();
        Debug.Log("Button pressed:" + b);
        errorText.text = "";
        if (b == star1)
        {
            pressedLocation = 1;
            movementError();
        }
        else if (b == star2)
        {
            pressedLocation = 2;
            if (starLocation == 1)
            {
                player.Move("right");
                starLocation = 2;
            }
            else
            {
                movementError();
            }
        }
        else if (b == star3)
        {
            pressedLocation = 3;
            if (starLocation == 2)
            {
                player.Move("right");
                starLocation = 3;
            }
            else
            {
                movementError();
            }
        }
        else if (b == star4)
        {
            pressedLocation = 4;
            if (starLocation == 3)
            {
                player.Move("right");
                starLocation = 4;
            }
            else
            {
                movementError();
            }
        }

        //orbit1.Update(b);
        locationText.text = player.GetLocation().GetName();
        Debug.Log(player.GetLocation().GetName() + ", Location: " + starLocation);
    }

    /**
     movementError is called when the player tries to move to a star he cannot move to at the moment.
     **/

    public void movementError()
    {
        Debug.Log("Movement error.");
        if (starLocation < pressedLocation)
        {
            errorText.text = "We can't reach that star from over here!";
            Debug.Log("We can't reach that star from here!");

        } else if (starLocation > pressedLocation)
        {
            errorText.text = "We can't go backwards!";
            Debug.Log("We can't go backwards!");

        } else
        {
            errorText.text = "We are already here!";
            Debug.Log("We are already here!");
        }
        StartCoroutine(coolDown());
    }

    /**
     IEnumerator is called in movementError. It makes it so the error message from trying to move to an invalid location doesnt stay onscreen
        forever.
     **/

    public IEnumerator coolDown()
    {
        yield return new WaitForSeconds(2.5f);
        errorText.text = "";
    }
}
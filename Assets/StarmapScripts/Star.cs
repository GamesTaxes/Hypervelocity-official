using System;

public class Star
{
    private string roomName;
    private Star nextRoomLeft;
    private Star nextRoomRight;

    /**
    When a new star is made, it gets its name here.
    **/

    public Star(string name)
    {
        this.roomName = name;
    }

    /**
    Different rooms created in GameController use this. First variable shows direction to its left and second to its right.
    **/

    public void SetNextRooms(Star left, Star right)
    {
        this.nextRoomLeft = left;
        this.nextRoomRight = right;
    }

    ///GetNext moves the ship. nextRoomLeft is not used anymore, since the player doesnt move backwards in the game.

    public Star GetNext(string direction)
    {
        /**if (direction == "left")
        {
            return nextRoomLeft;
        }**/
        if (direction == "right")
        {
            return nextRoomRight;
        }
        return null;
    }

    /**
    GetName fetches the name of the Star you are currently at.
    **/

    public string GetName()
    {
        return this.roomName;
    }

}
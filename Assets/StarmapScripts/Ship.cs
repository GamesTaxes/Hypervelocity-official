using System;

public class Ship
{
    private Star location;

    ///Updates the location of the ship.

    public Ship(Star location)
    {
        this.location = location;
    }

    ///Move is called in GameController to move the ship, as long as it doesn't try to move anywhere its not supposed to.

    public void Move(string direction)
    {
        if (location.GetNext(direction) != null)
        {
            this.location = location.GetNext(direction);
        }
    }

    ///GetLocation gets the current location of the ship

    public Star GetLocation()
    {
        return this.location;
    }



}

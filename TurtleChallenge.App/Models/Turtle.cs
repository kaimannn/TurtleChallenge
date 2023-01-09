namespace TurtleChallenge.App.Entities;

public class Turtle : Position
{
    public Enums.Direction Direction { get; set; }

    public Turtle Rotate()
    {
        if (Direction == Enums.Direction.East)
            Direction = Enums.Direction.North;

        else if (Direction == Enums.Direction.North)
            Direction = Enums.Direction.West;

        else if (Direction == Enums.Direction.West)
            Direction = Enums.Direction.South;

        else if (Direction == Enums.Direction.South)
            Direction = Enums.Direction.East;

        return this;
    }

    public Turtle Move()
    {
        if (Direction == Enums.Direction.East)
            X--;

        else if (Direction == Enums.Direction.North)
            Y++;

        else if (Direction == Enums.Direction.West)
            X++;

        else if (Direction == Enums.Direction.South)
            Y--;

        return this;
    }
}

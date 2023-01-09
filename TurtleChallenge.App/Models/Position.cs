﻿namespace TurtleChallenge.App.Entities;

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public bool IsValid(Board board) => X >= 0 && Y >= 0 && X < board.SizeX && Y < board.SizeY;
}

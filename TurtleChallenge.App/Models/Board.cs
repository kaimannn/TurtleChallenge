using System;
using System.Collections.Generic;

namespace TurtleChallenge.App.Entities;

public class Board
{
    public int SizeX { get; set; }
    public int SizeY { get; set; }

    public Position[,] Cells { get; set; }

    public void Initialize(IEnumerable<Mine> mines, Exit exit, int sizeX, int sizeY)
    {
        SizeX = sizeX;
        SizeY = sizeY;
        Fill(mines, exit);
    }

    public void Initialize(IEnumerable<Mine> mines, Exit exit) => Fill(mines, exit);

    private void Fill(IEnumerable<Mine> mines, Exit exit)
    {
        try
        {
            Cells = new Position[SizeX, SizeY];

            for (int x = 0; x < SizeX; x++)
                for (int y = 0; y < SizeY; y++)
                    Cells[x, y] = new Position();

            foreach (var mine in mines)
                Cells[mine.X, mine.Y] = new Mine();

            Cells[exit.X, exit.Y] = new Exit();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while filling the board", ex);
        }
    }

    public bool IsValid() => SizeX > 1 && SizeY > 1;
}

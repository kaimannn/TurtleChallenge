using System;
using TurtleChallenge.App.Entities;

namespace TurtleChallenge.App.Services;

public interface IGameService
{
    void Start();
}

public class GameService : IGameService
{
    private readonly IRepositoryService _loadTurtleGameService;

    public GameService(IRepositoryService fileService)
    {
        _loadTurtleGameService = fileService;
    }

    public void Start()
    {
        try
        {
            var sequences = _loadTurtleGameService.LoadSequences();
            var numSequence = 0;

            foreach (var sequence in sequences)
            {
                numSequence++;

                Console.WriteLine($"****************** Start Squence {numSequence} ******************");

                var game = CreateGame();
                var gameOver = false;

                foreach (var action in sequence.Actions)
                {
                    gameOver = PlayGame(game, action);
                    if (gameOver)
                        break;
                };

                EndGame(numSequence, game, gameOver);

                Console.WriteLine($"*****************************************************");
                Console.WriteLine(Environment.NewLine);
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error has ocurred during the game -> {ex.Message}");
        }
    }

    private Game CreateGame()
    {
        var game = _loadTurtleGameService.LoadGame();
        game.Validate();
        game.Board.Initialize(game.Mines, game.Exit);

        return game;
    }

    private bool PlayGame(Game game, Enums.Action action)
    {
        if (action == Enums.Action.Rotate)
        {
            game.Turtle.Rotate();

            Console.WriteLine($"Turtle has rotated to: {game.Turtle.Direction}");

            return false;
        }

        if (game.IsMovementAllowed(game.Board, game.Turtle))
        {
            game.Turtle.Move();

            Console.WriteLine($"Turtle has moved to cell: [{game.Turtle.X}, {game.Turtle.Y}]");

            return game.IsGameOver();
        }

        Console.WriteLine($"Warning: Turtle is trying to escape from the cell: [{game.Turtle.X}, {game.Turtle.Y}]!");

        return false;
    }

    private void EndGame(int numSequence, Game game, bool gameOver)
    {
        if (!gameOver)
        {
            Console.WriteLine($"Squence {numSequence}, result: Still in danger!");
            return;
        }

        var endCell = game.Board.Cells[game.Turtle.X, game.Turtle.Y];
        if (endCell is Mine)
            Console.WriteLine($"Squence {numSequence}, result: Mine hit!");
        else
            Console.WriteLine($"Squence {numSequence}, result: Success!");
    }
}
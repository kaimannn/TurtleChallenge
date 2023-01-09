using System.Collections.Generic;
using TurtleChallenge.App.Entities;

namespace TurtleChallenge.App.Services;

public interface IRepository
{
    Game LoadGame();
    IEnumerable<Sequence> LoadSequences();
}

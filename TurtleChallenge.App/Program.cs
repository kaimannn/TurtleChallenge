using Microsoft.Extensions.DependencyInjection;
using System;
using TurtleChallenge.App.Services;

string jsonFileSettings = "game-settings.json";
string jsonFileSequences = "game-moves.json";
int exitCode = -1;

try
{
    var serviceProvider = new ServiceCollection()
        .AddSingleton<IRepositoryService>(sp => new LoadJsonConfigService(jsonFileSettings, jsonFileSequences))
        .AddSingleton<IGameService, GameService>()
        .BuildServiceProvider();

    serviceProvider.GetRequiredService<IGameService>().Start();
}
catch (Exception ex)
{
    exitCode = -1;
    Console.WriteLine("Exception In Main: {0}\r\nStackTrace: {1}", ex.Message, ex.StackTrace);
}

Environment.Exit(exitCode);


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TurtleChallenge.App.Entities;

namespace TurtleChallenge.App.Services;

public class LoadJsonConfigService : IRepository
{
    public string JsonGameSettings { get; set; }
    public string JsonGameSequences { get; set; }

    public LoadJsonConfigService(string gameSettings, string gameSequences)
    {
        JsonGameSettings = gameSettings;
        JsonGameSequences = gameSequences;
    }

    public Game LoadGame()
    {
        try
        {
            return JsonConvert.DeserializeObject<Game>(System.IO.File.ReadAllText(JsonGameSettings));
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred reading {JsonGameSettings} file", ex);
        }
    }

    public IEnumerable<Sequence> LoadSequences()
    {
        try
        {
            return JsonConvert.DeserializeObject<IEnumerable<Sequence>>(System.IO.File.ReadAllText(JsonGameSequences));
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred reading {JsonGameSequences} file", ex);
        }
    }
}

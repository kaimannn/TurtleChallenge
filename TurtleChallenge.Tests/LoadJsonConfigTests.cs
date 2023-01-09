using NUnit.Framework;
using System;
using System.IO;
using TurtleChallenge.App.Services;

namespace TurtleChallenge.Tests;

[TestFixture]
public class LoadJsonConfigTests
{
    private readonly string _fileSequences = @"TurtleChallenge.App\game-moves.json";
    private readonly string _fileSettings = @"TurtleChallenge.App\game-settings.json";

    private LoadJsonConfigService _loadJsonConfig;

    [SetUp]
    public void Setup()
    {
        _loadJsonConfig = new LoadJsonConfigService(
            Path.Combine(AppContext.BaseDirectory[..AppContext.BaseDirectory.IndexOf(typeof(LoadJsonConfigTests).Namespace)], _fileSettings),
            Path.Combine(AppContext.BaseDirectory[..AppContext.BaseDirectory.IndexOf(typeof(LoadJsonConfigTests).Namespace)], _fileSequences));
    }

    [Test]
    public void LoadSequences_WhenCalled_ReturnSequences()
    {
        var sequences = _loadJsonConfig.LoadSequences();

        Assert.That(sequences, Is.All.Not.Null);
    }

    [Test]
    [TestCase("xxx.json")]
    [TestCase("")]
    [TestCase(null)]
    public void LoadSequences_WhenErrorOccurs_ThrowsException(string jsonFilePath)
    {
        _loadJsonConfig.JsonGameSequences = jsonFilePath;
        var ex = Assert.Throws<Exception>(() => _loadJsonConfig.LoadSequences());

        Assert.That(ex.Message, Is.EqualTo($"An error occurred reading {jsonFilePath} file"));
    }

    [Test]
    public void LoadGame_WhenCalled_ReturnGame()
    {
        var game = _loadJsonConfig.LoadGame();

        Assert.That(game, Is.Not.Null);
    }

    [Test]
    [TestCase("xxx.json")]
    [TestCase("")]
    [TestCase(null)]
    public void LoadGame_WhenErrorOccurs_ThrowsException(string jsonFilePath)
    {
        _loadJsonConfig.JsonGameSettings = jsonFilePath;
        var ex = Assert.Throws<Exception>(() => _loadJsonConfig.LoadGame());

        Assert.That(ex.Message, Is.EqualTo($"An error occurred reading {jsonFilePath} file"));
    }
}
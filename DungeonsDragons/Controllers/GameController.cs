using System.Net.Http.Headers;
using DungeonsDragons.Services.LogConvertingService;
using DungeonsDragons.ViewModels;
using GameModels;
using Microsoft.AspNetCore.Mvc;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DungeonsDragons.Controllers;

public class GameController(HttpClient? client,ILogConvertingService logConvertingService) : Controller
{
    private readonly HttpClient? _client = client;
    private const string GameServer = "https://localhost:1002/";
    public async Task<IActionResult> StartGame(string name, int hp, int attackModifier, int apr, string damage,
        int damageModifier, int ac)
    {
        _client!.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        
        var player = new Player
        {
            Id = 0,
            Name = name,
            HitPoints = hp,
            AttackModifier = attackModifier,
            AttackPerRound = apr,
            Damage = damage,
            DamageModifier = damageModifier,
            Ac = ac
        };
        
        var monster = await GetMonster();
        
        var opponents = new Opponents { Player = player, Monster = monster };

        var fightLog = await GetFightResult(opponents);

        var viewModel = new GameViewModel
        {
            Player = player,
            Monster = monster,
            FightLog = fightLog.Item1,
            IsPlayerLose = fightLog.Item2
        };

        return View(viewModel);
    }

    private async Task<Monster> GetMonster()
    {
        var requestMessage = new HttpRequestMessage();
        requestMessage.Method = HttpMethod.Get;
        requestMessage.RequestUri = new Uri($"{GameServer}GetRandomMonster");
        using var response = await _client!.SendAsync(requestMessage);

        if (!response.IsSuccessStatusCode) 
            throw new ArgumentException("Ошибка в получении монстра");
        
        var monster = await response.Content.ReadFromJsonAsync<Monster>();
        return monster!;
    }

    private async Task<(List<string>, bool)> GetFightResult(Opponents opponents)
    {
        var json = JsonSerializer.Serialize(opponents);
        var requestMessage = new HttpRequestMessage();
        requestMessage.Method = HttpMethod.Post;
        requestMessage.RequestUri = new Uri($"{GameServer}GetFightResult?opponents={json}");
        using var response = await _client!.SendAsync(requestMessage);

        if (!response.IsSuccessStatusCode)
            throw new ArgumentException("Ошибка в получении итогов боя");
        
        var content = await response.Content.ReadFromJsonAsync<List<Round>>();
        var fightLog = logConvertingService.ConvertToLog(content!);
        return fightLog;
    }
}
using GameModels;
using GameServer.Services.LogicService;
using GameServer.Services.MonsterService;
using Microsoft.AspNetCore.Mvc;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace GameServer.Controllers;

[ApiController]
public class GameController(ILogicService logicService, IRandomMonsterService monsterService) : ControllerBase
{
    [HttpGet]
    [Route("GetRandomMonster")]
    public Monster GetRandomMonster() => monsterService.GetRandomMonster();
    
    [HttpPost]
    [Route("GetFightResult")]
    public List<Round>? GetFightResult(string opponents) => logicService.StartGame(JsonSerializer.Deserialize<Opponents>(opponents)!);
}
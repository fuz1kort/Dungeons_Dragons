using GameModels;

namespace GameServer.Services.LogicService;

public interface ILogicService
{
    public List<Round>? StartGame(Opponents opponents);
}
using GameModels;

namespace GameServer.Services.MonsterService;

public interface IRandomMonsterService
{
    public Monster GetRandomMonster();
}
using GameModels;
using GameServer.Data;

namespace GameServer.Services.MonsterService;

public class RandomMonsterService(AppDbContext context) : IRandomMonsterService
{
    public Monster GetRandomMonster()
    {
        IQueryable<Monster> monsters = context.Monsters!;
        var rnd = new Random();
        var id = rnd.Next(0,monsters.Count());
        return monsters.FirstOrDefault(x => x.Id == id)!;
    }
}
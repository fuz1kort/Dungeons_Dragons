using GameModels;
using GameServer.Data;

namespace GameServer.Services.MonsterService;

public class RandomMonsterService(AppDbContext context) : IRandomMonsterService
{
    public Monster GetRandomMonster()
    {
        IQueryable<Monster> monsters = context.Monsters!;
        var rnd = new Random();
        var id = rnd.Next(1, monsters.Count() + 1);
        return monsters.FirstOrDefault(x => x.Id == id)!;
    }
}
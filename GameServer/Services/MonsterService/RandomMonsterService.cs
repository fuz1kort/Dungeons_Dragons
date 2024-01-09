using GameModels;
using GameServer.Data;

namespace GameServer.Services.MonsterService;

public class RandomMonsterService(AppDbContext context) : IRandomMonsterService
{
    public Monster GetRandomMonster()
    {
        IEnumerable<Monster> monsters = context.Monsters!;
        var rnd = new Random();
        var id = rnd.Next(1,monsters.Count()+1);
        return monsters.First(m => m.Id == id);
    }
}
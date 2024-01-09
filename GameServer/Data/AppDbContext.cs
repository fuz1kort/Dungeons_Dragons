using GameModels;
using Microsoft.EntityFrameworkCore;

namespace GameServer.Data;

public class AppDbContext: DbContext
{
    public DbSet<Monster>? Monsters { get; set; }

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var noori = new Monster
        {
            Id = 1,
            Name = "Ноори",
            HitPoints = 22,
            AttackModifier = 2,
            AttackPerRound = 1,
            Damage = "1d6",
            DamageModifier = 2,
            Ac = 15
        };
        var goblin = new Monster
        {
            Id = 2,
            Name = "Гоблин",
            HitPoints = 7,
            AttackModifier = -1,
            AttackPerRound = 1,
            Damage = "1d6",
            DamageModifier = 2,
            Ac = 15 
        };
        var skeleton = new Monster
        {
            Id = 3,
            Name = "Скелет бладфина",
            HitPoints = 13,
            AttackModifier = 0,
            AttackPerRound = 1,
            Damage = "1d6",
            DamageModifier = 2,
            Ac = 13
        };
        var ottokent = new Monster
        {
            Id = 4,
            Name = "Оттокент",
            HitPoints = 80,
            AttackModifier = 5,
            AttackPerRound = 2,
            Damage = "2d7",
            DamageModifier = 4,
            Ac = 20
        };
        var scorpion = new Monster
        {
            Id = 5,
            Name = "Скорпион",
            HitPoints = 30,
            AttackModifier = 15,
            AttackPerRound = 1,
            Damage = "2d5",
            DamageModifier = 2,
            Ac = 5
        };
        modelBuilder.Entity<Monster>().HasData(noori, goblin, skeleton, ottokent, scorpion);
        base.OnModelCreating(modelBuilder);
    }
}
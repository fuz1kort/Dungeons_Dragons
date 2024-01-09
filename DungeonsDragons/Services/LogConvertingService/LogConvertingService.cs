using GameModels;

namespace DungeonsDragons.Services.LogConvertingService;

public class LogConvertingService: ILogConvertingService
{
    public (List<string>, bool) ConvertToLog(List<Round> fightLog)
    {
        var builder = new List<string>();
        var isPlayerLose = false;
        
        foreach (var round in fightLog)
        {
            builder.Add($"Раунд {round.Id}");
            foreach (var fight in round.Rounds!)
            {
                Creature attackCreature, aimCreature;
                if (fight.IsPlayerTurn)
                {
                    attackCreature = fight.Player!;
                    aimCreature = fight.Monster!;
                }
                else
                {
                    attackCreature = fight.Monster!;
                    aimCreature = fight.Player!;
                }

                builder.Add(attackCreature.Name!);
                switch (fight.Status)
                {
                    case HitStatusType.CriticalHit:
                        builder.Add(
                            $"{fight.AttackDice} (+{attackCreature.AttackModifier}) критическое попадание. " +
                            $"{fight.DamageDice} (+{attackCreature.DamageModifier}) * 2 наносит {fight.Damage} " +
                            $"урона игроку {aimCreature.Name}({aimCreature.HitPoints}). ");
                        break;
                    case HitStatusType.CriticalMiss:
                        builder.Add($"{fight.AttackDice}(+{attackCreature.AttackModifier}) критический промах. ");
                        break;
                    case HitStatusType.Hit:
                        builder.Add(
                            $"{fight.AttackDice} (+{attackCreature.AttackModifier}) больше {aimCreature.Ac}, попадание. " +
                            $"{fight.DamageDice} (+{attackCreature.DamageModifier}) наносит {fight.Damage} " +
                            $"урона игроку {aimCreature.Name}({aimCreature.HitPoints}). ");
                        break;
                    case HitStatusType.Miss:
                        builder.Add(
                            $"{fight.AttackDice}(+{attackCreature.AttackModifier}) меньше {aimCreature.Ac}, промах. ");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (!fight.Win) 
                    continue;
                
                if (attackCreature == fight.Monster!) 
                    isPlayerLose = true;
                
                builder.Add($"{aimCreature.Name} мертв(а). {attackCreature.Name} победил(а).");
                break;
            }
        }

        return (builder,isPlayerLose);
    }
}
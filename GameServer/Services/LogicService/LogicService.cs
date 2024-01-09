using GameModels;
using GameServer.Models;

namespace GameServer.Services.LogicService;

public class LogicService : ILogicService
{
    private AttackResult? _attackResult;
    private List<Round>? _fightLog;
    private int _roundCounts;

    public List<Round>? StartGame(Opponents opponents)
    {
        _attackResult = new AttackResult(opponents);
        _fightLog = new List<Round>();
        while (!_attackResult.Win)
        {
            var round = Fight();
            _fightLog.Add(round);
        }
        
        return _fightLog;
    }

    private Round Fight()
    {
        var currentRound = new Round { Id = ++_roundCounts, Rounds = new List<AttackResult>()};
        var playerAttackCount = _attackResult!.Player!.AttackPerRound;
        var monsterAttackCount = _attackResult.Monster!.AttackPerRound;
        while (playerAttackCount != 0 || monsterAttackCount != 0)
        {
            if (_attackResult.IsPlayerTurn)
            {
                if (playerAttackCount == 0)
                {
                    _attackResult.IsPlayerTurn = false;
                    continue;
                }

                currentRound.Rounds.Add(Attack(_attackResult.Player!, _attackResult.Monster!));
                _attackResult.IsPlayerTurn = false;
                playerAttackCount--;
            }
            else
            {
                if (monsterAttackCount == 0)
                {
                    _attackResult.IsPlayerTurn = true;
                    continue;
                }

                currentRound.Rounds.Add(Attack(_attackResult.Monster!, _attackResult.Player!));
                _attackResult.IsPlayerTurn = true;
                monsterAttackCount--;
            }
        }

        return currentRound;
    }

    private AttackResult Attack(Creature attackCreature, Creature aimCreature)
    {
        var attackDice = Dice.DiceTwenty.Roll();
        _attackResult!.AttackDice = attackDice;
        switch (attackDice)
        {
            case 20:
                aimCreature.HitPoints -= CalculateDamage(true, attackCreature);
                _attackResult.Status = HitStatusType.CriticalHit;
                break;
            case 1:
                _attackResult.Status = HitStatusType.CriticalMiss;
                break;
            default:
            {
                if (attackDice + attackCreature.MinAcToHit >= aimCreature.Ac)
                {
                    aimCreature.HitPoints -= CalculateDamage(false, attackCreature);
                    _attackResult.Status = HitStatusType.Hit;
                }
                else
                {
                    _attackResult.Status = HitStatusType.Miss;
                }

                break;
            }
        }

        if (aimCreature.HitPoints <= 0) 
            _attackResult.Win = true;

        return _attackResult.Clone();
    }
    
    private int CalculateDamage(bool isCriticalHits, Creature creature)
    {
        var attackDamage = 0;
        var numbOfThrows = int.Parse(creature.Damage![0].ToString());
        var damageDice = new Dice(int.Parse(creature.Damage![2].ToString()));

        for (var j = 0; j < numbOfThrows; j++) 
            attackDamage += damageDice.Roll();

        _attackResult!.DamageDice = attackDamage;
        attackDamage += creature.DamageModifier;
        if (isCriticalHits) attackDamage *= 2;
        _attackResult.Damage = attackDamage;
        return attackDamage;
    }
}
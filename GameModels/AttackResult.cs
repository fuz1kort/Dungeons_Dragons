using Newtonsoft.Json;

namespace GameModels;

public class AttackResult : Opponents
{
    public HitStatusType Status { get; set; }
    public bool IsPlayerTurn { get; set; }
    public int AttackDice { get; set; }
    public int DamageDice { get; set; }
    public int Damage { get; set; }
    public bool Win { get; set; }

    public AttackResult()
    {
    }
    
    public AttackResult(Opponents opponents)
    {
        Player = opponents.Player;
        Monster = opponents.Monster;
        IsPlayerTurn = true;
        Win = false;
    }
    
    public AttackResult Clone()
    {
        var serialized = JsonConvert.SerializeObject(this);
        return JsonConvert.DeserializeObject<AttackResult>(serialized)!;
    }
}
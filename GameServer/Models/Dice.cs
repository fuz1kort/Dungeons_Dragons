namespace GameServer.Models;

public class Dice(int count)
{
    public int NumberOfFaces { get; } = count;

    public int Roll()
    {
        var rnd = new Random();
        return rnd.Next(1, NumberOfFaces);
    }
    
    public static Dice DiceTwenty => new(20);
}
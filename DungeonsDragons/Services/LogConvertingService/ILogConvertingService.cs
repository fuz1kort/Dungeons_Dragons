using GameModels;

namespace DungeonsDragons.Services.LogConvertingService;

public interface ILogConvertingService
{
    public (List<string>, bool) ConvertToLog(List<Round> fightLog);
}
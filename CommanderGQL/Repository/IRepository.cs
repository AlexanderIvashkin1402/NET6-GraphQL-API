using CommanderGQL.Models;

namespace CommanderGQL.Repository;

public interface IRepository
{
    IEnumerable<Platform> GetPlatforms();
    IEnumerable<Command> GetCommands();
    IEnumerable<Command> GetCommandsByPlatform(int platformId);
    Platform GetPlatform(int platformId);
    Task<ILookup<int, Command>> GetCommandsByPlatformAsync(IEnumerable<int> platformIds);
}

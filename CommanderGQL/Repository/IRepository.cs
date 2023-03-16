using CommanderGQL.Models;

namespace CommanderGQL.Repository;

public interface IRepository
{
    IEnumerable<Platform> GetPlatforms();
    Task<Platform> AddPlatformAsync(Platform platform);
    IEnumerable<Command> GetCommands();
    IEnumerable<Command> GetCommandsByPlatform(int platformId);
    Platform GetPlatform(int platformId);
    Task<ILookup<int, Command>> GetCommandsByPlatformAsync(IEnumerable<int> platformIds);
    Task<Command> AddCommandAsync(Command command);
}

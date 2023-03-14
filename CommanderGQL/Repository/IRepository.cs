using CommanderGQL.Models;

namespace CommanderGQL.Repository;

public interface IRepository
{
    IEnumerable<Platform> GetPlatforms();
    IEnumerable<Command> GetCommandsByPlatform(int platformId);
    Platform GetPlatform(int platformId);
}

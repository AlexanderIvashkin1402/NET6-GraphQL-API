using CommanderGQL.Models;

namespace CommanderGQL.Repository;

public interface IRepository
{
    IEnumerable<Platform> GetPlatforms();
}

using CommanderGQL.Data;
using CommanderGQL.Models;

namespace CommanderGQL.Repository
{
    public class SqlDbRepository : IRepository
    {
        private readonly IServiceProvider _serviceProvider;

        public SqlDbRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<Platform> GetPlatforms()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                return appDbContext.Platforms.ToList();
            }
        }
    }
}

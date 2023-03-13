using CommanderGQL.Data;
using CommanderGQL.Models;
using Microsoft.EntityFrameworkCore;

namespace CommanderGQL.Repository
{
    public class SqlDbRepository : IRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public SqlDbRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<Platform> GetPlatforms()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return context.Platforms.ToList();
            }
        }
    }
}

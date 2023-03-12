using CommanderGQL.GraphQL.Queries;
using GraphQL.Types;

namespace CommanderGQL.GraphQL.Shcemas;

public class PlatformSchema : Schema
{
    public PlatformSchema(IServiceProvider serviceProvider) : base(serviceProvider, true)
    {
        Query = serviceProvider.GetRequiredService<PlatformQuery>();
    }
}

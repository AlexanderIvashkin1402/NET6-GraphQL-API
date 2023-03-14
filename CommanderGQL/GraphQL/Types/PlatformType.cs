using CommanderGQL.Models;
using CommanderGQL.Repository;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace CommanderGQL.GraphQL.Types;

public class PlatformType : ObjectGraphType<Platform>
{
    public PlatformType(IRepository repository, IDataLoaderContextAccessor dataLoaderAccessor)
    {
        Description = "Software Platform";

        Field(x => x.Id).Description("Platformm Id");
        Field(x => x.Name).Description("Platform Name");
        Field(x => x.LicenseKey).Description("Platform License Key");
        Field<ListGraphType<CommandType>>(nameof(Platform.Commands))
            .ResolveAsync(async context => {
                var loader =
                        dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int, Command>(
                            "GetCommandsByPlatformId", repository.GetCommandsByPlatformAsync);
                return await loader.LoadAsync(context.Source.Id).GetResultAsync();
            })
            .Description("Platform's Commands");
    }
}
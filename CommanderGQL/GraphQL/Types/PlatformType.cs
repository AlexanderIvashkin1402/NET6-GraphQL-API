using CommanderGQL.Models;
using CommanderGQL.Repository;
using GraphQL.Types;

namespace CommanderGQL.GraphQL.Types;

public class PlatformType : ObjectGraphType<Platform>
{
    private readonly IRepository _repository;

    public PlatformType(IRepository repository)
    {
        _repository = repository;

        Description = "Software Platform";

        Field(x => x.Id).Description("Platformm Id");
        Field(x => x.Name).Description("Platform Name");
        Field(x => x.LicenseKey).Description("Platform License Key");
        Field<ListGraphType<CommandType>>(nameof(Platform.Commands))
            .Resolve(x => _repository.GetCommandsByPlatform(x.Source.Id))
            .Description("Platform's Commands");
    }
}
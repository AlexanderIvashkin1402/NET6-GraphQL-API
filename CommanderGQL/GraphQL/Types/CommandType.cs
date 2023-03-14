using CommanderGQL.Models;
using CommanderGQL.Repository;
using GraphQL.Types;

namespace CommanderGQL.GraphQL.Types;

public class CommandType :  ObjectGraphType<Command>
{
    private readonly IRepository _repository;

    public CommandType(IRepository repository)
    {
        _repository = repository;
        
        Description = "Command to execute action";

        Field(x => x.Id).Description("Command Id");
        Field(x => x.HowTo).Description("How to execute action");
        Field(x => x.CommandLine).Description("Command to execute action");
        Field<PlatformType>("platform")
            .Resolve(context => _repository.GetPlatform(context.Source.PlatformId))
            .Description("Command Platform");
    }
}

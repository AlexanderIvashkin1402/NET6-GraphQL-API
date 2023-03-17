using CommanderGQL.GraphQL.Messaging;
using CommanderGQL.Models;
using CommanderGQL.Repository;
using GraphQL;
using GraphQL.Types;

namespace CommanderGQL.GraphQL.Types;

public class PlatformMutation : ObjectGraphType
{
    public PlatformMutation(IRepository repository, PlatformMessageService messageService)
    {
        Field<PlatformType>("addPlatform")
            .Argument<NonNullGraphType<PlatformInputType>>("platform")
            .ResolveAsync(async context =>
                {
                    var platform = context.GetArgument<Platform>("platform");
                    await repository.AddPlatformAsync(platform);
                    messageService.AddPlatformAddedMessage(platform);
                    return platform;
                });

        Field<CommandType>("addCommand")
            .Argument<NonNullGraphType<CommandInputType>>("command")
            .ResolveAsync(async context =>
            {
                var command = context.GetArgument<Command>("command");
                return await repository.AddCommandAsync(command);
            });
    }
}

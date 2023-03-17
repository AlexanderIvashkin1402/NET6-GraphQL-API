using CommanderGQL.GraphQL.Messaging;
using GraphQL.Resolvers;
using GraphQL.Types;

namespace CommanderGQL.GraphQL.Types;

public class PlatformSubscription : ObjectGraphType
{
    public PlatformSubscription(PlatformMessageService messageService)
    {
        Name = "Subscription";
        AddField(new FieldType
        {
            Name = "platformAdded",
            Type = typeof(PlatformAddedMessageType),
            Resolver = new FuncFieldResolver<PlatformAddedMessage>(c => c.Source as PlatformAddedMessage),
            StreamResolver = new SourceStreamResolver<PlatformAddedMessage>(c => messageService.GetMessages())
        });
    }
}

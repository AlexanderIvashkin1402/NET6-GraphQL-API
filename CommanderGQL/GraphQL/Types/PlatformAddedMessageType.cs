using CommanderGQL.GraphQL.Messaging;
using GraphQL.Types;

namespace CommanderGQL.GraphQL.Types;

public class PlatformAddedMessageType : ObjectGraphType<PlatformAddedMessage>
{
    public PlatformAddedMessageType()
    {
        Field(x => x.Id);
        Field(x => x.Name);
    }
}

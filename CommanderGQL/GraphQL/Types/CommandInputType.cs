using GraphQL.Types;

namespace CommanderGQL.GraphQL.Types;

public class CommandInputType : InputObjectGraphType
{
    public CommandInputType()
    {
        Name = "commandInput";

        Field<NonNullGraphType<StringGraphType>>("howTo");
        Field<NonNullGraphType<StringGraphType>>("commandLine");
        Field<NonNullGraphType<IntGraphType>>("platformId");
    }
}

using GraphQL.Types;

namespace CommanderGQL.GraphQL.Types;

public class PlatformInputType : InputObjectGraphType
{
    public PlatformInputType()
    {
        Name = "platformInput";

        Field<NonNullGraphType<StringGraphType>>("name");
        Field<StringGraphType>("licenseKey");
    }
}

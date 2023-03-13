using CommanderGQL.Models;
using GraphQL.Types;

namespace CommanderGQL.GraphQL.Types;

public class PlatformType : ObjectGraphType<Platform>
{
    public PlatformType()
    {
        Description = "Software Platform";

        Field(x => x.Id);
        Field(x => x.Name);
        Field(x => x.LicenseKey);
    }
}
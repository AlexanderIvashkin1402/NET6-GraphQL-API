using GraphQL;
using GraphQL.Types;
using CommanderGQL.GraphQL.Types;
using CommanderGQL.Repository;

namespace CommanderGQL.GraphQL.Queries;

public class PlatformQuery : ObjectGraphType
{
    public PlatformQuery(IRepository repository)
    {
        Description = "Query to get Platforms and Commands";

        Field<ListGraphType<PlatformType>>("platforms")
            .Argument<IdGraphType>("id")
            .Argument<StringGraphType>("name")
            .Argument<StringGraphType>("licenseKey")
            .Resolve(context =>
                {
                    var query = repository.GetPlatforms().ToList();

                    var platfromId = context.GetArgument<int?>("id");
                    if (platfromId.HasValue)
                    {
                        var result = query.Where(p => p.Id == platfromId.Value);
                        return result;
                    }

                    var name = context.GetArgument<string?>("name");
                    if (!string.IsNullOrEmpty(name))
                    {
                        return query
                            .Where(p => !string.IsNullOrEmpty(p.Name) && p.Name.Equals(name));
                    }

                    var licenseKey = context.GetArgument<string?>("licenseKey");
                    if (!string.IsNullOrEmpty(licenseKey))
                    {
                        return query
                            .Where(p => !string.IsNullOrEmpty(p.LicenseKey) && p.LicenseKey.Equals(licenseKey));
                    }

                    return query;
                })
            .Description("Query to get Platforms");

        Field<ListGraphType<CommandType>>("commands")
            .Argument<IdGraphType>("id")
            .Argument<StringGraphType>("howTo")
            .Resolve(context =>
            {
                var query = repository.GetCommands().ToList();

                var commandId = context.GetArgument<int?>("id");
                if (commandId.HasValue)
                {
                    var result = query.Where(c => c.Id == commandId.Value);
                    return result;
                }

                var howTo = context.GetArgument<string?>("howTo");
                if (!string.IsNullOrEmpty(howTo))
                {
                    return query
                        .Where(c => !string.IsNullOrEmpty(c.HowTo) && c.HowTo.Equals(howTo, StringComparison.OrdinalIgnoreCase));
                }

                return query;
            })
            .Description("Query to get Commands");
    }
}

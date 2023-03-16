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
            .Argument<StringGraphType>("orderBy")
            .Argument<IdGraphType>("skip")
            .Argument<IdGraphType>("take")
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

                    var sortOrder = context.GetArgument<string?>("orderBy");
                    if (!string.IsNullOrEmpty(sortOrder))
                    {
                        var order = SortOrder.ASC;
                        var lexems = sortOrder.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                        if (lexems.Length == 2)
                        {
                            Enum.TryParse<SortOrder>(lexems[1].ToUpper(), out order);
                        }

                        if (lexems[0].Equals("name", StringComparison.OrdinalIgnoreCase))
                        {
                            query = order == SortOrder.ASC 
                                ? query.OrderBy(p => p.Name).ToList()
                                : query.OrderByDescending(p => p.Name).ToList();
                        }                        
                    }

                    var skip = context.GetArgument<int?>("skip");
                    if (skip.HasValue)
                    {
                        query = query.Skip(skip.Value).ToList();
                    }

                    var take = context.GetArgument<int?>("take");
                    if (take.HasValue)
                    {
                        query = query.Take(take.Value).ToList();
                    }

                    return query;
                })
            .Description("Query to get Platforms");

        Field<ListGraphType<CommandType>>("commands")
            .Argument<IdGraphType>("id")
            .Argument<IdGraphType>("platformId")
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
                
                var platformId = context.GetArgument<int?>("platformId");
                if (platformId.HasValue)
                {
                    var result = query.Where(c => c.Platform?.Id == platformId.Value);
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

public enum SortOrder
{
    ASC,
    DESC
}

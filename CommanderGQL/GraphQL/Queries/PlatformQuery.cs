using GraphQL;
using GraphQL.Types;
using CommanderGQL.GraphQL.Types;
using CommanderGQL.Repository;

namespace CommanderGQL.GraphQL.Queries;

public class PlatformQuery : ObjectGraphType
{
    private readonly IRepository _repository;

    public PlatformQuery(IRepository repository)
    {
        _repository = repository;

        Description = "Query to get Platforms";

        Field<ListGraphType<PlatformType>>("platforms")
            .Argument<IdGraphType>("id")
            .Argument<StringGraphType>("name")
            .Argument<StringGraphType>("licenseKey")
            .Resolve(context =>
                {
                    var query = _repository.GetPlatforms().ToList();

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
                });
     }
}

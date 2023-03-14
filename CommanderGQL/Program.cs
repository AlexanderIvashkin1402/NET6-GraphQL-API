using CommanderGQL.Data;
using GraphQL;
using Microsoft.EntityFrameworkCore;
using CommanderGQL.GraphQL.Queries;
using CommanderGQL.GraphQL.Schemas;
using CommanderGQL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPooledDbContextFactory<AppDbContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("CommandConStr")));
builder.Services.AddSingleton<IRepository, SqlDbRepository>();

builder.Services.AddGraphQL(b => b
    .AddSystemTextJson()
    .AddSelfActivatingSchema<PlatformSchema>()
    .AddGraphTypes()
    .AddDataLoader()
);

// must manually register the query and mutation types or AOT will trim their constructors
// all other graph types' constructors are preserved via calls to Field<T>
builder.Services.AddTransient<PlatformQuery>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseGraphQL<PlatformSchema>();

if (app.Environment.IsDevelopment())
{
    app.UseGraphQLPlayground();
}

app.UseGraphQLVoyager("/graphql/graphql-voyager");

app.Run();

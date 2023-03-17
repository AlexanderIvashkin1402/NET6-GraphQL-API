using CommanderGQL.Data;
using GraphQL;
using Microsoft.EntityFrameworkCore;
using CommanderGQL.GraphQL.Schemas;
using CommanderGQL.Repository;
using CommanderGQL.GraphQL.Messaging;
using Microsoft.AspNetCore.WebSockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPooledDbContextFactory<AppDbContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("CommandConStr")));
builder.Services.AddSingleton<IRepository, SqlDbRepository>();
builder.Services.AddSingleton<PlatformMessageService>();
builder.Services.AddWebSockets(configure: options =>
{
    options.KeepAliveInterval = TimeSpan.FromMinutes(5);
});

builder.Services.AddGraphQL(b => b
    .AddSystemTextJson()
    .AddSelfActivatingSchema<PlatformSchema>()
    .AddGraphTypes()
    .AddDataLoader()
);

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseGraphQLPlayground();
}

app.UseWebSockets();
app.UseGraphQL<PlatformSchema>("/graphql", options =>
{
    options.HandleWebSockets = true;
});
app.UseGraphQLVoyager("/graphql/graphql-voyager");

app.Run();

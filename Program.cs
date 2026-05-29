using UserAccountsApi.Routes;
using DotNetEnv;
using UserAccountsApi.Lib;

Env.Load();
EnvVars.CheckEnvVars();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

MainRouter.MapAPi(app);

app.Run();


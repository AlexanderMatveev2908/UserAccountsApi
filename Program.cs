using DotNetEnv;
using UserAccountsApi.Lib;
using UserAccountsApi.Databases.RedisDbNS;
using UserAccountsApi.Config;

Env.Load();
EnvVarsLib.CheckEnvVars();

var builder = WebApplication.CreateBuilder(args);
SettingsConfig.ConfigureBuilder(builder);


var app = builder.Build();
await RedisDb.Connect();
SettingsConfig.ConfigureApp(app);

app.Run();


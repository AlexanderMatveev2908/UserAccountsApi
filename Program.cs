using UserAccountsApi.Routes;
using DotNetEnv;
using UserAccountsApi.Lib;
using UserAccountsApi.Databases.RedisDbNS;
;

Env.Load();
EnvVarsLib.CheckEnvVars();

var builder = WebApplication.CreateBuilder(args);
SettingsLib.ConfigureBuilder(builder);


var app = builder.Build();
await RedisDb.Connect();
SettingsLib.ConfigureApp(app);

app.Run();


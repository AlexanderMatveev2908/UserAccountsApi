using DotNetEnv;
using UserAccountsApi.LibNS;
using UserAccountsApi.ConfigNS.RedisNS;
using UserAccountsApi.ConfigNS;


Env.Load();
EnvVarsLib.CheckEnvVars();

var builder = WebApplication.CreateBuilder(args);
SettingsConf.ConfigureBuilder(builder);

var app = builder.Build();
await RedisConf.Connect();
SettingsConf.ConfigureApp(app);

app.Run();


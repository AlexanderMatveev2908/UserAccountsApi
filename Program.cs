using DotNetEnv;
using UserAccountsApi.LibNS;
using UserAccountsApi.ConfigNS.RedisNS;
using UserAccountsApi.ConfigNS;


Env.Load();
EnvVarsLib.CheckEnvVars();

var builder = WebApplication.CreateBuilder(args);
SettingsConf.ConfigureBuilder(builder);

await RedisConf.Connect();

var app = builder.Build();
SettingsConf.ConfigureApp(app);

app.Run();


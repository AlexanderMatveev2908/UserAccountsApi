using UserAccountsApi.LibNS;
using UserAccountsApi.RoutesNS;
using UserAccountsApi.MiddlewareNS;

namespace UserAccountsApi.ConfigNS;

public static class SettingsConf
{
  public static void ConfigureBuilder(WebApplicationBuilder builder)
  {
    builder.Services.AddOpenApi();

    builder.Services.AddCors(options =>
    {
      options.AddPolicy(
      "Frontend",
      policy =>
      {
        policy
        .WithOrigins(
            EnvVarsLib.Get("FRONTEND_URL")
        )
        .AllowAnyMethod()
        .AllowAnyHeader();
      }
  );
    });

    builder.WebHost.ConfigureKestrel(options =>
   {
     options.Limits.MaxRequestBodySize =
     1024 * 1024 * 500;
   });
  }

  public static void ConfigureApp(WebApplication app)
  {
    if (app.Environment.IsDevelopment())
      app.MapOpenApi();


    app.UseHttpsRedirection();
    app.UseCors("Frontend");
    MainMdw.UseMainMdw(app);

    MainRouter.MapAPi(app);
  }
}
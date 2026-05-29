using UserAccountsApi.Lib;
using UserAccountsApi.Routes;

namespace UserAccountsApi.Config;

public static class SettingsConfig
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

    MainRouter.MapAPi(app);
  }
}
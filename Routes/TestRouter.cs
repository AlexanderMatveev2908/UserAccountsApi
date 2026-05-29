using UserAccountsApi.ExtensionsNS.RateLimitNS;

namespace UserAccountsApi.RoutesNS;

public static class TestRouter
{

  public static void MapAPi(RouteGroupBuilder api)
  {
    api.MapGet("test", () =>
    {
      return Results.Json(new
      {
        msg = "Hello World",
        status = 200
      });
    }).WithRateLimit(
        TimeSpan.FromMinutes(5),
        5
    );
  }
}
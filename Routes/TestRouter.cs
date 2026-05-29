namespace UserAccountsApi.RoutesNS;

public static class TestRouter
{

  public static void MapAPi(RouteGroupBuilder api)
  {
    api.MapGet("test", () =>
    {
      return Results.Json(new
      {
        msg = "Hello World"
      });
    });
  }
}
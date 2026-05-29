namespace UserAccountsApi.RoutesNS;

public static class MainRouter
{
  public static void MapAPi(WebApplication app)
  {
    RouteGroupBuilder api = app.MapGroup("/api/v1");

    TestRouter.MapAPi(api);
    CloudRouter.MapAPi(api);
  }
}
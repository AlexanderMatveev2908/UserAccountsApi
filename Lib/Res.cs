namespace UserAccountsApi.LibNS;

public static class Res
{
  public static IResult Json(
    int status,
    string message,
    object? data = null
)
  {
    return Results.Json(
          new
          {
            status,
            message,
            data
          },
          statusCode: status
      );
  }
}
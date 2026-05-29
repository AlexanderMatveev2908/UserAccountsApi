namespace UserAccountsApi.ControllersNS.CloudNS;


public static class CloudCtrl
{
  public static IResult PostFile(HttpContext ctx)
  {
    return Results.Json(new { msg = "File uploaded successfully", status = 201 },
        statusCode: 201);
  }
}
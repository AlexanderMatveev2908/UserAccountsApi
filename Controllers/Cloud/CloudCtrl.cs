using UserAccountsApi.ServicesNS.CLoudNS;

namespace UserAccountsApi.ControllersNS.CloudNS;


public static class CloudCtrl
{
  public static async Task<IResult> PostFile(IFormFile file)
  {
    var result = await CloudSvc.UploadSingle(file);

    return Results.Json(new
    {
      msg = "File uploaded successfully",
      data = result
    }, statusCode: 201);

  }
}
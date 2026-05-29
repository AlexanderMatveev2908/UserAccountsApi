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

  public static async Task<IResult> DeleteFile(HttpContext ctx, string publicId, string resourceType)
  {

    await CloudSvc.Delete(publicId, resourceType);

    return Results.Json(new
    {
      msg = "File deleted successfully"
    }, statusCode: 200);
  }
}
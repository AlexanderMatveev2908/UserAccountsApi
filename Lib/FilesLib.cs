namespace UserAccountsApi.LibNS;


public static class FilesLib
{
  public static string MakeFilename(IFormFile file)
  {
    string ext =
 Path.GetExtension(
     file.FileName
 );
    string fileName =
        $"{Guid.NewGuid():N}{ext}";

    return fileName;
  }
}
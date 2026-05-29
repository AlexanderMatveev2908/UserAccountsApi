using System.ComponentModel.DataAnnotations;
using UserAccountsApi.TypesNS;

namespace UserAccountsApi.ValidatorsNS;

public static class UsersCheck
{
  public static async Task<IResult?> Check(HttpContext ctx)
  {
    if (!ctx.Request.HasJsonContentType())
      return Results.Json(new
      {
        status = 415,
        message = "Content-Type must be application/json"
      }, statusCode: 415);


    UsersDto? dto = await ctx.Request.ReadFromJsonAsync<UsersDto>();

    if (dto is null)
      return Results.Json(new
      {
        status = 400,
        message = "Invalid JSON body"
      }, statusCode: 400);

    List<ValidationResult> errors = new();
    ValidationContext validationContext = new(dto);

    bool isValid = Validator.TryValidateObject(
          dto,
          validationContext,
          errors,
          validateAllProperties: true
        );

    if (!isValid)
      return Results.Json(new
      {
        status = 400,
        message = "Invalid user data",
        errors = errors.Select(e => new
        {
          field = e.MemberNames.FirstOrDefault(),
          error = e.ErrorMessage
        })
      }, statusCode: 400);

    ctx.Items["user"] = dto;

    return null;
  }


}
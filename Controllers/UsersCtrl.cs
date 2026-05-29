using Dapper;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using UserAccountsApi.ConfigNS.SqlNS;
using UserAccountsApi.ModelsNS;
using UserAccountsApi.TypesNS;
using Superpower.Model;

namespace UserAccountsApi.ControllersNS.UsersNS;

public static class UsersCtrl
{
  public static async Task<IResult> PostUser(HttpContext ctx, SqlDbCtx db)
  {

    if (ctx.Items["user"] is not UsersDto user)
    {
      return Results.Json(
          new { msg = "User not found in context", status = 500 }, statusCode: 500
      );
    }
    NpgsqlConnection conn =
     (NpgsqlConnection)db.Database.GetDbConnection();
    await conn.OpenAsync();
    await using NpgsqlTransaction trx =
          await conn.BeginTransactionAsync();

    try
    {
      Users createdUser = await conn.QuerySingleAsync<Users>(
       """
            INSERT INTO "Users" ("FirstName", "LastName", "Email", "Password")
            VALUES (@FirstName, @LastName, @Email, @Password )
            RETURNING *;
            """,
       new
       {
         user.FirstName,
         user.LastName,
         user.Email,
         user.Password,
       },
       trx
   );
      await trx.CommitAsync();


      return Results.Json(new
      {
        msg = "User posted",
        status = 201,
        user = createdUser
      }, statusCode: 201);
    }
    catch
    {
      await trx.RollbackAsync();
      throw;
    }

  }


  public static async Task<IResult> DeleteUser(
      SqlDbCtx db,
      int userId
  )
  {
    Users? user =
        await db.Users.FindAsync(userId);

    Console.WriteLine(user);

    if (user is null)
    {
      return Results.Json(
          new
          {
            message = "User not found",
            status = 404
          },
          statusCode: 404
      );
    }

    db.Users.Remove(user);

    int deletedCount =
        await db.SaveChangesAsync();

    return Results.Json(
        new
        {
          msg = "User deleted",
          deletedCount,
          status = 200
        },
        statusCode: 200
    );
  }
}
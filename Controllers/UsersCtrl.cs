using Dapper;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using UserAccountsApi.ConfigNS.SqlNS;
using UserAccountsApi.ModelsNS;
using UserAccountsApi.TypesNS;
using Superpower.Model;
using UserAccountsApi.ServicesNS.SqlTrxNS;

namespace UserAccountsApi.ControllersNS.UsersNS;

public static class UsersCtrl
{
  public static async Task<IResult> PostUser(HttpContext ctx, SqlDbCtx db)
  {

    if (ctx.Items["user"] is not UsersDto user)
      return Results.Json(
          new { msg = "User not found in context", status = 500 }, statusCode: 500
      );

    Users createdUser = null!;

    return await SqlTrxSvc.InTrx(db, async (NpgsqlConnection conn, NpgsqlTransaction trx) =>
      {
        createdUser = await conn.QuerySingleAsync<Users>(
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

        return Results.Json(new
        {
          msg = "User posted",
          status = 201,
          user = createdUser
        }, statusCode: 201);
      });
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